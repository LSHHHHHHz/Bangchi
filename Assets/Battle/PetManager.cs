using Assets.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public static PetManager instance;
    public List<PetInfo> petInfos;

    private void Awake()
    {
        instance = this;
        petInfos = new List<PetInfo>(3);
       
    }

    private void Update()
    {
      
    }
    public void AddPetInfo(PetInfo petInfo)
    {
        for(int i = 0; i< petInfos.Count; i++)
        {
            if (petInfos[i] == petInfo)
            {
                return;
            }
        }
        petInfos.Add(petInfo);

    }
}