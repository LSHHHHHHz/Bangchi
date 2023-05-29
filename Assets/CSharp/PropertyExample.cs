using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CSharp
{
    public class PropertyExample : MonoBehaviour
    {
        private int salary;
        private int bonus = 10;

        /* private void SetSalary(int value) //value 값을 매개변수로 넘겨줌 value를 이용해 salary 변수 값을 조정함
         {
             salary = value;
         }

         public int GetSalary() //salary값을 조정할 수는 없지만 보여줄 수는 있음
         {
             return salary;
         }
        */
        public int SalaryP { get { return salary; } private set { salary = value; } }  //get은 읽기, set은 쓰기

        public int AutoProperty { get; private set; } // 자동 프로퍼티를 쓰면 더 간단하게 프로퍼티를 만들 수 있다.
        public int AutoProperty2;

        public int intField;

        void Start()
        {
            SalaryP = 10;
            print(SalaryP);


            print(MyMonthlySalary); // 필드처럼 가볍게 쓸 수 있음. 의미적으로 좀더 이 값에 대한 접근이 빠르다 / 무겁지 않다.

            print(MyMonthlySalary2()); // 함수호출의 경우는 필드나 프로퍼티보다는 좀더 계산량이 많거나 느린 의미가 있음.

            AutoProperty = 10;
            int value = AutoProperty; // value : 10


            int allDays = AllDays;  // 그냥 단순하게 값을 읽어오는 느낌.
            int allDays2 = intField;
            allDays = ComputeAllDays(); // 뭔가 무겁게 계산하는 느낌.
            Debug.Log("AllDays : " + allDays);
            print("Print AllDays : " + allDays);

            string s = "dd";

            // AllDays 프로퍼티는 좋지 못한 예시임. 함수를 사용하는 것이 권장됨.
        }


        // 프로퍼티 : get만 있을수도 있고, set만 있을 수도 있고, 둘다 있을수도 있음.

        public int MyMonthlySalary { get { return salary / 12; } }

        // 사실상 아래 함수와 동일함.
        public int MyMonthlySalary2()
        {
            return salary / 12;
        }

        public int AllDays
        {
            get
            {
                // 모든 날들의 값을 더해서 반환하는 함수
                for (int i = 0; i < 1000000; ++i)
                {
                    // ds
                }

                return 1;
            }
        }

        public int ComputeAllDays()
        {
            // 모든 날들의 값을 더해서 반환하는 함수
            for (int i = 0; i < 1000000; ++i)
            {
                // ds
            }

            return 1;
        }
    }
}
