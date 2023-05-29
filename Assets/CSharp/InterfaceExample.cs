using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CSharp
{
    public interface IMachine
    {
        int Battery { get; }
    }

    public interface ICalculator : IMachine
    {
        int CurrentValue { get; }

        int Add(int a, int b); // 명세라고도 할 수 있고, 시그니쳐라고도 할 수 있음.
    }

    // 인터페이스 : 하나의 약속
    // 인터페이스를 상속한다(구현한다) : 인터페이스가 가지는 멤버(프로퍼티, 함수등)을 나도 구현하겠다.(약속을 지킨다)
    //                                   ICalculator가 가지는멤버(CurrentValue, Add())를 나도 구현하겠다.

    // public class Human : Animal
    public class MyCalculator : ICalculator, IDisposable
    {
        // 프로퍼티의 get만 있는 경우는 이렇게 쓸 수 있음.
        public int CurrentValue => 10;

        public int Battery => 50;

        // 아래랑 똑같은 코드임.
        //public int CurrentValue
        //{
        //    get
        //    {
        //        return 10;
        //    }
        //} 3 2  11

        // => : return 구문이랑 똑같다고 보면 됨.
        public int Add(int a, int b) => a + b;
        //public int Add(int a, int b)
        //{
        //    return a + b;
        //}
        public void Dispose()
        {

        }
    }


    internal class InterfaceExample
    {
        void Test()
        {
            // 다형성
            // 인간 <-- 포유류 <-- 척추동물 <-- 동물 <-- 생물 <-- 유기체 / 인간(하위) -- 유기체(상위)
            // MyCalculator <-- ICalculator
            MyCalculator myCalculator = new MyCalculator();
            UseCalculator(myCalculator);
        }

        // 1. 하위 클래스는 상위 클래스의 기능을 모두 가지고 있기 때문에, 상위 클래스를 파라미터로 요구하는 함수에 넘길 수 있다.
        // 2. 하위 클래스는 상위 클래스의 일종이기 때문에 하위 클래스는 상위 클래스로도 쓸 수 있다.
        //    사람은           동물의     일종이기 때문에 사람은        동물로 취급할 수 있다.

        void UseCalculator(ICalculator calculator)
        {

        }
    }
}
