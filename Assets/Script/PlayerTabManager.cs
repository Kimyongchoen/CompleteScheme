using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTabManager : MonoBehaviour
{
    public Image PlayerImage;

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

    public PlayerStats playerStats_temp;
    private PlayerStats.Stats playerStats;

    private Sprite sprite_temp;

    private void Awake()
    {
        playerStats = playerStats_temp.stats[0];
        ChangeMonsterInfomation(1);//처음 세팅
    }

    public void SetPlayerInfomation(int PlayerNum)
    {
        ChangeMonsterInfomation(PlayerNum);
    }

    public void ChangeMonsterInfomation(int PlayerNum)
    {
        Playername.text = "플레이어 이름 : " + playerStats.Playername; // 레벨
        level.text = "level : " + playerStats.level; // 레벨
        attackDamage.text = "공력력 : " + playerStats.attackDamageMin.ToString() + " - " + playerStats.attackDamageMax.ToString(); //공격력
        health.text = "체력 : " + playerStats.health.ToString();//체력
        defense.text = "방어력 : " + playerStats.defense.ToString();//방어력
        attackSpeed.text = "공격속도 : " + playerStats.attackSpeed.ToString(); //공격속도
        attackRange.text = "공격범위 : " + playerStats.attackRange.ToString(); //공격범위
        hit.text = "명중치 : " + playerStats.hit.ToString();//명중치
        evasion.text = "회피치 : " + playerStats.evasion.ToString();//회피치
        criticalChance.text = "치명타 확률 : " + playerStats.criticalChance.ToString();//치명타 확률
        criticalDamagema.text = "치명타 데미지 : " + playerStats.criticalDamagema.ToString();//치명타 데미지
        vampire.text = "체력 흡혈 % : " + playerStats.vampire.ToString();//체력 흡혈 %
        goldBonus.text = "골드 보너스 : " + playerStats.goldBonus.ToString();//골드 보너스
        experienceBonus.text = "경험치 보너스 : " + playerStats.experienceBonus.ToString();//경험치 보너스
        experience.text = "보유 경험치 : " + playerStats.experience.ToString();//보유 경험치
        gold.text = "보유 골드 : " + playerStats.gold.ToString();//보유 골드
        
        Color color = PlayerImage.color;
        color.a = 1.0f;
        PlayerImage.color = color;
     
        sprite_temp = playerStats.PlayerPrefab.GetComponentInChildren<Image>().sprite;

        PlayerImage.sprite = sprite_temp;
    }
}
