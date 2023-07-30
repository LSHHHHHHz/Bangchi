using Assets.Battle.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle.Projectile
{
    public class BaseProjectile : MonoBehaviour
    {
        public BaseUnit owner;
        public int damage;
        public float lifetime;
        public float speed;
        public Vector3 direction;

        private float createdTime;

        protected virtual void Awake()
        {
            createdTime = Time.time;
        }

        private void OnTriggerEnter(Collider other)
        { 
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

        private void Update()
        {
            if (Time.time - createdTime > lifetime)
            {
                Destroy(gameObject);
                return;
            }

            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
