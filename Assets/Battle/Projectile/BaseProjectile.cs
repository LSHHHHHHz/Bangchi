using Assets.Battle.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Battle.Projectile
{
    public class BaseProjectile : MonoBehaviour
    {
        public int Skillnumber;

        public BaseUnit owner;
        public int damage;
        public float lifetime;
        public float speed;
        public Vector3 direction;

        private float createdTime;

        public Vector3 startPos;
        public Vector3 movePos;
        public int distance;

        BoxCollider boxcollider;
        private float elapsedTime;

        bool isSkill4CoroutineRunning = false;
        private float lastUpdateTime;
        protected virtual void Awake()
        {
            boxcollider = GetComponent<BoxCollider>();


            createdTime = Time.time;

            startPos = Player.instance.transform.position;
            skill4Status();
            Skill8Status();
        }
        private void Update()
        {
            elapsedTime += Time.deltaTime;

            skill2Status();
            skill3Status();



            //스킬 거리로 파괴(스킬 1 사용)
            movePos = this.transform.position;
            if (startPos.x + distance < movePos.x)
            {
                Destroy(gameObject);
            }

            //시간으로 파괴
            if (Time.time - createdTime > lifetime)
            {
                Destroy(gameObject);
                return;
            }

            //(스킬 1 사용)
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        private void OnTriggerEnter(Collider other)
        {
            // skill3Trigger();

            if (owner is Player)
            {
                // 몬스터 공격
                var monster = other.gameObject.GetComponent<Monster>();
                if (monster != null)
                {
                    monster._Current_HP -= damage;
                    // 
                }
            }
            else
            {
                // 플레이어 공격
            }
        }

        private void OnTriggerStay(Collider other)
        {

        }

        public void skill2Status()
        {
            if (boxcollider != null && Skillnumber == 2)
            {
                if (elapsedTime > 0.3f)
                {
                    boxcollider.enabled = true;
                }
            }
        }

        public void skill3Status()
        {
            if (boxcollider != null && Skillnumber == 3)
            {
                if (elapsedTime > 2)
                {
                    boxcollider.isTrigger = false;
                    boxcollider.size = new Vector3(3, 3, 3);
                    boxcollider.isTrigger = true;
                    damage = 2;
                }
            }
        }

        public void skill4Status()
        {
            if (boxcollider != null && Skillnumber == 4 && !isSkill4CoroutineRunning)
            {
                StartCoroutine(Skill4Coroutine());
                isSkill4CoroutineRunning = true;
            }
        }

        private IEnumerator Skill4Coroutine()
        {
            yield return new WaitForSeconds(0.8f);

            for (int i = 0; i < 5; i++)
            {
                boxcollider.isTrigger = true;
                yield return new WaitForSeconds(0.1f);
                boxcollider.isTrigger = false;
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void Skill8Status()
        {
            if (boxcollider != null && Skillnumber == 8)
            {
                StartCoroutine(Skill8Coroutine());
            }
        }
        private IEnumerator Skill8Coroutine()
        {
            yield return new WaitForSeconds(3.2f);
            boxcollider.isTrigger = true;
        }
    }
}
