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
    public event Action OnInitialize;
    public GridLayoutGroup gird;
    public GameObject petPrefab;

    private List<GameObject> children = new List<GameObject>();
    private Action<int> oneMoreTimeAction;

    private bool isCoroutineDone = true;
    void Start()
    {
    }


    public void Initialize(PetGachaResult petgachaResult, Action<int> oneMoreTime)
    {
        transform.position = new Vector3(0, -1000, 0);
        foreach (GameObject child in children)
        {
            Destroy(child); //객체 파괴
        }
        children.Clear(); // 리스트 비우는것


        this.oneMoreTimeAction = oneMoreTime;
        isCoroutineDone = false;
        StartCoroutine(SetupCoroutine(petgachaResult));

    }

    private IEnumerator SetupCoroutine(PetGachaResult petGachaResult2)
    {
        yield return transform.DOLocalMoveY(0, 2f).Play().WaitForCompletion();

        for (int i = 0; i < petGachaResult2.pets.Count; i++)
        {

            PetInfo petInfo = petGachaResult2.pets[i];
            PetSlot petSlot = Instantiate(petPrefab, gird.transform).GetComponent<PetSlot>();
            
           // PetSlot InventoryPetSlot = Instantiate(petPrefab, PetInventoryManager.Instance.inventoryGrid.transform).GetComponent<PetSlot>();
           // InventoryPetSlot.SetData(petInfo);
           
            petSlot.SetData(petInfo);


            var sequence = DOTween.Sequence();
            petSlot.transform.localScale = Vector3.one * 3f;

           

            sequence.Append(petSlot.transform.DOScale(1, 0.2f).SetEase(Ease.OutQuad));
          
            sequence.Play();


            // 아이템 프리팹이 원래 Active:false였으니 이것도 false인 상태. true로 바꿔서 보이게 한다.
            petSlot.gameObject.SetActive(true);
            //InventoryPetSlot.gameObject.SetActive(true);
            // 나중에 삭제해야되니까 children에 넣어서 관리


            children.Add(petSlot.gameObject);
           // PetInventoryManager.Instance.inventoryChildren.Add(InventoryPetSlot.gameObject);
            yield return new WaitForSeconds(0.1f);

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

