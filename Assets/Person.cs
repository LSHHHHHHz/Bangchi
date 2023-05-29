using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.CSharp
{
  
    class Program : MonoBehaviour
    {

    }

    public class Animal
    {

    }

    class Person : Animal
    {
        public string Name;
        public int Age;
        public readonly string ResidentRegistrationNumber;

        public Person()
        {

        }

        public Person(string name)
        {
            Name = name;
        }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Person(string name, int age, string residentRegistrationNumber)
        {
            Name = name;
            Age = age;
            ResidentRegistrationNumber = residentRegistrationNumber;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetNameAndAge(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    //생성자가 있으면 new Person 안됨
    class Call
    {
        Person call = new Person();
      

        public void ConstructorTest() // 생성자 : constructor
        {
            // 생성자를 쓰는 이유?
            // 클래스는 데이터를 가질 수 있고, 생성자를 통하든 다른 메서드를 통해서든 데이터를 설정할 수 있음.
            // 클래스의 데이터를 생성시(최초)에만 설정하고 그 이후로는 못바꾸게 하고 싶다!
            // --> 생성자를 통해서 데이터를 전달해서 설정해야 함.

            var person = new Person("홍길동", 33, "92309811-1231245108");
            // person.ResidentRegistrationNumber = "ewewweew"; <--- 생성 이후에 readonly field는 값을 바꿀 수 없음. 그 경우에 생성자를 통해 readonly field의 값을 설정해야 함.

            // readonly 를 쓰는 이유?
            person.Name = "아아";
            //person.ResidentRegistrationNumber = "qweqwe314r";

            
            // 생성자를 쓰는 이유 2
            // 생성자 : 클래스를 생성할 때 호출되는 함수
            // 클래스를 생성했다면 이게 사용할 준비가 완료되는게 코드가 깔끔한 경우가 많음.

            // var instance = new MyClass();
            // instance.SetXXXX();
            // instance.Initasdasdsa();
            // instance.Use();
            // 위와 같은 방법은 지저분하고 사용하기가 어려움.

            // var instance = new MyClass(필요한 설정값들 전달); 
            // var instance.Use();
        }

        public void Test()
        {
            // 상위 클래스 타입으로 변환은 가능.
            Person gildong = new Person();
            Animal a = gildong;


            // 하위 클래스 타입으로 변환은 불가.
            Animal tiger = new Animal();
            //Person p = tiger;

            // 강제로 하는 방법
            Person gildong2 = (Person)a; // 형변환(캐스팅) - 강제 : 변환을 시도하고 실패하면 에러가 남.
            Person gildong3 = a as Person; // 형변환(캐스팅) - 강제X : 변환을 시도하고 실패하면 null을 반환.

            // is 연산자
            if (a is Person)
            {
                Person gildong4 = (Person)a;
            }

            // Type 객체 사용
            if (a.GetType() == typeof(Person))
            {
                Type aType = a.GetType();
                Type personType = typeof(Person);
            }
        }
    }




}
