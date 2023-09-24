using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Battle
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager instance;
        public List<Monster> monsterList = new();
       
        public Player player;
        public Vector3 playerInitialPosition;
        public PoolManager pool;

        private void Awake()
        {
            instance = this;
            playerInitialPosition = player.transform.position;
        }

        public void RegisterMonster(Monster monster)
        {
            monsterList.Add(monster);
        }

        public void UnregisterMonster(Monster monster)
        {
            monsterList.Remove(monster);
            Achievement.instance.MonsterKilledCount += 1;
        }
    }
}