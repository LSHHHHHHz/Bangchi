//using Assets.Battle;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PetManager : MonoBehaviour
//{
//    public static PetManager instance;
//    public List<PetInfo> petEquipInfos;

//    private void Awake()
//    {
//        instance = this;
//        petEquipInfos = new List<PetInfo>(3);
       
//    }
//    public void AddEquipPetInfo(PetInfo petInfo)
//    {
//        for(int i = 0; i< petEquipInfos.Count; i++)
//        {
//            if (petEquipInfos[i] == petInfo)
//            {
//                return;
//            }
//        }
//        petEquipInfos.Add(petInfo);

//    }
//}