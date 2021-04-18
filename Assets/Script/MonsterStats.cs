using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterStats : ScriptableObject
{
    public Stats[] stats; //케릭터 정보

    [System.Serializable]
    public struct Stats
    {
        public GameObject MonsterPrefab;// 몬스터 프리팹
        public int number; //몬스터Number (근거리(1), 원거리(2), 회복(3), 버퍼(4), Stage 보스(5) ) 1001 (1=type 001은 Number)

        public int attackDamageMin; //공격력(min)
        public int attackDamageMax; //공격력(max)
        public int health;//체력
        public int defense;//방어력

        public float attackSpeed; //공격속도
        public float attackRange; //공격범위
        public float hit;//명중치
        public float evasion;//회피치
        public float criticalChance;//치명타 확률
        public float criticalDamagema;//치명타 데미지
        public float vampire;//체력 흡혈 %

        public float experience;//경험치 획득 %
        public float gold;//골드 획득 %

    }
}
