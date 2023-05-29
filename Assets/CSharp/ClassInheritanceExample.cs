using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CSharp
{
    // 추상 클래스(abstract class)
    // class 선언시 'abstract' class 앞에다가 붙여주면 됨.
    // 특징
    //  - 그 자체로 추상 클래스의 객체를 생성할 수 없음.
    //    - 추상클래스 : 무언가 명확하지 않은 추상적인 것이 있는 클래스.
    //    - 추상클래스를 상속받는 하위 클래스가 추상적인 부분을 명확하게 해줘야 한디.  
    //     - 이유 : 추상 : 무언가 명확하지 않은 개념을 뜻한다?

    public abstract class Food
    {
        public int Calory;

        // "추상" 프로퍼티.
        public abstract string Ingridient { get; } // 재료
        public int Add(int a, int b)
        {
            return a + b;
        }

        public virtual void GotoTrash()
        {
            UnityEngine.Debug.Log("GotoTrash");
        }
    }

    public class Bread : Food
    {
        public int WheatPercentage; // 밀 함유량

        // override : 위에 있는 것을 내가 다시 구현하겠다!
        public override string Ingridient => "Wheat";
        public override void GotoTrash()
        {
            base.GotoTrash();
            UnityEngine.Debug.Log("GotoTrash : Bread");
        }
    }

    internal class ClassInheritanceExample
    {
        void Test()
        {
            //var food = new Food();

            var bread = new Bread();
            bread.Calory = 30;
            bread.WheatPercentage = 50;
        }
    }
}
