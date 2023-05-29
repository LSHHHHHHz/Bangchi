using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CSharp
{

    public class DelegateExample : MonoBehaviour
    {
        int a = 5;
        int b = 10;

        int sum;

        void Add()
        {
            sum = a + b;
            print("Add");
        }


        void Back()
        {
            sum = 0;
            print("Back");
        }

        delegate void MyDelegate();
        MyDelegate myDelegate;

        void Start()
        {

            myDelegate = Add;

            myDelegate += delegate () { print("무명 메소드"); }; //무명 메소드
            myDelegate += () => print("람다식");//람다식 

            myDelegate += Back;
            myDelegate();

            //질문
            a += b;
            a = a + b;

            myDelegate += Back;


            int num = 1;
            num += 2;
            num = num + 2;

            myDelegate = Add;
            myDelegate += Back;
            myDelegate = myDelegate + Back;
        }




        // 이런 간단한 타입의 경우 System.Action<>이나 System.Func<>을 사용.
        delegate int AgeGetter();
        delegate int MoneyGetter();

        // 복잡한 함수의 경우는 다른 사람들과의 원활한 소통과 오해 방지를 위해 델리게이트를 정의해서 사용.
        delegate int PersonScoreCalculator(int age, int height, int weight, string name, List<int> parentScores);

        public int CalculatePersonScore(int age, int height, int weight, string name, List<int> parentScores)
        {
            return age + (int)(height * 1.35) + (int)(weight / 0.5f) + name.Length + (int)parentScores.Average();
        }

        public DelegateExample()
        {




            // ShowMenu 함수에 MyDelegate를 넘겨줘야 함.
            // - 함수를 넣음 : GetAge_Korea
            // - 자동으로 이 함수가 해당 MyDelegate 타입으로 변환
            ShowMenu(GetAge_Korea);
            ShowMenu(GetAge_Japan);
            ShowMoney(Money);
        }

        void ShowPersonScore(PersonScoreCalculator calculator)
        {
            calculator(10, 184, 32, "sads", new List<int>());
            int score = calculator(10, 187, 212, "ds", new List<int>() { 23, 6 });
        }

        void ShowPersonScore(Func<int, int, int, string, List<int>, int> calculator)
        {
            int score = calculator(10, 187, 212, "ds", new List<int>() { 23, 6 });
        }

        //void ShowMenu(AgeGetter Getage)
        void ShowMenu(Func<int> Getage)
        {
            int age = Getage();
            if (age >= 20)
            {

            }
            else
            {

            }
        }
        void ShowMoney(Func<int> getMoney)
        {
            int money = getMoney();
            if (money >= 10)
            {

            }
        }


        public int GetAge_Korea() // MyDelegate, Func<int> 둘다 변환 가능
        {
            return 0;
        }

        public int GetAge_Japan()
        {
            return 0;
        }

        public int Money()
        {
            return 0;
        }
    }

}


internal class DelegateExample2
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
        DelegateExample2 example = new DelegateExample2();
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