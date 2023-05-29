using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CSharp
{
    //    LG전자
    //LG모니터, LG TV
    //삼성전자
    //삼성모니터, 삼성TV
    public abstract class ElectronicManufacturer
    {
        // 이 전자제품회사가 생상하는 모니터를 하나 가져온다.
        public abstract Monitor GetMonitor();
    }

    public class LG : ElectronicManufacturer
    {
        // 이 전자제품회사(LG)가 생상하는 모니터(LGMonitor)를 하나 가져온다.
        public override Monitor GetMonitor() { return new LGMonitor(); }
        //public Monitor GetMonitor() { return new LGMonitor(); }
    }

    public class Samsung : ElectronicManufacturer
    {
        // 이 전자제품회사(Samsung)가 생상하는 모니터(SamsungMonitor)를 하나 가져온다.
        public override Monitor GetMonitor() { return new SamsungMonitor(); }
        //public Monitor GetMonitor() { return new SamsungMonitor(); }

    }
    public abstract class Monitor
    {
        public abstract void PrintSpec();
        public void QC()
        {
            UnityEngine.Debug.Log("QC complete");
        }
    }
    public class LGMonitor : Monitor
    {
        public override void PrintSpec()
        {
            UnityEngine.Debug.Log("LG Monitor!");
        }
    }
    public class SamsungMonitor : Monitor
    {
        public override void PrintSpec()
        {
            UnityEngine.Debug.Log("Samsung Monitor!");
        }
    }


    internal class PolymorphismExample
    {
        public void Test()
        {
            // 이것도 다형성! 실제 객체 타입은 LG지만, 상위 타입인 ElectronicManufacturer 타입 변수로 사용.
            ElectronicManufacturer manufacturer = new LG();
            // ElectronicManufacturer 타입의 변수 'manufacturer'를 선언하는데,
            // LG타입의 인스턴스를 만들어서(new LG();) 'manufacturer' 변수에 대입(=)한다.
            
            Monitor newMonitor = BuyMonitorFrom(manufacturer);

            Samsung samsung = new Samsung();


            // 이것도 다형성! Samsung 타입 변수를 가지고 상위 타입인 ElectronicManufacturer 파라미터에 값을 전달.
            // Samsung은 ElectronicManufacturer이다. 그러니까 ElectronicManufacturer 타입의 파라미터에도 값을 전달할 수 있다.
            Monitor monitor2 = BuyMonitorFrom(samsung);
        }

        public Monitor BuyMonitorFrom(ElectronicManufacturer manufacturer)
        {
            Monitor monitor = manufacturer.GetMonitor();
            monitor.PrintSpec();
            monitor.QC();
            return monitor;
        }

        //public Monitor BuySamsungMonitor(Samsung manufacturer)
        //{
        //    Monitor monitor = manufacturer.GetMonitor();
        //    monitor.PrintSpec();
        //    monitor.QC();
        //    return monitor;
        //}

        //public Monitor BuyLGMonitor(LG manufacturer)
        //{
        //    Monitor monitor = manufacturer.GetMonitor();
        //    monitor.PrintSpec();
        //    monitor.QC();
        //    return monitor;
        //}
    }
}
