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

    public int attackSpeed; //공격속도
    public int hit;//명중치
    public int evasion;//회피치
    public int criticalChance;//치명타 확률(0-100)

    public int experienceBonus;//경험치 획득 %
    public int goldBonus;//골드 획득 %

    public int attackDamageMinUp; //공격력(min)
    public int attackDamageMaxUp; //공격력(max)

    public int MaxhealthUp;//체력
    public int defenseUp;//방어력

    public float attackSpeedUp; //공격속도
    public float hitUp;//명중치
    public float evasionUp;//회피치
    public float criticalChanceUp;//치명타 확률(0-100)

    public float experienceBonusUp;//경험치 획득 %
    public float goldBonusUp;//골드 획득 %
}


