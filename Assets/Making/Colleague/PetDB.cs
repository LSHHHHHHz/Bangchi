using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/PetDB")]
public class PetDB : ScriptableObject
{
    public List<PetInfo> pets = new List<PetInfo>();
}

