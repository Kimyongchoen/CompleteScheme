using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerInfo : MonoBehaviour
{
    public Text Playername;
    public Text level;
    public Text attackDamage;
    public Text health;
    public Text defense;
    public Text attackSpeed;
    public Text attackRange;
    public Text hit;
    public Text evasion;
    public Text criticalChance;
    public Text criticalDamagema;
    public Text vampire;
    public Text experienceBonus;
    public Text goldBonus;
    public Text experience;
    public Text gold;
    public Text ScreenGold;
    public Text ScreenLevel;

    public ItemStats ItemStats;
    public PlayerStats.Stats playerStats;


    public void ChangeMonsterInfomation(int PlayerNum)
    {
        int AttackDamagePlus = 0;
        int defensePlus = 0;
        int hp = playerStats.health;

        Playername.text = "플레이어 이름 : " + playerStats.Playername; // 플레이어 이름

        level.text = "Level : " + playerStats.level; // 레벨

        attackDamage.text = "공력력 : " + playerStats.attackDamageMin.ToString() + " - " + playerStats.attackDamageMax.ToString(); //공격력
        if (ItemStats.AttackDamageUp > 0)//공격력 버프 증가시
            AttackDamagePlus += ItemStats.AttackDamageUp;
        if (playerStats.Weapon >= 0)//무기 강화에 따른 데미지 UP 무기가 없으면 -1
            AttackDamagePlus += ItemStats.Weapon[playerStats.Weapon];
        if (AttackDamagePlus > 0)
            attackDamage.text += " + (" + AttackDamagePlus + ")";

        if (playerStats.Hat >= 0)//투구 강화에 따른 체력 UP 투구가 없으면 -1
        {
            health.text = "체력  : " + hp.ToString() + " / " + (playerStats.health - ItemStats.Hat[playerStats.Hat]).ToString() + "+(" + ItemStats.Hat[playerStats.Hat] + ")";//체력
        }
        else
        {
            health.text = "체력  : " + hp.ToString() + " / " + playerStats.health.ToString();//체력
        }

        defense.text = "방어력 : " + playerStats.defense.ToString();//방어력
        if (ItemStats.DefenseUp > 0)//방어력 버프 증가시
            defensePlus += ItemStats.DefenseUp;
        if (playerStats.Armor >= 0)//갑옷 강화에 따른 방어력 UP 갑옷이 없으면 -1
            defensePlus += ItemStats.Armor[playerStats.Armor];
        if (defensePlus > 0)
            defense.text += " + (" + defensePlus + ")";

        attackSpeed.text = "공격속도 : " + playerStats.attackSpeed.ToString(); //공격속도

        attackRange.text = "공격범위 : " + playerStats.attackRange.ToString(); //공격범위

        hit.text = "명중치 : " + playerStats.hit.ToString();//명중치

        evasion.text = "회피치 : " + playerStats.evasion.ToString();//회피치
        if (playerStats.Boots >= 0)//강화에 따른 회피 UP 부츠가 없으면 -1
            evasion.text += " + (" + ItemStats.Boots[playerStats.Boots] + ")";

        criticalChance.text = "치명타 확률 % : " + playerStats.criticalChance.ToString();//치명타 확률
        if (playerStats.Gloves >= 0)//강화에 따른 치확 UP 장갑이 없으면 -1
            criticalChance.text += " + (" + ItemStats.Gloves[playerStats.Gloves] + ") %";

        criticalDamagema.text = "치명타 데미지 : " + playerStats.criticalDamagema.ToString();//치명타 데미지

        vampire.text = "체력 흡혈 % : " + playerStats.vampire.ToString();//체력 흡혈 %
        if (playerStats.Shield >= 0)//강화에 따른 체력흡혈 UP 망토가 없으면 -1
            vampire.text += " + (" + ItemStats.Shield[playerStats.Shield] * 100 + ") %";

        goldBonus.text = "골드 보너스 : " + playerStats.goldBonus.ToString();//골드 보너스

        experienceBonus.text = "경험치 보너스 : " + playerStats.experienceBonus.ToString();//경험치 보너스

        experience.text = "보유 경험치 : " + (playerStats.experience).ToString();//보유 경험치

        gold.text = "보유 골드 : " + (playerStats.gold).ToString();//보유 골드

    }
}
