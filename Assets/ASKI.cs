using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ASKI : MonoBehaviour

    //�̰� ����������
{   //10 - 15 + 10 - 20
    //���Ⱑ ������ ���⸦ ������ ��� ���ڸ� �迭�� �������
    //���� int������ ��ȯ�� �Ǹ� ��ȯ��Ű�� ��ȯ���� ������ �״�� �H��


    /* public int solution(string my_string)
     {
         char[] split = new char[] { ' ' };
         string[] s = my_string.Split(split);
         string[] A = new string[my_string.Length];
         for (int i = 0; i < s.Length; i++)
         {
             A[i] = s[i];
         }
         for (int i = 0; i < A.Length; i++)
         {
             if (A[i] == "+")
             {
                 return A[i];
             }
         }

         A[0] = "10";
         A[1] = "-";
         A[2] = "15";
     }
       /*  int[] n = new int[10];
         for (int i = 0; i < 10; i++) 
         {
             n[i] = i;
         }
       */
    public int solution(string my_string)
    {
        int answer = 0;
        string[] strings = my_string.Split(' ');
        bool isAdd = true;

        for (int i = 0; i < strings.Length; i++) 
        {
            if (strings[i] == "+")
            { 
                isAdd = true;
            }
            else if (strings[i] == "-")
            {
                isAdd = false;
            }
            else
            {
                answer += isAdd ? int.Parse(strings[i]) : -int.Parse(strings[i]);
            }
        }
        return answer;
    }

}