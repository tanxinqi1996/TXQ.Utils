using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TXQ.Utils.Tool;

namespace TXQ.Utils.P2P
{
    public static class Client
    {

        static Client()
        {
            //初始化数据目录
            LOG.INFO($"DataDir:{Workdir}");
            Level = ExIni.Read("P2P", "Level", 100, true);
            Speedlimit = ExIni.Read("P2P", "SpeedLimit", 100, true) * 1024 * 1024;
            Connection = ExIni.Read("P2P", "Conntions", 5, true);
            TellTrackerLimit = ExIni.Read("P2P", "TellTrackerLimit", 10, true);
            //获取IP
            Host = ExIni.Read("P2P", "ReportIP", Environment.UserDomainName, true);
            if (Host == null)
            {
                throw new Exception("没有找到可用的IP");
            }

            //初始化HttpListener
            _httplistener = new HttpListener(); //创建监听实例
            _httplistener.Prefixes.Add($@"http://*:{Port}/"); //添加监听地址 注意是以/结尾。
            LOG.INFO($"ListenOn:http://*:{Port}/");
            LOG.INFO($"Peer:{Peer}");
            _httplistener.Start();
            Task.Run(HTTPListener);
            Task.Run(async () =>
           {
               while (true)
               {
                   try
                   {
                       Directory.CreateDirectory(Workdir);
                       IEnumerable<string> list = new DirectoryInfo(Workdir).GetFiles().Select(O => O.Name);
                       string url = $"{_tracker}api/peer?peer={Peer}&level={Level}";
                       string res = HTTP.Post(url, list.EXToJSON()).Result;
                       if (res == "true")
                       {
                           LOG.DEBUG($"Tell Tracker Succecc WorkDir:{Workdir};Peer:{Peer};Level:{Level};File:{list.Count()}");
                       }
                       else
                       {
                           throw new Exception("Unknown Error!");
                       }
                   }
                   catch (Exception ex)
                   {
                       LOG.ERROR($"Tell Tracker Error WorkDir:{Workdir};Peer:{Peer};Level:{Level}{ex.Message}");
                   }
                   finally
                   {
                       await Task.Delay(1000 * TellTrackerLimit);
                   }
               }
           });

        }

        private static HttpListener _httplistener = null; //文件下载处理请求监听
        //数据目录
        public static string Workdir = Environment.CurrentDirectory + "\\Data\\";
        public static string TempDir = Path.GetTempPath();//向Tracker汇报的Peer地址
        public static string Peer => $@"http://{Host}:{Port}/";
        public static int Level = 100;
        private static int Port = 55555;
        public static string Host;
        private static long _Send = 0;
        private static long _DownLoad = 0;

        public static long Send => _Send;
        public static long DownLoad => _DownLoad;

        //Tracker服务器
        public static string _tracker = "http://server:44444/";

        //文件下载超时 30s
        private static int _downloadTimeout = 1000 * 30;

        //单线程下载速度限制 10m
        private static long Speedlimit = 1024 * 1024 * 10;
        private static int Connection = 5;
        public static int TellTrackerLimit = 15;
        private static void HTTPListener()
        {
            while (true)
            {
                HttpListenerContext request = _httplistener.GetContext(); //接受到新的请求
                Stopwatch watcher = Stopwatch.StartNew();
                LOG.DEBUG($@"Request Start Path:{request.Request.RawUrl}");
                Task.Run(new Action(() =>
                {
                    try
                    {
                        string path = Workdir + request.Request.RawUrl;
                        if (request.Request.RawUrl == "/check")
                        {
                            request.HttpSendText("im ok", 200);
                            return;
                        }
                        else if (File.Exists(path) == false)
                        {
                            request.HttpSendText("File Not Found", 404);
                            return;

                        }
                        else if (path.EndsWith(".txt"))
                        {
                            request.HttpSendText(File.ReadAllText(path), 200);
                            return;

                        }
                        else if (path.EndsWith(".json"))
                        {
                            request.HttpSendText(File.ReadAllText(path), 200);
                            return;

                        }
                        else if (path.EndsWith(".xml"))
                        {
                            request.HttpSendText(File.ReadAllText(path), 200);
                            return;
                        }
                        else
                        {
                            request.HttpSendFile();
                            return;

                        }
                    }
                    catch
                    {

                        request.HttpSendText("File Not Found", 404);
                    }
                    finally
                    {
                        request.Response.OutputStream.Close();
                        LOG.DEBUG($@"Request End Path:{request.Request.RawUrl}  Code:{request.Response.StatusCode}  {watcher.ElapsedMilliseconds}ms");
                        watcher.Stop();
                    }
                }));
            }
        }

