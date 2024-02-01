using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public class ColleagueUI : MonoBehaviour
{
    public RectTransform[] uiGroup;
    Vector3 ExitColleagueUI = new Vector3(-1000, 0, 0);
    public static ColleagueUI instance;
    public ColleaguePoly colleaguePoly;
    public Vector3[] effectImagepos;
    private void Awake()
    {
        instance = this;
        effectImagepos = new Vector3[3];
    }



    public void ChageColleagueUI(int index)
    {
        for (int i = 0; i < uiGroup.Length; i++)
        {
            if(i == index)
            {
                uiGroup[i].localPosition = new Vector3(0, 0, 0);
                colleaguePoly = uiGroup[i].GetComponent<ColleaguePoly>();
                for(int j = 0; j< colleaguePoly.effectimagePos.Length; j++)
                {
                    effectImagepos[j] = colleaguePoly.effectimagePos[j].transform.localPosition;
                }
            }
            else
            {
                uiGroup[i].localPosition = ExitColleagueUI;
            }
        }
        
    }
}