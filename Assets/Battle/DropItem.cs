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
        public float exp;
        public int coin;

        Rigidbody prefabRigid;
        Transform target;
        float startSpeed = 1f;
        float maxSpeed = 30f;
        public LayerMask layerMask = -1;
        float createdTime;

        private const float moveWaitTime = 0.5f;

        private void Awake()
        {
            createdTime = Time.time; //30
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
                // 40 - 30
                // 내 나이 = 현재 날짜(가변) - 내 출생 날짜(불변)
                float createdElapsed = Time.time - createdTime; // 아이템이 생성된 후 흐른 시간.
                float moveElapsed = createdElapsed - moveWaitTime; // 움직이기 시작한 시간.

                // Lerp : (시작값, 끝값, 현재 시간)
                // 0,  10,  0.5 --> 5
                // 3, 7, 0.5 --> 5
                // 0, 30, 0.1 --> 3
                // 0, 30, 0.2 --> 6
                // 0, 30, 1 -->   30
                float currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, moveElapsed);
                Vector3 dir = (target.position - transform.position).normalized;
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
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 생성되자말자 플레이어한테 먹히지 않게끔 생성된 후 1초가 지나야 먹을 수 있다.
            if (Time.time - createdTime < 1f)
                return;

            var player = collision.transform.GetComponent<Player>();
            if (player == null)
                return;

            player.Exp += exp;
            player.Coin += coin;
            Destroy(gameObject);
        }
    }
}
