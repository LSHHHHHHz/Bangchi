using Assets.Battle.Projectile;
using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Battle.Skill
{
    public class ACT1_LaunchProjectileSkill : BaseSkill
    {
        public GameObject projectilePrefab;
        public override void Execute()
        {
            var projectile = Instantiate(projectilePrefab).GetComponent<ACT_Skill1_Projectile>();
            projectile.transform.position = owner.transform.position;
            projectile.owner = owner;
            projectile.direction = Vector3.right;
        }
    }
}
