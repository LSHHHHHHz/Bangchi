using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CSharp
{
    internal class MethodExample
    {
        void Test()
        {
            Human gildong = new Human() { Age = 20 };
            gildong.MyProperty = 3;

            Human abc = new Human();
            abc.Run();
        }
    }

    public class Human
    {
        public int Age;
        public int MyProperty
        {
            get
            {
                return myPropertyValue;
            }
            set
            {
                myPropertyValue = value;
            }
        }
        private int myPropertyValue;

        // 메서드 : 객체의 기능
        // 인스턴스 메서드 : 인스턴스를 가지고 메서드를 사용한다. : gildong을 가지고 메서드를 사용한다.
        // 인스턴스 메서드 내에서는 자기 자신을 호출을 생략해요.
        public void Run() // (길동이가) 달린다.
        {
            // this <- Run 메서드를 호출한 객체를 가리킴.
            // 자기 자신 : Human 객체
            // 내 Age를 얻어올 때는 그냥 바로 쓸 수 있음.
            Console.WriteLine("My Age : " + Age);
            var myMog = new Dog();
            Console.WriteLine("My dog height : " + myMog.height);
        }

        public void Eat()
        {
            
            // this <- Eat 메서드를 호출한 객체를 가리킴.
        }
    }

    class Dog
    {
        public int height;
    }
}
