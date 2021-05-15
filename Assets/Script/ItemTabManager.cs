using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTabManager : MonoBehaviour
{
    public Text WeaponName;
    public Text WeaponStats;
    public Image WeaponImage;
    
    public Text ArmorName;
    public Text ArmorStats;
    public Image ArmorImage;
    
    public Text HatName;
    public Text HatStats;
    public Image HatImage;
    
    public Text GlovesName;
    public Text GlovesStats;
    public Image GlovesImage;
    
    public Text BootsName;
    public Text BootsStats;
    public Image BootsImage;
    
    public Text CloakName;
    public Text CloakStats;
    public Image CloakImage;

    public PlayerStats playerStats_temp;
    public ItemStats ItemStats;
    private PlayerStats.Stats playerStats;

    private void Awake()
    {
        playerStats = playerStats_temp.stats[0];
        ChangeItemInfomation();
    }

    public void ChangeItemInfomation()
    {
        WeaponName.text = "+0 무기";
        WeaponStats.text = "공격력 + 0";

        if (playerStats.weapon >= 0)//무기 강화에 따른 데미지 UP 무기가 없으면 -1
        {
            WeaponName.text = "+" + playerStats.weapon + " 무기";
            WeaponStats.text = "공격력 + " + ItemStats.weapon[playerStats.weapon];
        }

        ArmorName.text = "+0 갑옷";
        ArmorStats.text = "방어력 +0";

        if (playerStats.Armor>= 0)//갑옷 강화에 따른 방어력 UP 갑옷이 없으면 -1
        {
            ArmorName.text = "+" + playerStats.Armor + " 갑옷";
            ArmorStats.text = "방어력 + " + ItemStats.Armor[playerStats.Armor];
        }

        HatName.text = "+0 투구";
        HatStats.text = "체력 +0";

        if (playerStats.hat >= 0)//투구 강화에 따른 체력 UP 투구가 없으면 -1
        {
            HatName.text = "+" + playerStats.hat + " 투구";
            HatStats.text = "체력 + " + ItemStats.hat[playerStats.hat];
        }

        GlovesName.text = "+0 장갑";
        GlovesStats.text = "치명타 확율 +0";

        if (playerStats.Gloves >= 0)//장갑 강화에 따른 치명타 확율 UP 장갑이 없으면 -1
        {
            GlovesName.text = "+" + playerStats.Gloves + " 장갑";
            GlovesStats.text = "치명타 확율 + " + ItemStats.Gloves[playerStats.Gloves]+"%";
        }

        BootsName.text = "+0 신발";
        BootsStats.text = "회피력 +0";

        if (playerStats.Boots >= 0)//신발 강화에 따른 회피력 UP 신발이 없으면 -1
        {
            BootsName.text = "+" + playerStats.Boots + " 신발";
            BootsStats.text = "회피력 + " + ItemStats.Boots[playerStats.Boots];
        }

        CloakName.text = "+0 방패";
        CloakStats.text = "체력 흡혈 +0";

        if (playerStats.Cloak >= 0)//장갑 강화에 따른 치명타 확율 UP 장갑이 없으면 -1
        {
            CloakName.text = "+" + playerStats.Cloak + " 방패";
            CloakStats.text = "체력흡혈 + " + ItemStats.Cloak[playerStats.Cloak]*100+"%";
        }
    }
}

