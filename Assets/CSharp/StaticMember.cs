using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CSharp
{
    // member : class가 가진 구성 요소.
    // 필드, 프로퍼티, 메서드

    // instance member
    //   * instance가 가지고 있는 member
    //   * public int Level;
    // static member
    //   * type이 가지고 있는 member
    //   * public static int count;
    //   * MyClass.count = 5;
    

    public class Car
    {

        // 인스턴스 필드들
        public int speed;
        public string modelName;
        public string brand;


        // 정적(스태틱) 필드
        public static int totalCarCount;

        public Car()
        {
            totalCarCount += 1;
        }
    }

    public class CarFactory
    {
        public void Run()
        {
            for (int i = 0; i < 100; i++)
            {
                Car car = new Car();
            }

            Debug.Log($"Total car count : {Car.totalCarCount}");
            // Total car count : 100
            //개체참조는 인스턴스를 필요로 한다는 말
        }
    }

    public class StaticMethodTest
    {
        // 인스턴스 메서드
        public void MyInstanceMethod()
        {

        }

        // 정적 메서드 // 스태틱 메서드
        // 인스턴스 없이 바로 사용 가능.
        public static void MyStaticMethod()
        {
            Car.totalCarCount += 1;
        }
    }
}
