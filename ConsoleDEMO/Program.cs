using TXQ.Utils.Tool;
using System.Linq;

public class Program
{
    public static void Main()
    {

        LOG.INFO(TXQ.Utils.SDK.HWiNFO.AllSensors.Where(X => X.SensorType == TXQ.Utils.SDK.HWiNFO.SensorType.Fan ).EXToJSON(true));
        var CPU = TXQ.Utils.SDK.HWiNFO.AllSensors.Where(X => X.SensorType == TXQ.Utils.SDK.HWiNFO.SensorType.Temperature && X.Model == "CPU").First();

        while (true)
        {
            CPU.ReInit();
            LOG.INFO($"{CPU.Model,-10} Value:{ CPU.Value,-5} ValueMin:{ CPU.ValueMin,-5} ValueMax:{ CPU.ValueMax,-5} ValueAvg:{ CPU.ValueAvg,-5}");
            System.Threading.Thread.Sleep(1000);
        }
    }
}