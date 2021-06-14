using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadShopInfo : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;//플래이어 정보

    [SerializeField]
    private ItemStats itemStats;//아이템 정보

    //플레이어정보
    [SerializeField]
    private Text Level;
    [SerializeField]
    private Text Gold;

    //아이템 정보
    [SerializeField]
    private Image HatImg;
    [SerializeField]
    private Text HatPlus;

    [SerializeField]
    private Image WeaponImg;
    [SerializeField]
    private Text WeaponPlus;

    [SerializeField]
    private Image ArmorImg;
    [SerializeField]
    private Text ArmorPlus;

    [SerializeField]
    private Image GlovesImg;
    [SerializeField]
    private Text GlovesPlus;

    [SerializeField]
    private Image ShieldImg;
    [SerializeField]
    private Text ShieldPlus;

    [SerializeField]
    private Image BootsImg;
    [SerializeField]
    private Text BootsPlus;


    //타일 구매
    [SerializeField]
    private Text RecoveryCnt;
    [SerializeField]
    private Text RecoveryGold;

    [SerializeField]
    private Text AttackDamageUpCnt;
    [SerializeField]
    private Text AttackDamageUpGold;

    [SerializeField]
    private Text DefenseUpCnt;
    [SerializeField]
    private Text DefenseUpGold;

    [SerializeField]
    private Text MonsterCnt;
    [SerializeField]
    private Text MonsterGold;

    // Start is called before the first frame update
    public void Awake()
    {
        LoadInfo();
    }

    public void LoadInfo()
    {
        Level.text = "Level " + playerStats.stats[0].level.ToString();
        Gold.text = "Gold " + playerStats.stats[0].gold.ToString();

        //투구
        if (playerStats.stats[0].Hat >= 0)
        {
            Color color = HatImg.color;
            color.a = 1;
            HatImg.color = color;
            if (playerStats.stats[0].Hat > 0)
            {
                HatPlus.text = "+" + playerStats.stats[0].Hat.ToString();
            }
        }
        else
        {
            Color color = HatImg.color;
            color.a = 0.4f;
            HatImg.color = color;
            HatPlus.text = "";
        }

        //무기
        if (playerStats.stats[0].Weapon >= 0)
        {
            Color color = WeaponImg.color;
            color.a = 1;
            WeaponImg.color = color;
            if (playerStats.stats[0].Weapon > 0)
            {
                WeaponPlus.text = "+" + playerStats.stats[0].Weapon.ToString();
            }
        }
        else
        {
            Color color = WeaponImg.color;
            color.a = 0.4f;
            WeaponImg.color = color;
            WeaponPlus.text = "";
        }

        //갑옷
        if (playerStats.stats[0].Armor >= 0)
        {
            Color color = ArmorImg.color;
            color.a = 1;
            ArmorImg.color = color;
            if (playerStats.stats[0].Armor > 0)
            {
                ArmorPlus.text = "+" + playerStats.stats[0].Armor.ToString();
            }
        }
        else
        {
            Color color = ArmorImg.color;
            color.a = 0.4f;
            ArmorImg.color = color;
            ArmorPlus.text = "";
        }

        //장갑
        if (playerStats.stats[0].Gloves >= 0)
        {
            Color color = GlovesImg.color;
            color.a = 1;
            GlovesImg.color = color;
            if (playerStats.stats[0].Gloves > 0)
            {
                GlovesPlus.text = "+" + playerStats.stats[0].Gloves.ToString();
            }
        }
        else
        {
            Color color = GlovesImg.color;
            color.a = 0.4f;
            GlovesImg.color = color;
            GlovesPlus.text = "";
        }

        //방패
        if (playerStats.stats[0].Shield >= 0)
        {
            Color color = ShieldImg.color;
            color.a = 1;
            ShieldImg.color = color;
            if (playerStats.stats[0].Shield > 0)
            {
                ShieldPlus.text = "+" + playerStats.stats[0].Shield.ToString();
            }
        }
        else
        {
            Color color = ShieldImg.color;
            color.a = 0.4f;
            ShieldImg.color = color;
            ShieldPlus.text = "";
        }

        //신발
        if (playerStats.stats[0].Boots >= 0)
        {
            Color color = BootsImg.color;
            color.a = 1;
            BootsImg.color = color;
            if (playerStats.stats[0].Boots > 0)
            {
                BootsPlus.text = "+" + playerStats.stats[0].Boots.ToString();
            }
        }
        else
        {
            Color color = BootsImg.color;
            color.a = 0.4f;
            BootsImg.color = color;
            BootsPlus.text = "";
        }


        RecoveryCnt.text = itemStats.RecoveryCnt+"/2";
        RecoveryGold.text = itemStats.RecoveryGold.ToString() + " 원";

        AttackDamageUpCnt.text = itemStats.AttackDamageUpCnt + "/2";
        AttackDamageUpGold.text = itemStats.AttackDamageUpGold.ToString() + " 원";

        DefenseUpCnt.text = itemStats.DefenseUpCnt + "/2";
        DefenseUpGold.text = itemStats.DefenseUpGold.ToString() + " 원";

        MonsterCnt.text = itemStats.MonsterCnt + "/99";
        MonsterGold.text = itemStats.MonsterGold.ToString() + " 원";

    }

}