        private static void HttpSendText(this HttpListenerContext httpListener, string content, int code = 200)
        {
            httpListener.Response.StatusCode = code;
            httpListener.Response.ContentType = "application/json";
            httpListener.Response.ContentEncoding = Encoding.UTF8;
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            httpListener.Response.ContentLength64 = buffer.Length;
            httpListener.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }
        private static void HttpSendFile(this HttpListenerContext httpListener)
        {
            FileInfo file = new FileInfo(Workdir + httpListener.Request.RawUrl);

            //  httpListener.Response.AddHeader("Accept-Ranges", "bytes");//重要：续传必须  

            using FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);

            long startPos = 0;
            string range = httpListener.Response.Headers["Range"];
            Console.WriteLine(range);
            bool isResume = range.EXIsNullOrEmptyOrWhiteSpace();
            if (isResume == false) //断点续传请求
            {
                Console.WriteLine(range);
                //格式bytes=9216-
                startPos = long.Parse(range.Split('=')[1].Split('-')[0]);
                httpListener.Response.StatusCode = 206;
                httpListener.Response.ContentLength64 = fs.Length - startPos;
                fs.Position = startPos; //设置传送的起始位置
            }
            else
            {
                httpListener.Response.ContentLength64 = fs.Length;
            }
            httpListener.Response.ContentType = "application/octet-stream";
            httpListener.Response.AddHeader("Content-Disposition", $@"attachment;filename={file.Name}");

            CopyStream(fs, httpListener.Response.OutputStream); //文件传输
            httpListener.Response.OutputStream.Close();
            fs.Close();
        }

        private static void CopyStream(Stream orgStream, Stream desStream)
        {
            byte[] buffer = new byte[1024 * 1024];
            int read;
            while ((read = orgStream.Read(buffer, 0, 1024)) > 0)
            {
                _Send += buffer.Length;
                desStream.Write(buffer, 0, read);
            }
        }

