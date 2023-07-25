using Assets.Battle;
using Assets.Item1;
using Assets.Making.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PetUI : MonoBehaviour
{
    public PetType type;

    public Text[] PetPriceText;
    public int[] PetPrice;
    PetPopup petPopup;
    PetDB petDB;

    public void Awake()
    {
        
    }
    public PetUI()
    {
    }

    public void Update()
    {
       
    }

    public void RunPet(int count, Action<int> oneMoreTime)
    {
        if(petPopup != null)
        {
            GameObject prefab = Resources.Load<GameObject>("PetPopup");

            petPopup = Instantiate(prefab).GetComponent<PetPopup>();
        }

        PetGachaResult petGachaResult = PetGachaCalculator.Calculate(count, petDB);




    }



}

