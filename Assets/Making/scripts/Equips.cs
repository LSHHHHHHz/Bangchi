using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equips : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform talkText;
    public Player enterPlayer;
    public void EnterEquip()
    {
        uiGroup.anchoredPosition = new Vector3(0, 1670, 0);
    }

    public void ExitEquip()
    {
        uiGroup.anchoredPosition = new Vector3(2049, -1607, 0);
    }
}