        public static void GenerateDHT(string InputFile, string OutPutFilePath, int cutFileSize = 1024 * 1024 * 100)
        {
            FileInfo fileInfo = new FileInfo(InputFile);
            LOG.INFO($"正在读取文件，耗时较长，请稍候，{fileInfo.Length / 1024 / 1024}MB");
            //文件流读取文件
            DHT DHT = new DHT()
            {
                SubFiles = new Dictionary<int, string>(),
                DateTime = DateTime.Now,
                FileName = fileInfo.Name,
                SHA = fileInfo.ExGetSha1()
            };
            using FileStream FileStream = new FileStream(fileInfo.FullName, FileMode.Open);
            using BinaryReader FileReader = new BinaryReader(FileStream);

            byte[] cutBytes;
            //分割的文件个数
            int fileCount = Convert.ToInt32(Math.Ceiling((double)FileStream.Length / cutFileSize));
            for (int i = 0; i < fileCount; i++)
            {
                LOG.INFO($"正在分片:{i + 1}/{fileCount}");
                //分割后的文件流
                cutBytes = FileReader.ReadBytes(cutFileSize);
                //分割后的文件SHA1
                string sha = cutBytes.EXGetSha1();
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
                     string SHA = new FileInfo(combineFile).ExGetSha1();
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
                 using FileStream CombineFS = File.OpenWrite(combineFile);
                 using BinaryWriter CombineFW = new BinaryWriter(CombineFS);

                 foreach (KeyValuePair<int, string> item in file.SubFiles.OrderBy(o => o.Key))
                 {
                     LOG.INFO($"{file.SHA}:正在合并文件 {item.Key + 1}/{file.SubFiles.Count}");
                     using FileStream fs = File.OpenRead($@"{Workdir}{item.Value}");
                     using BinaryReader fr = new BinaryReader(fs);
                     byte[] TempBytes = fr.ReadBytes((int)fs.Length);
                     //  if (item.Value == TempBytes.EXGetSha1())
                     //  {
                     CombineFW.Write(TempBytes);
                     //   }
                     fs.Close();
                     fr.Close();
                 }
                 CombineFS.Close();
                 CombineFW.Close();
                 LOG.INFO("正在读取校验SHA1，时间可能较长，请稍后");
                 string SHA1 = new FileInfo(combineFile).ExGetSha1();
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

        private static async Task<bool> HttpDownload(string url, string path, string sha1, long speedlimit = 1024 * 1000 * 100)
        {
            Directory.CreateDirectory(TempDir);
            string tempFile = TempDir + "\\" + Guid.NewGuid().ToString();
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            Directory.CreateDirectory(TempDir);
            Directory.CreateDirectory(Workdir);
            try
            {

                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                byte[] bArr = new byte[1024 * 1024];
                int size = responseStream.Read(bArr, 0, bArr.Length);
                //创建计时器
                Stopwatch watcher = new Stopwatch();
                int lenth = 0;

                watcher.Start();
                while (size > 0)
                {
                    //限速相关代码
                    if (watcher.ElapsedMilliseconds < 100 && lenth > speedlimit / 10)
                    {
                        lenth = 0;
                        await Task.Delay((int)(100 - watcher.ElapsedMilliseconds));
                        watcher.Restart();
                    }
                    lenth += size;
                    _DownLoad += size;
                    await fs.WriteAsync(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, bArr.Length);
                }
                fs.Close();
                responseStream.Close();
                string hash = new FileInfo(tempFile).ExGetSha1();
                if (sha1 != hash)
                {
                    throw new Exception($"HASH校验失败:{sha1};{hash}");
                }
                LOG.INFO($"{sha1}:HASH校验完成，下载成功");
                File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
                LOG.ERROR($"{sha1}{url}:下载失败，{ex.Message}");
                return false;
            }
        }

        public static async Task<string> DownLoadDHTFile(DHT DHT, string path)
        {

            List<Task> tasks = new List<Task>();
            foreach (KeyValuePair<int, string> ITEM in DHT.SubFiles)
            {
                string filename = Workdir + ITEM.Value;
                if (File.Exists(filename))
                {
                    LOG.INFO($"{ ITEM.Value}: 文件已存在");
                    continue;
                }
                tasks.Add(new Task(() =>
                {
                    SubDownLoadTask(ITEM.Value);
                }));
            }
            Stopwatch watch = Stopwatch.StartNew();
            while (tasks.Any(O => O.IsCompleted == false))
            {
                List<Task> waittorun = tasks.Where(O => O.Status == TaskStatus.Created).ToList();
                int running = tasks.Where(O => (O.Status == TaskStatus.Running)).Count();
                int IsCompleted = tasks.Where(O => O.IsCompleted).Count();
                if (waittorun.Count > 0 && running < Connection)
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

        public static async void SubDownLoadTask(string sha)
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

                    string Peers = await HTTP.Get(trackerurl);
                    if (Peers == null)
                    {
                        throw new Exception($"Tracker Report Error;Request:{trackerurl};Reslt:{Peers}");
                    }
                    List<string> p = Peers.EXJsonToType<List<string>>();
                    LOG.DEBUG($"{sha}：Found Peer {Peers}");
                    foreach (string ITEM in Peers.EXJsonToType<List<string>>())
                    {
                        peerurl = ITEM + sha;
                        LOG.INFO($"{sha}: form {ITEM}");


                        Directory.CreateDirectory(Workdir);

                        if (HTTP.Get($"{ITEM}check").Wait(500) == false)
                        {
                            throw new Exception("无法连接到Peer");
                        }
                        Task<bool> downloadok = HttpDownload(peerurl, Workdir + sha, sha, Speedlimit);
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
                    LOG.INFO($"{sha}: 下载错误; peerurl:{peerurl} {ex.Message} ");
                    await Task.Delay(2000);

                }

            }

        }

    }
}
