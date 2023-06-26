using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle
{
    public class Prefabs : MonoBehaviour

    {
        public Collider collider;
        public float exp;
        public int coin;

        Rigidbody prefabRigid;
        Transform target;
        float maxSpeed = 100f;
        float CurrentSpeed = 50f;
        public LayerMask layerMask = -1;

        private void Awake()
        {
            BattleManager.instance.target = target;
        }
        private void Start()
        {
            prefabRigid = GetComponent<Rigidbody>();
            StartCoroutine(SearchPlayer());
        }
        private void Update()
        {
            if(target != null)
            {
                if(CurrentSpeed <= maxSpeed)
                {
                    CurrentSpeed = maxSpeed * Time.deltaTime;
                    Vector3 dir = (target.position - transform.position).normalized;
                    transform.up = Vector3.Lerp(transform.up, dir, 0.25f);
                }
            }
        }


        void SearchTarget()
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, 100f, layerMask);
            if(collider != null && collider.Length>0)
            {
                target = collider[UnityEngine.Random.Range(0, collider.Length)].transform;
            }
        }

        IEnumerator SearchPlayer()
        {
            yield return new WaitForSeconds(0.2f);
            SearchTarget();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
