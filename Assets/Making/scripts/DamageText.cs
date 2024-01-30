using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destoryTime;

    public int damage;
    public Weapons[] weapon;

    public TextMeshPro text;
    Color alpha;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        Invoke("DestoryObject", destoryTime);
        StartCoroutine(SizeUpandDown());
    }

    private void Awake()
    {
    }
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a,0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }
    IEnumerator SizeUpandDown()
    {
        text.transform.DOScale(1.5f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        text.transform.DOScale(1, 0.1f);
    }
    private void DestoryObject()
    {
        Destroy(gameObject);
    }
}
