using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CSharp
{
    internal class DelegateExample
    {
        // delegate : 대리자, 위임자
        // 대표적인 delegate : Action, Func

        // Action : 파라미터만 없거나 있고, 반환타입은 "무조건" void

        public void Run()
        {

        }

        public void Run2(int a, string s)
        {

        }

        // Func : "무조건" 반환 타입이 있는 delegate

        public int GetLevel()
        {
            return 1;
        }

        public int StrLen(string a, string b)
        {
            return a.Length + b.Length;
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }
    }

    public class DelegateTest
    {
        public Action myMethod;

        public void Test()
        {
            DelegateExample example = new DelegateExample();
            Action runDelegate = example.Run;
            Action<int, string> run2Delegate = example.Run2;

            // example.Run();
            runDelegate();

            // example.Run2(1, "qq");
            run2Delegate(1, "qq");


            Func<int> getLevelDelegate = example.GetLevel;
            Func<string, string, int> strLenDelegate = example.StrLen; //반환타입은 맨뒤에

            // example.GetLevel();
            int level = getLevelDelegate();

            // example.StrLen("abc", "qqqqq");
            int strLen = strLenDelegate("abc", "qqqqq");

            MySum mySum = example.Sum;
            // example의 Sum 함수를 mySum 델리게이트(여기선 지역 변수)에 할당(저장)한다.

            int sum = mySum(1, 2);
        }

        public void Test2()
        {
            Action printLog = MyPrintLogMethod;
            Test3(printLog);
        }

        // 델리게이트 객체는 파라미터로도 전달 가능하다.
        // 필드로 저장할 수도 있다.
        public void Test3(Action printLog22)
        {
            if (1 + 1 == 2)
            {
                printLog22();
            }
        }

        public void MyPrintLogMethod()
        {
            Console.WriteLine("qqqqqq");
        }

        public delegate int MySum(int a, int b);

        public class MyClass { }
    }
}
