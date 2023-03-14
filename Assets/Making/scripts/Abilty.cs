using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilty : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform talkText;
    public int[] ablityPrice;

    public Player enterPlayer;

    public void Enter() //이걸 왜쓰는거지
    {
       
        uiGroup.anchoredPosition = new Vector3(610, 516, 0);
    }

    // Update is called once per frame
    public void Exit()
    {
        uiGroup.anchoredPosition = new Vector3(550, -700, 0);
    }

    public void Buy(int index)
    {
        int price = ablityPrice[index];
        if (price > enterPlayer.Coin)
        {
            StartCoroutine(Talk());
            StopCoroutine(Talk());
            return;
        }

            enterPlayer.Coin -= price;
            enterPlayer.Current_HP += enterPlayer.HPLevel;
            enterPlayer.HPLevel += 1;
            ablityPrice[index] += 1;
        
    }

    IEnumerator Talk()
    {

        talkText.anchoredPosition = new Vector3(550, 837, 0);
        yield return new WaitForSeconds(2f);

        talkText.anchoredPosition = new Vector3(550, -1000, 0);
    }
}
