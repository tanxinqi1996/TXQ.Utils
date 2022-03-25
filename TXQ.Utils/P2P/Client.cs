using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TXQ.Utils.Tool;

namespace TXQ.Utils.P2P
{
    public static class Client
    {
        private static HttpListener _httplistener = null; //文件下载处理请求监听
        //数据目录
        public static string Workdir;
        public static string TempDir = Path.GetTempPath();//向Tracker汇报的Peer地址
        public static string _peer => $@"http://{_host}:{_port}/";
        private static int _port = 55555;
        private static string _host;
        //Tracker服务器
        public static string _tracker = "http://192.168.31.239:44444/";

        //文件下载超时 30s
        private static int _downloadTimeout = 1000 * 30;

        //单线程下载速度限制 10m
        private static int _speedlimit = 1024 * 1024 * 20;
        private static int _downloadconnection = 10;

        private static long _ID = 1;
        private static int _Conntions = 0;
        private static void HTTPListener()
        {
            while (true)
            {
                var request = _httplistener.GetContext(); //接受到新的请求
                var id = _ID++;
                var watcher = Stopwatch.StartNew();
                LOG.INFO($@"Request {id} Start  Path:{request.Request.RawUrl}");
                _Conntions++;
                Task.Run(new Action(() =>
                {
                    try
                    {
                        var path = Workdir + request.Request.RawUrl;
                        if (File.Exists(path) == false)
                        {
                            request.SendText("File Not Found", 404);
                        }
                        else if (path.EndsWith(".txt"))
                        {
                            request.SendText(File.ReadAllText(path), 200);
                        }
                        else if (path.EndsWith(".json"))
                        {
                            request.SendText(File.ReadAllText(path), 200);
                        }
                        else if (path.EndsWith(".xml"))
                        {
                            request.SendText(File.ReadAllText(path), 200);
                        }
                        else
                        {
                            request.SendFile();
                        }

                    }
                    catch
                    {
                        request.SendText("File Not Found", 404);
                    }
                    finally
                    {
                        request.Response.OutputStream.Close();
                        LOG.INFO($@"Request {id} End  Code:{request.Response.StatusCode}  {watcher.ElapsedMilliseconds}ms");
                        watcher.Stop();
                        _Conntions--;

                    }
                }));
            }
        }

        private static void SendText(this HttpListenerContext httpListener, string content, int code = 200)
        {
            httpListener.Response.StatusCode = code;
            httpListener.Response.ContentType = "application/json";
            httpListener.Response.ContentEncoding = Encoding.UTF8;
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            httpListener.Response.ContentLength64 = buffer.Length;
            httpListener.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }
        private static void SendFile(this HttpListenerContext httpListener)
        {
            var path = Workdir + httpListener.Request.RawUrl;
            var file = new FileInfo(path);
            FileStream fs = new(file.FullName, FileMode.Open, FileAccess.Read);
            var buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length); //将文件读到缓存区
            httpListener.Response.StatusCode = 200;
            httpListener.Response.Headers.Add("content-disposition", $@"attachment;filename={file.Name}");
            httpListener.Response.ContentType = "application/octet-stream";
            httpListener.Response.ContentLength64 = buffer.Length;
            httpListener.Response.OutputStream.Write(buffer, 0, buffer.Length);  //将缓存区的字节数写入当前请求流返回
            httpListener.Response.OutputStream.Close();
            fs.Close();

        }



        public static void GenerateDHT(string InputFile, string OutPutFilePath, int cutFileSize = 1024 * 1024 * 100)
        {
            var fileInfo = new FileInfo(InputFile);
            LOG.INFO($"正在读取文件，耗时较长，请稍候，{fileInfo.Length / 1024 / 1024}MB");
            //文件流读取文件
            var DHT = new DHT()
            {
                SubFiles = new Dictionary<int, string>(),
                DateTime = DateTime.Now,
                FileName = fileInfo.Name,
                SHA = fileInfo.ExGetSha1()
            };
            using var FileStream = new FileStream(fileInfo.FullName, FileMode.Open);
            using var FileReader = new BinaryReader(FileStream);

            byte[] cutBytes;
            //分割的文件个数
            int fileCount = Convert.ToInt32(Math.Ceiling((double)FileStream.Length / cutFileSize));
            for (int i = 0; i < fileCount; i++)
            {
                LOG.INFO($"正在分片:{i + 1}/{fileCount}");
                //分割后的文件流
                cutBytes = FileReader.ReadBytes(cutFileSize);
                //分割后的文件SHA1
                var sha = cutBytes.EXGetSha1();
                //分割文件名
                string cutFileName = Path.Combine(Workdir, sha);
                //写入分割文件
                using FileStream tempStream = new(cutFileName, FileMode.OpenOrCreate);
                using BinaryWriter tempWriter = new(tempStream);
                tempWriter.Write(cutBytes);
                DHT.SubFiles.Add(i, sha);
            }
            FileStream.Close();
            FileReader.Close();
            LOG.INFO("分片完成");
            File.WriteAllText(OutPutFilePath, DHT.EXToJSON());
        }


