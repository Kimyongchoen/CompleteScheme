using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterStats : ScriptableObject
{
    public Stats[] stats; //몬스터 정보

    [System.Serializable]
    public struct Stats
    {
        public GameObject MonsterPrefab;// 몬스터 프리팹
        public int number; //몬스터Number (보통근거리(1), 강한근거리(2), 원거리(3), 버퍼(4), 회복(5), Stage 보스(6) ) 1201 (1=지역 2=type 01은 Number)
        
        public string name;//몬스터 이름
        
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

        public float experience;//드랍 경험치
        public float gold;//드랍 골드

        public float buff;//버프 공격력 추가 0.2 = 20%
        public float recovery;//힐 추가 초당 +hp

    }
}
