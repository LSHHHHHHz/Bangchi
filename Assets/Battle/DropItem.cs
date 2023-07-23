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
                Vector3 posGap = target.position - transform.position;
                Vector3 dir = (posGap).normalized;
                currentSpeed = Mathf.Lerp(0, currentSpeed, posGap.magnitude);
                transform.Translate(dir * currentSpeed * Time.deltaTime, Space.World);
                // 아이템을 움직이는 코드. 정해진 만큼 움직임.
                // 만약 플레이어랑 아이템이랑 굉장히 가까운 상태면 정해진 만큼 움직였을 때 플레이어를 지나칠수도 있음.
                // 그것을 방지하기 위해서  currentSpeed = Mathf.Lerp(0, currentSpeed, posGap.magnitude); <-- 이 구문을 넣었음.
                // posGap.magnitude : 아이템과 플레이어 사이의 거리.
                // Mathf.Lerp(0, 10, 1) : 10
                // Mathf.Lerp(0, 10, 0.3) 3
                // Mathf.Lerp(0, 10, 0.1) 1
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
            //// 생성되자말자 플레이어한테 먹히지 않게끔 생성된 후 1초가 지나야 먹을 수 있다.
            //if (Time.time - createdTime < 1f)
            //    return;

            var player = other.transform.GetComponent<Player>();
            if (player == null)
                return;

            player.Exp += exp;
            player.Coin += coin;
            Destroy(gameObject);
        }
    }
}
