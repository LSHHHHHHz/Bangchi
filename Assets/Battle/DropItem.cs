using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle
{
    public class DropItem : MonoBehaviour
    {
        public Collider collider;
        public int exp;
        public int coin;
        public int enforceCoin;

        Rigidbody prefabRigid;
        Transform target;
        float startSpeed = 1f;
        float maxSpeed = 30f;
        public LayerMask layerMask = -1;
        float createdTime;

        private const float moveWaitTime = 0.5f;

        private void Awake()
        {
            createdTime = Time.time;            
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
                float createdElapsed = Time.time - createdTime; // 아이템이 생성된 후 흐른 시간.
                float moveElapsed = createdElapsed - moveWaitTime; // 움직이기 시작한 시간.

                float currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, moveElapsed);
                Vector3 posGap = target.position - transform.position;
                Vector3 dir = (posGap).normalized;
                currentSpeed = Mathf.Lerp(0, currentSpeed, posGap.magnitude);
                transform.Translate(dir * currentSpeed * Time.deltaTime, Space.World);
            }
        }


        IEnumerator SearchPlayer()
        {
            // 0.5초 대기했다가, Rigidody를 파괴해 더이상 물리적인 움직임을 하지 않도록 한다.
            // target 설정을 해 target을 쫓아가도록 한다.
            yield return new WaitForSeconds(moveWaitTime);
            target = UnitManager.instance.player.transform;
            Destroy(GetComponent<Rigidbody>());
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.transform.GetComponent<Player>();
            if (player == null)
                return;

            player.Exp += exp;
            player.Coin += coin;
            player.enforceCoin += enforceCoin;
            Destroy(gameObject);
        }
    }
}
