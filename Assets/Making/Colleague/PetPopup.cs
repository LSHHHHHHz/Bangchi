using Assets.HeroEditor.Common.Scripts.Common;
using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PetPopup : MonoBehaviour 
{
    public GridLayoutGroup gird;
    public GridLayoutGroup effectgird;
    public GameObject petPrefab;
    public GameObject effectPrefab;
    public GameObject noneffectPrefab;

    private List<GameObject> children = new List<GameObject>();
    private List<GameObject> effectchildren = new List<GameObject>();

    private Action<int> oneMoreTimeAction;

    private bool isCoroutineDone = true;
    public void Initialize(PetGachaResult petgachaResult, Action<int> oneMoreTime)
    {
        transform.position = new Vector3(0, -1000, 0);
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
        children.Clear(); 
        foreach (GameObject child in effectchildren)
        {
            Destroy(child);
        }
        effectchildren.Clear();

        this.oneMoreTimeAction = oneMoreTime;
        isCoroutineDone = false;
        StartCoroutine(SetupCoroutine(petgachaResult));
    }

    private IEnumerator SetupCoroutine(PetGachaResult petGachaResult2)
    {
        yield return transform.DOLocalMoveY(0, 1f).Play().WaitForCompletion();

        for (int i = 0; i < petGachaResult2.pets.Count; i++)
        {
            PetInfo petInfo = petGachaResult2.pets[i];
            bool isHighGrade = (int)petInfo.petgrade >= (int)PetGrade.A;
            if (isHighGrade)
            {
                GameObject effect = Instantiate(effectPrefab, effectgird.transform);
                yield return new WaitForSeconds(2f);
                Destroy(effect);
            }
            var noneffect = Instantiate(noneffectPrefab, effectgird.transform);
            effectchildren.Add(noneffect);
          
            PetSlot petSlot = Instantiate(petPrefab, gird.transform).GetComponent<PetSlot>();
            
            petSlot.SetData(petInfo);

            petSlot.effecticon.transform.localScale = Vector3.one * 7;

            petSlot.gameObject.SetActive(true);
            petSlot.icon.color = new Color(1, 1, 1, 0);
            var sequence = DOTween.Sequence();

            petSlot.effecticon.transform.DOScale(1, 0.1f);
            sequence.AppendCallback(() => petSlot.effecticon.gameObject.SetActive(true));
            sequence.Append(petSlot.effecticon.DOFade(0.5f, 0.1f)); 
            sequence.AppendCallback(() => petSlot.icon.gameObject.SetActive(true));
            sequence.Append(petSlot.effecticon.DOFade(0f, 0.1f)); 

            sequence.Join(petSlot.icon.DOFade(1, 0.1f));
           
            sequence.Play();

            children.Add(petSlot.gameObject);
            
            yield return sequence.WaitForCompletion();
        }
        isCoroutineDone = true;
    }
    public void OneMoreTime1()
    {
        if (isCoroutineDone == false)
            return;

        oneMoreTimeAction?.Invoke(1);
    }

    public void OneMoreTime11()
    {
        if (isCoroutineDone == false)
            return;

        oneMoreTimeAction?.Invoke(11);
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}

