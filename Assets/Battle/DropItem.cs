using Assets.Item1;
using Assets.Making;
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
        public ItemInfo droppedItemInfo;
        public ItemDB itemDB;


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
            this.exp = BattleManager.instance.currentStageInfo.exp;
            this.coin = BattleManager.instance.currentStageInfo.coin;
        }
        private void Start()
        {
            prefabRigid = GetComponent<Rigidbody>();
            StartCoroutine(ColliderDelay(transform.gameObject,0.2f));
            StartCoroutine(SearchPlayer());
        }
        private void Update() //Time.time과 Time.deltaTime의 차이, Lerp, normalized
        {
            if(target != null)
            {
                //Time.deltaTime은 이전 프레임과 현재 프레임 사이의 시간 간격을 나타내므로 Time.deltaTime 동작 구현을 위해 사용됨
                //아이템이 생성된 후 흐른 시간을 계산할 때에는 Time.time을 사용해야함(절대적 시간 참조가 필요하고 정확한 시간을 알기 위해)

                float createdElapsed = Time.time - createdTime; // 아이템이 생성된 후 흐른 시간.
                float moveElapsed = createdElapsed - moveWaitTime; // 움직이기 시작한 시간.

                float currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, moveElapsed);
                //얼마나 떨어져 있는지 확인함(실제 방향은 정규화를 통해 진행)
                Vector3 posGap = target.position - transform.position;
                //벡터를 정규화하여 방향만을 나타내는 단위 벡터(unit vector)로 변환
                Vector3 dir = (posGap).normalized;
                //currentSpeed = Mathf.Lerp(0, currentSpeed, posGap.magnitude);
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
        private IEnumerator ColliderDelay(GameObject icon, float delay)
        {  //컬라이더 처음에 꺼졌다가 켜지는걸로
            CapsuleCollider iconCollider = icon.GetComponent<CapsuleCollider>();
            if (iconCollider != null)
            {
                iconCollider.enabled = false;
                yield return new WaitForSeconds(delay);
                iconCollider.enabled = true;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            var player = other.transform.GetComponent<Player>();
            if (player == null)
                return;
            AudioManager.instance.PlaySound("DropItem");
            player.Current_Exp += exp + player.AddExp;
            player.Coin += coin + player.AddCoin;
            player.enforceCoin += enforceCoin;

            if (droppedItemInfo != null && droppedItemInfo.type == ItemType.Sword)
            {
                InventoryManager.instance.AddItem(droppedItemInfo);
            }
            Player.instance.statDataSave();
            Destroy(gameObject);
        }
    }
}
