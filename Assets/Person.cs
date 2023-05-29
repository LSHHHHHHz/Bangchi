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

    //�����ڰ� ������ new Person �ȵ�
    class Call
    {
        Person call = new Person();
      

        public void ConstructorTest() // ������ : constructor
        {
            // �����ڸ� ���� ����?
            // Ŭ������ �����͸� ���� �� �ְ�, �����ڸ� ���ϵ� �ٸ� �޼��带 ���ؼ��� �����͸� ������ �� ����.
            // Ŭ������ �����͸� ������(����)���� �����ϰ� �� ���ķδ� ���ٲٰ� �ϰ� �ʹ�!
            // --> �����ڸ� ���ؼ� �����͸� �����ؼ� �����ؾ� ��.

            var person = new Person("ȫ�浿", 33, "92309811-1231245108");
            // person.ResidentRegistrationNumber = "ewewweew"; <--- ���� ���Ŀ� readonly field�� ���� �ٲ� �� ����. �� ��쿡 �����ڸ� ���� readonly field�� ���� �����ؾ� ��.

            // readonly �� ���� ����?
            person.Name = "�ƾ�";
            //person.ResidentRegistrationNumber = "qweqwe314r";

            
            // �����ڸ� ���� ���� 2
            // ������ : Ŭ������ ������ �� ȣ��Ǵ� �Լ�
            // Ŭ������ �����ߴٸ� �̰� ����� �غ� �Ϸ�Ǵ°� �ڵ尡 ����� ��찡 ����.

            // var instance = new MyClass();
            // instance.SetXXXX();
            // instance.Initasdasdsa();
            // instance.Use();
            // ���� ���� ����� �������ϰ� ����ϱⰡ �����.

            // var instance = new MyClass(�ʿ��� �������� ����); 
            // var instance.Use();
        }

        public void Test()
        {
            // ���� Ŭ���� Ÿ������ ��ȯ�� ����.
            Person gildong = new Person();
            Animal a = gildong;


            // ���� Ŭ���� Ÿ������ ��ȯ�� �Ұ�.
            Animal tiger = new Animal();
            //Person p = tiger;

            // ������ �ϴ� ���
            Person gildong2 = (Person)a; // ����ȯ(ĳ����) - ���� : ��ȯ�� �õ��ϰ� �����ϸ� ������ ��.
            Person gildong3 = a as Person; // ����ȯ(ĳ����) - ����X : ��ȯ�� �õ��ϰ� �����ϸ� null�� ��ȯ.

            // is ������
            if (a is Person)
            {
                Person gildong4 = (Person)a;
            }

            // Type ��ü ���
            if (a.GetType() == typeof(Person))
            {
                Type aType = a.GetType();
                Type personType = typeof(Person);
            }
        }
    }




}
