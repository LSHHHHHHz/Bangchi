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
    public class ACT_Skill1_Projectile : BaseSkillLaunch
    {
        public float speed;
        public Vector3 direction;
        private Vector3 startPos;
        private Vector3 movePos;
        public int distance; 

        BoxCollider boxcollider;
       void Awake()
        {
            boxcollider = GetComponent<BoxCollider>();
            startPos = Player.instance.transform.position;
            damage = Player.instance.Current_Attack * 2;
        }
        private void Update()
        {
            elapsedTime += Time.deltaTime;


            //스킬 거리로 파괴
            movePos = this.transform.position;
            if (startPos.x + distance < movePos.x)
            {
                Destroy(gameObject);
            }

            //스킬 속도
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

    }
}
