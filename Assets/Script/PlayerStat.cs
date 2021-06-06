using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStat : ScriptableObject
{
    public int Level; //레벨
    public int Stat;  //사용한 스텟
    public int BonusStat; //남은 스텟

    public int attackDamageMin; //공격력(min)
    public int attackDamageMax; //공격력(max)
    
    public int Maxhealth;//체력
    public int defense;//방어력
    
    public float attackSpeed; //공격속도
    public float hit;//명중치
    public float evasion;//회피치
    public float criticalChance;//치명타 확률(0-100)
    
    public float experienceBonus;//경험치 획득 %
    public float goldBonus;//골드 획득 %
}