        private static async Task<bool> CombineFiles(DHT file, string combineFile)
        {
            await Task.Run(() =>
             {
                 if (File.Exists(combineFile))
                 {
                     LOG.INFO("文件已存在，正在读取校验SHA1，时间可能较长，请稍后");
                     var SHA = new FileInfo(combineFile).ExGetSha1();
                     if (SHA == file.SHA)
                     {
                         LOG.INFO($"{SHA}：校验成功");
                         return true;
                     }
                     else
                     {
                         LOG.INFO($"{SHA}：校验失败，尝试覆盖当前文件");
                     }
                 }
                 LOG.INFO($"{file.SHA}:正在合并文件");
                 using var CombineStream = new FileStream(combineFile, FileMode.OpenOrCreate);
                 using var CombineWriter = new BinaryWriter(CombineStream);
                 foreach (var item in file.SubFiles.OrderBy(o => o.Key))
                 {
                     LOG.INFO($"{file.SHA}:正在合并文件 {item.Key + 1}/{file.SubFiles.Count}");
                     using var fileStream = new FileStream($@"{Workdir}{item.Value}", FileMode.Open);
                     using var fileReader = new BinaryReader(fileStream);
                     byte[] TempBytes = fileReader.ReadBytes((int)fileStream.Length);
                     if (item.Value == TempBytes.EXGetSha1())
                     {
                         CombineWriter.Write(TempBytes);
                     }
                 }
                 CombineWriter.Close();
                 CombineStream.Close();
                 LOG.INFO("正在读取校验SHA1，时间可能较长，请稍后");
                 var SHA1 = new FileInfo(combineFile).ExGetSha1();
                 if (SHA1 == file.SHA)
                 {
                     LOG.INFO($"{SHA1}：校验成功");
                     return true;
                 }
                 else
                 {
                     LOG.ERROR($"校验失败：{SHA1}");
                     return false;
                 }
             });
            return false;
        }

