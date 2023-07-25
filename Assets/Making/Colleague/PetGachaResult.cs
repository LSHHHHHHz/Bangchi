using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PetGachaResult
{
    public List<PetInfo> pets = new List<PetInfo>();
}

internal class PetGachaCalculator
{
    public static PetGachaResult Calculate(int count, PetDB petDB)
    {
        PetGachaResult result = new PetGachaResult();
        List<PetInfo> pets = petDB.pets;
        for (int i = 0; i < count; i++) 
        {
            PetInfo petSelected = pets[UnityEngine.Random.Range(0, pets.Count)];
            result.pets.Add(petSelected);
        }


        return result;
    }
}
