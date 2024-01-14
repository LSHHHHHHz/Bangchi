using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum PetGrade
{
    D, C, B, A
}

public enum PetType
{
    Water,
    Soil,
    Wind,
    Fire
}

[CreateAssetMenu(menuName = "My Assets/PetInfo")]
public class PetInfo : ScriptableObject
{
    public PetGrade petgrade;
    public PetType petType;
    public string name;
    public string iconPath;
    public int Number;
    public int petAttack;
    public int petHP;
    public int petExp;
    public int petCoin;
}