        private static async Task<bool> HttpDownload(string url, string path, string sha1, int speedlimit = 1024 * 1000 * 100)
        {
            Directory.CreateDirectory(TempDir);
            var tempFile = TempDir+"\\"+sha1;

            try
            {
                var fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                var request = WebRequest.Create(url) as HttpWebRequest;
                var response = request.GetResponse() as HttpWebResponse;
                var responseStream = response.GetResponseStream();
                var bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, bArr.Length);
                //创建计时器
                var watcher = new Stopwatch();
                int lenth = 0;

                watcher.Start();
                while (size > 0)
                {
                    //限速相关代码
                    if (watcher.ElapsedMilliseconds < 200 && lenth > speedlimit / 5)
                    {
                        lenth = 0;
                        await Task.Delay((int)(200 - watcher.ElapsedMilliseconds));
                        watcher.Restart();
                    }
                    lenth += size;

                    await fs.WriteAsync(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, bArr.Length);
                }
                fs.Close();
                responseStream.Close();
                var hash = new FileInfo(tempFile).ExGetSha1();
                if (sha1 != hash)
                {
                    throw new Exception("HASH校验失败");
                }
                LOG.INFO($"{sha1}:HASH校验完成，下载成功");
                File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                LOG.ERROR($"{sha1}{url}:下载失败，{ex.Message}");
                return false;
            }
        }

        public static async Task<string> DownLoadFile(DHT DHT, string path)
        {

            var tasks = new List<Task>();
            foreach (var ITEM in DHT.SubFiles)
            {
                string filename = Workdir + ITEM.Value;
                if (File.Exists(filename))
                {
                    LOG.INFO($"{ ITEM.Value}: 文件已存在");
                    continue;
                }
                tasks.Add(new Task(() =>
                {
                    Add(ITEM.Value);
                }));
            }
            Stopwatch watch = Stopwatch.StartNew();
            while (tasks.Any(O => O.IsCompleted == false))
            {
                var waittorun = tasks.Where(O => O.Status == TaskStatus.Created).ToList();
                int running = tasks.Where(O => (O.Status == TaskStatus.Running)).Count();
                int IsCompleted = tasks.Where(O => O.IsCompleted).Count();
                if (waittorun.Count > 0 && running < _downloadconnection)
                {
                    int I = new Random().Next(0, waittorun.Count);
                    waittorun[I].Start();
                }
                LOG.INFO($"Total:{tasks.Count}  Wait:{waittorun.Count}  Running:{running}  Completed:{IsCompleted}  Time:{watch.ElapsedMilliseconds / 1000}s");
                await Task.Delay(1000);
            }
            LOG.INFO($"{DHT.SHA},下载完成");
            await CombineFiles(DHT, path + DHT.FileName);
            return path + DHT.FileName;

        }

        public static async void Add(string sha)
        {
            LOG.INFO($"{sha}: 开始下载");

            string filename = Workdir + sha;
            if (File.Exists(filename))
            {
                LOG.INFO($"{sha}: 文件已存在");

                return;
            }
            while (!File.Exists(filename))
            {
                string trackerurl = $"{_tracker}api/peer?filehash={sha}&count=1";
                string peerurl = null;
                try
                {

                    var Peers = await HTTP.Get(trackerurl);
                    if (Peers == null)
                    {
                        throw new Exception($"Tracker Report Error;Request:{trackerurl};Reslt:{Peers}");
                    }
                    var p = Peers.EXJsonToType<List<string>>();
                    foreach (var ITEM in Peers.EXJsonToType<List<string>>())
                    {
                        peerurl = ITEM + sha;
                        LOG.INFO($"{sha}: form {ITEM}");

                        if (Directory.Exists(Workdir) == false)
                        {
                            Directory.CreateDirectory(Workdir);
                            LOG.INFO($"DataDir:{Workdir}");
                        }
                        var downloadok = HttpDownload(peerurl, Workdir + sha, sha, _speedlimit);
                        if (downloadok.Wait(_downloadTimeout))
                        {
                            if (downloadok.Result == true)
                            {
                                return;
                            }
                        }
                        else
                        {
                            LOG.INFO($"{sha}: 下载超时");
                            continue;
                        }
                    }

                }
                catch (Exception ex)
                {
                    LOG.INFO($"{sha}: 下载错误;trackerurl:{trackerurl} peerurl:{peerurl} {ex.Message} ");

                }

            }

        }

        public static void Init(bool startpeer = true, string workdir = null)
        {
            //初始化数据目录
            Workdir = workdir ?? Environment.CurrentDirectory + "\\Data\\";
            LOG.INFO($"DataDir:{Workdir}");

            //创建数据目录
            if (Directory.Exists(Workdir) == false)
            {
                Directory.CreateDirectory(Workdir);
                LOG.INFO($"DataDir:{Workdir}");
            }

            if (startpeer == false) return;

            //获取IP
            foreach (var item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (item.ToString().StartsWith("192.168"))
                {
                    _host = item.ToString();
                    break;
                }
            }
            if (_host == null) throw new Exception("没有找到可用的IP");

            //初始化HttpListener
            _httplistener = new HttpListener(); //创建监听实例
            _httplistener.Prefixes.Add($@"http://*:{_port}/"); //添加监听地址 注意是以/结尾。
            LOG.INFO($"ListenOn:http://*:{_port}/");
            LOG.INFO($"Peer:{_peer}");
            _httplistener.Start();
            Task.Run(HTTPListener);
            var t = Task.Run(async () =>
              {
                  while (true)
                  {
                      try
                      {
                          var list = new DirectoryInfo(Workdir).GetFiles().Select(O => O.Name).EXToJSON();
                          var url = $"{_tracker}api/peer?peer={_peer}";
                          var res = HTTP.Post(url, list).Result;

                          if (res == "true")
                          {
                              LOG.INFO("Tell Tracker Succecc");
                          }
                          else
                          {
                              throw new Exception("Unknown Error!");
                          }
                          await Task.Delay(1000 * 30);
                      }
                      catch
                      {
                      }
                  }
              });

        }
    }
}
