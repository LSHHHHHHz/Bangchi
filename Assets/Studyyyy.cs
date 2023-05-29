using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Studyyyy : MonoBehaviour
{
    public class Solution5
    {
        public void solution()
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                numbers.Add(i);
            }

            foreach (int num in numbers)
            {
                Debug.Log(num);
            }
        }
        
       

    }
}