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
[CreateAssetMenu(menuName = "My Assets/SkillInfo")]
public class PetInfo
{
    public PetGrade petgrade;
    public PetType petType;
    public string name;
    public string iconPath;
    public int Number;

}

