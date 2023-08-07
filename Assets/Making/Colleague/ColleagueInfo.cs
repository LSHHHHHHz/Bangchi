using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum ColleagueGrade
{
    D, C, B, A
}

public enum ColleagueType
{
    Water,
    Soil,
    Wind,
    Fire
}
[CreateAssetMenu(menuName = "My Assets/PetInfo")]
public class ColleagueInfo : ScriptableObject
{
    public ColleagueGrade colleagueGrade;
    public ColleagueType colleagueType;
    public string name;
    public string iconPath;
    public int Number;

}

