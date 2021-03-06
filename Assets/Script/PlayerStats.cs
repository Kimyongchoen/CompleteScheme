using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public Stats[] stats; //케릭터 정보

    [System.Serializable]
    public struct Stats
    {
        public GameObject PlayerPrefab;// 케릭터 프리팹
        public string Playername;
        public int level; //레벨

        public int attackDamageMin; //공격력(min)
        public int attackDamageMax; //공격력(max)
        public int health;//체력
        public int Maxhealth;//체력
        public int defense;//방어력

        public float attackSpeed; //공격속도
        public float attackRange; //공격범위
        public float hit;//명중치
        public float evasion;//회피치
        public float criticalChance;//치명타 확률(0-100)
        public float criticalDamagema;//치명타 데미지(몇배 0-1.5 )
        public float vampire;//체력 흡혈 %

        public float experienceBonus;//경험치 획득 %
        public float goldBonus;//골드 획득 %

        public float experience;//경험치
        public float gold;//골드
        public float goldMax;//획득한 골드

        //아이템
        public int Weapon; //무기
        public int Armor; //갑옷
        public int Hat; //투구
        public int Gloves; //장갑
        public int Boots; //부츠
        public int Shield; //망토
    }
}
