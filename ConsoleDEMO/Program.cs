using TXQ.Utils.Tool;
using System.Linq;

public class Program
{
    public static void Main()
    {
        TXQ.Utils.SDK.HWiNFO.Init();
        //TXQ.Utils.SDK.GPUZ.ReadSharedMemory();
        //while (true)
        //{
        //    var DATA = TXQ.Utils.SDK.GPUZ.GetListOfSensors().Where(O => O.name == "CPU Temperature").Single();
        //    LOG.INFO(DATA.EXToJSON());
        //    System.Threading.Thread.Sleep(1000);
        //}
        var all = TXQ.Utils.SDK.HWiNFO.AllSensors;
        System.Console.WriteLine(all.EXToJSON(true));
        var CPU = all.Where(X => X.SensorType == TXQ.Utils.SDK.HWiNFO.SensorType.SENSOR_TYPE_TEMP && X.Model == "CPU").First();

        while (true)
        {

            CPU.ReInit();
            LOG.INFO($"{CPU.Model,-10} Value:{ CPU.Value,-5} ValueMin:{ CPU.ValueMin,-5} ValueMax:{ CPU.ValueMax,-5} ValueAvg:{ CPU.ValueAvg,-5}");
            System.Threading.Thread.Sleep(1000);
        }
    }
}