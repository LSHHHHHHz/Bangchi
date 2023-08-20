using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestoryObject", destoryTime);

        
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

    private void DestoryObject()
    {
        Destroy(gameObject);
    }
}
