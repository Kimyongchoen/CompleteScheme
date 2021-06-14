using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadStatsInfo : MonoBehaviour
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
    public Text gold;

    public PlayerStats playerStats_temp;
    public ItemStats ItemStats;
    private PlayerStats.Stats playerStats;

    private Sprite sprite_temp;

    [SerializeField]
    private Stage10 Stage10;//스테이지 정보

    public Text BonusStat; //남은 스텟

    //적용한 능력치
    public Text attackDamageMax_Stat; //공격력(max)
    public Text Maxhealth_Stat;//체력
    public Text defense_Stat;//방어력
    public Text attackSpeed_Stat; //공격속도
    public Text hit_Stat;//명중치
    public Text evasion_Stat;//회피치
    public Text criticalChance_Stat;//치명타 확률(0-100)
    public Text experienceBonus_Stat;//경험치 획득 %
    public Text goldBonus_Stat;//골드 획득 %

    //요구량
    public Text attackDamageMax_Up; //공격력(max)
    public Text Maxhealth_Up;//체력
    public Text defense_Up;//방어력
    public Text attackSpeed_Up; //공격속도
    public Text hit_Up;//명중치
    public Text evasion_Up;//회피치
    public Text criticalChance_Up;//치명타 확률(0-100)
    public Text experienceBonus_Up;//경험치 획득 %
    public Text goldBonus_Up;//골드 획득 %

    //추가능력치
    public Text attackDamageMax_Plus; //공격력(max)
    public Text Maxhealth_Plus;//체력
    public Text defense_Plus;//방어력
    public Text attackSpeed_Plus; //공격속도
    public Text hit_Plus;//명중치
    public Text evasion_Plus;//회피치
    public Text criticalChance_Plus;//치명타 확률(0-100)
    public Text experienceBonus_Plus;//경험치 획득 %
    public Text goldBonus_Plus;//골드 획득 %

    //적용할 능력치
    public Text attackDamageMax_Apply; //공격력(max)
    public Text Maxhealth_Apply;//체력
    public Text defense_Apply;//방어력
    public Text attackSpeed_Apply; //공격속도
    public Text hit_Apply;//명중치
    public Text evasion_Apply;//회피치
    public Text criticalChance_Apply;//치명타 확률(0-100)
    public Text experienceBonus_Apply;//경험치 획득 %
    public Text goldBonus_Apply;//골드 획득 %

    //Up 버튼
    public GameObject attackDamageMax_UpBtn; //공격력(max)
    public GameObject Maxhealth_UpBtn;//체력
    public GameObject defense_UpBtn;//방어력
    public GameObject attackSpeed_UpBtn; //공격속도
    public GameObject hit_UpBtn;//명중치
    public GameObject evasion_UpBtn;//회피치
    public GameObject criticalChance_UpBtn;//치명타 확률(0-100)
    public GameObject experienceBonus_UpBtn;//경험치 획득 %
    public GameObject goldBonus_UpBtn;//골드 획득 %

    //Down 버튼
    public GameObject attackDamageMax_DownBtn; //공격력(max)
    public GameObject Maxhealth_DownBtn;//체력
    public GameObject defense_DownBtn;//방어력
    public GameObject attackSpeed_DownBtn; //공격속도
    public GameObject hit_DownBtn;//명중치
    public GameObject evasion_DownBtn;//회피치
    public GameObject criticalChance_DownBtn;//치명타 확률(0-100)
    public GameObject experienceBonus_DownBtn;//경험치 획득 %
    public GameObject goldBonus_DownBtn;//골드 획득 %

    [SerializeField]
    private PlayerStat playerStat;//스텟 정보

    //메시지 박스
    [SerializeField]
    private TextMeshProUGUI MainMessageBox;

    private int BonusStatPoint=0;

    private void Awake()
    {
        playerStats = playerStats_temp.stats[0];
        StatsInfo();//처음 세팅 기사
    }

    public void StatsInfo()
    {
        int AttackDamagePlus = 0;
        int defensePlus = 0;

        Playername.text = "플레이어 이름 : " + playerStats.Playername; // 플레이어 이름

        level.text = "Level : " + playerStats.level; // 레벨

        attackDamage.text = "공력력 : " 
            + (playerStats.attackDamageMin + (playerStat.attackDamageMax * playerStat.attackDamageMaxUp))
            + " - " 
            + (playerStats.attackDamageMax + (playerStat.attackDamageMin * playerStat.attackDamageMinUp)); //공격력
        if (ItemStats.AttackDamageUp > 0)//공격력 버프 증가시
            AttackDamagePlus += ItemStats.AttackDamageUp;
        if (playerStats.Weapon >= 0)//무기 강화에 따른 데미지 UP 무기가 없으면 -1
            AttackDamagePlus += ItemStats.Weapon[playerStats.Weapon];
        if (AttackDamagePlus > 0)
            attackDamage.text += " + (" + AttackDamagePlus + ")";

        health.text = "체력  : " + (playerStats.health + (playerStat.Maxhealth * playerStat.MaxhealthUp));//체력
        
        if (playerStats.Hat >= 0)//투구 강화에 따른 체력 UP 투구가 없으면 -1
        {
            health.text += "+(" + ItemStats.Hat[playerStats.Hat] + ")";//체력
        }

        defense.text = "방어력 : " + (playerStats.defense + (playerStat.defense * playerStat.defenseUp));//방어력
        if (ItemStats.DefenseUp > 0)//방어력 버프 증가시
            defensePlus += ItemStats.DefenseUp;
        if (playerStats.Armor >= 0)//갑옷 강화에 따른 방어력 UP 갑옷이 없으면 -1
            defensePlus += ItemStats.Armor[playerStats.Armor];
        if (defensePlus > 0)
            defense.text += " + (" + defensePlus + ")";

        attackSpeed.text = "공격속도 : " + (playerStats.attackSpeed + (playerStat.attackSpeed * playerStat.attackSpeedUp)); //공격속도

        attackRange.text = "공격범위 : " + playerStats.attackRange; //공격범위

        hit.text = "명중치 : " + (playerStats.hit+(playerStat.hit * playerStat.hitUp));//명중치

        evasion.text = "회피치 : " + (playerStats.evasion + (playerStat.evasion * playerStat.evasionUp));//회피치
        if (playerStats.Boots >= 0)//강화에 따른 회피 UP 부츠가 없으면 -1
            evasion.text += " + (" + ItemStats.Boots[playerStats.Boots] + ")";

        criticalChance.text = "치명타 확률 % : " + (playerStats.criticalChance + (playerStat.criticalChance * playerStat.criticalChanceUp));//치명타 확률
        if (playerStats.Gloves >= 0)//강화에 따른 치확 UP 장갑이 없으면 -1
            criticalChance.text += " + (" + ItemStats.Gloves[playerStats.Gloves] + ") %";

        criticalDamagema.text = "치명타 데미지 : " + playerStats.criticalDamagema;//치명타 데미지

        vampire.text = "체력 흡혈 % : " + playerStats.vampire.ToString();//체력 흡혈 %
        if (playerStats.Shield >= 0)//강화에 따른 체력흡혈 UP 망토가 없으면 -1
            vampire.text += " + (" + ItemStats.Shield[playerStats.Shield] * 100 + ") %";

        goldBonus.text = "골드 보너스 : " + (playerStats.goldBonus+(playerStat.goldBonus * playerStat.goldBonusUp));//골드 보너스

        experienceBonus.text = "경험치 보너스 : " + (playerStats.experienceBonus+(playerStat.experienceBonus * playerStat.experienceBonusUp));//경험치 보너스

        gold.text = "보유 골드 : " + (playerStats.gold);//보유 골드

        Color color = PlayerImage.color;
        color.a = 1.0f;
        PlayerImage.color = color;

        sprite_temp = playerStats.PlayerPrefab.GetComponentInChildren<Image>().sprite;

        PlayerImage.sprite = sprite_temp;

        //플레이어 Stat Load
        //포인트
        BonusStat.text = playerStat.BonusStat.ToString(); //공격력(max)
        BonusStatPoint = playerStat.BonusStat;

        //적용한 능력치
        attackDamageMax_Stat.text = playerStat.attackDamageMax.ToString(); //공격력(max)
        Maxhealth_Stat.text = playerStat.Maxhealth.ToString();//체력
        defense_Stat.text = playerStat.defense.ToString();//방어력
        attackSpeed_Stat.text = playerStat.attackSpeed.ToString(); //공격속도
        hit_Stat.text = playerStat.hit.ToString();//명중치
        evasion_Stat.text = playerStat.evasion.ToString();//회피치
        criticalChance_Stat.text = playerStat.criticalChance.ToString();//치명타 확률(0-100)
        experienceBonus_Stat.text = playerStat.experienceBonus.ToString();//경험치 획득 %
        goldBonus_Stat.text = playerStat.goldBonus.ToString();//골드 획득 %

        //추가능력치
        attackDamageMax_Plus.text = "+"+playerStat.attackDamageMaxUp.ToString(); //공격력(max)
        defense_Plus.text = "+" + playerStat.defenseUp.ToString();//체력
        Maxhealth_Plus.text = "+" + playerStat.MaxhealthUp.ToString();//방어력
        attackSpeed_Plus.text = "+" + playerStat.attackSpeedUp * -100 + "%"; //공격속도
        hit_Plus.text = "+" + playerStat.hitUp.ToString();//명중치
        evasion_Plus.text = "+" + playerStat.evasionUp.ToString();//회피치
        criticalChance_Plus.text = "+" + playerStat.criticalChanceUp + "%";//치명타 확률(0-100)
        experienceBonus_Plus.text = "+" + playerStat.experienceBonusUp + "%";//경험치 획득 %
        goldBonus_Plus.text = "+" + playerStat.goldBonusUp + "%";//골드 획득 %

        //요구량
        attackDamageMax_Up.text = GetUpStat(playerStat.attackDamageMax).ToString(); //공격력(max)
        Maxhealth_Up.text = GetUpStat(playerStat.Maxhealth).ToString();//체력
        defense_Up.text = GetUpStat(playerStat.defense).ToString();//방어력
        attackSpeed_Up.text = GetUpStat(playerStat.attackSpeed).ToString(); //공격속도
        hit_Up.text = GetUpStat(playerStat.hit).ToString();//명중치
        evasion_Up.text = GetUpStat(playerStat.evasion).ToString();//회피치
        criticalChance_Up.text = GetUpStat(playerStat.criticalChance).ToString();//치명타 확률(0-100)
        experienceBonus_Up.text = GetUpStat(playerStat.experienceBonus).ToString();//경험치 획득 %
        goldBonus_Up.text = GetUpStat(playerStat.goldBonus).ToString();//골드 획득 %

        //적용할 능력치
        attackDamageMax_Apply.text = "0"; //공격력(max)
        Maxhealth_Apply.text = "0";//체력
        defense_Apply.text = "0"; //방어력
        attackSpeed_Apply.text = "0";  //공격속도
        hit_Apply.text = "0"; //명중치
        evasion_Apply.text = "0"; //회피치
        criticalChance_Apply.text = "0"; //치명타 확률(0-100)
        experienceBonus_Apply.text = "0"; //경험치 획득 %
        goldBonus_Apply.text = "0"; //골드 획득 %


        //- 버튼 활성화 & 비활성화
        DownBtnOnOff();

        //+버튼 활성화 & 비활성화
        UpBtnOnOff();
    }

    private float upBtnLogic(string str,string str1)
    {
        float Stat = 0 ;
        int i = 0;
        int j = 0;
        
        if (int.TryParse(str, out i) && int.TryParse(str1, out j))
        {
            Stat = i + j;
        }

        return GetUpStat(Stat);
    }
    public void UpBtn(int btnCnt)
    {
        // 적용 포인트 **_Stat / 적용할 능력치 **_Apply / 요구량 **_Up
        
        if (BonusStatPoint <= 0) return;//보너스 포인트가 없다면

        switch (btnCnt)
        {
            case 1: //공격력(max) attackDamageMax

                //다운버튼 활성화
                attackDamageMax_DownBtn.SetActive(true);

                // 남은 포인트에서 요구량 만큼 제외하기
                BonusStatPoint -= (int)ChangeFloat(attackDamageMax_Up.text);
                
                //적용할 능력치 +1
                attackDamageMax_Apply.text = StringPlusInt(attackDamageMax_Apply.text , 1).ToString();
                
                // 요구량 구하기
                attackDamageMax_Up.text = upBtnLogic(attackDamageMax_Stat.text, attackDamageMax_Apply.text).ToString();

                // 요구량 이 남은 포인트보다 크다면 +버튼 비활성화
                if (BonusStatPoint < upBtnLogic(attackDamageMax_Stat.text, attackDamageMax_Apply.text))
                    attackDamageMax_UpBtn.SetActive(false);

                break;

            case 2: //방어력 defense
                defense_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(defense_Up.text);
                defense_Apply.text = StringPlusInt(defense_Apply.text, 1).ToString();
                defense_Up.text = upBtnLogic(defense_Stat.text, defense_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(defense_Stat.text, defense_Apply.text)) defense_UpBtn.SetActive(false);
                break;
            case 3: //체력 Maxhealth
                Maxhealth_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(Maxhealth_Up.text);
                Maxhealth_Apply.text = StringPlusInt(Maxhealth_Apply.text, 1).ToString();
                Maxhealth_Up.text = upBtnLogic(Maxhealth_Stat.text, Maxhealth_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(Maxhealth_Stat.text, Maxhealth_Apply.text)) Maxhealth_UpBtn.SetActive(false);
                break;
            case 4: //공격속도 attackSpeed
                attackSpeed_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(attackSpeed_Up.text);
                attackSpeed_Apply.text = StringPlusInt(attackSpeed_Apply.text, 1).ToString();
                attackSpeed_Up.text = upBtnLogic(attackSpeed_Stat.text, attackSpeed_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(attackSpeed_Stat.text, attackSpeed_Apply.text)) attackSpeed_UpBtn.SetActive(false);
                break;
            case 5: //명중치 hit
                hit_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(hit_Up.text);
                hit_Apply.text = StringPlusInt(hit_Apply.text, 1).ToString();
                hit_Up.text = upBtnLogic(hit_Stat.text, hit_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(hit_Stat.text, hit_Apply.text)) hit_UpBtn.SetActive(false);
                break;
            case 6: //회피치 evasion
                evasion_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(evasion_Up.text);
                evasion_Apply.text = StringPlusInt(evasion_Apply.text, 1).ToString();
                evasion_Up.text = upBtnLogic(evasion_Stat.text, evasion_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(evasion_Stat.text, evasion_Apply.text)) evasion_UpBtn.SetActive(false);
                break;
            case 7: //치명타 확률(0-100) criticalChance
                criticalChance_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(criticalChance_Up.text);
                criticalChance_Apply.text = StringPlusInt(criticalChance_Apply.text, 1).ToString();
                criticalChance_Up.text = upBtnLogic(criticalChance_Stat.text, criticalChance_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(criticalChance_Stat.text, criticalChance_Apply.text)) criticalChance_UpBtn.SetActive(false);
                break;
            case 8: //경험치 획득 % experienceBonus
                experienceBonus_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(experienceBonus_Up.text);
                experienceBonus_Apply.text = StringPlusInt(experienceBonus_Apply.text, 1).ToString();
                experienceBonus_Up.text = upBtnLogic(experienceBonus_Stat.text, experienceBonus_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(experienceBonus_Stat.text, experienceBonus_Apply.text)) experienceBonus_UpBtn.SetActive(false);
                break;
            case 9: //골드 획득 % goldBonus
                goldBonus_DownBtn.SetActive(true);
                BonusStatPoint -= (int)ChangeFloat(goldBonus_Up.text);
                goldBonus_Apply.text = StringPlusInt(goldBonus_Apply.text, 1).ToString();
                goldBonus_Up.text = upBtnLogic(goldBonus_Stat.text, goldBonus_Apply.text).ToString();
                if (BonusStatPoint < upBtnLogic(goldBonus_Stat.text, goldBonus_Apply.text)) goldBonus_UpBtn.SetActive(false);
                break;

            default:
                break;
    
        }

        BonusStat.text = BonusStatPoint.ToString(); //남은 포인트

        //버튼 활성화 & 비활성화
        UpBtnOnOff();
        DownBtnOnOff();
    }

    public void DownBtn(int btnCnt)
    {
        // 적용 포인트 **_Stat / 적용할 능력치 **_Apply / 요구량 **_Up
        int i = 0;

        switch (btnCnt)
        {
            case 1: //공격력(max)attackDamageMax

                /*
                // 남은 포인트에서 요구량 만큼 제외하기
                BonusStatPoint -= (int)ChangeFloat(attackDamageMax_Up.text);

                //적용할 능력치 +1
                attackDamageMax_Apply.text = StringPlusInt(attackDamageMax_Apply.text, 1).ToString();
                */


                int.TryParse(attackDamageMax_Apply.text, out i);
                if (i <= 0) return;

                // 남은 포인트에서 요구량 만큼 + 하기
                if ( (int)ChangeFloat(attackDamageMax_Stat.text)+ (int)ChangeFloat(attackDamageMax_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(attackDamageMax_Stat.text) + (int)ChangeFloat(attackDamageMax_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                
                //적용할 능력치 -1
                attackDamageMax_Apply.text = StringPlusInt(attackDamageMax_Apply.text, -1).ToString();

                // 요구량 구하기
                attackDamageMax_Up.text = upBtnLogic(attackDamageMax_Stat.text, attackDamageMax_Apply.text).ToString();

                if (attackDamageMax_Apply.text=="0") attackDamageMax_DownBtn.SetActive(false);

                break;
            case 2: //방어력
                int.TryParse(defense_Apply.text, out i);
                if (i <= 0) return;

                if ((int)ChangeFloat(defense_Stat.text) + (int)ChangeFloat(defense_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(defense_Stat.text) + (int)ChangeFloat(defense_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;

                defense_Apply.text = StringPlusInt(defense_Apply.text, -1).ToString();

                defense_Up.text = upBtnLogic(defense_Stat.text, defense_Apply.text).ToString();

                if (defense_Apply.text == "0") defense_DownBtn.SetActive(false);
                break;
            case 3: //체력
                int.TryParse(Maxhealth_Apply.text, out i);
                if (i <= 0) return;
                if ((int)ChangeFloat(Maxhealth_Stat.text) + (int)ChangeFloat(Maxhealth_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(Maxhealth_Stat.text) + (int)ChangeFloat(Maxhealth_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                Maxhealth_Apply.text = StringPlusInt(Maxhealth_Apply.text, -1).ToString();
                Maxhealth_Up.text = upBtnLogic(Maxhealth_Stat.text, Maxhealth_Apply.text).ToString();
                if (Maxhealth_Apply.text == "0") Maxhealth_DownBtn.SetActive(false);
                break;
            case 4: //공격속도
                int.TryParse(attackSpeed_Apply.text, out i);
                if (i <= 0) return;
                if ((int)ChangeFloat(attackSpeed_Stat.text) + (int)ChangeFloat(attackSpeed_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(attackSpeed_Stat.text) + (int)ChangeFloat(attackSpeed_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                attackSpeed_Apply.text = StringPlusInt(attackSpeed_Apply.text, -1).ToString();
                attackSpeed_Up.text = upBtnLogic(attackSpeed_Stat.text, attackSpeed_Apply.text).ToString();
                if (attackSpeed_Apply.text == "0") attackSpeed_DownBtn.SetActive(false);
                break;
            case 5: //명중치
                int.TryParse(hit_Apply.text, out i);
                if (i <= 0) return;
                if ((int)ChangeFloat(hit_Stat.text) + (int)ChangeFloat(hit_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(hit_Stat.text) + (int)ChangeFloat(hit_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                hit_Apply.text = StringPlusInt(hit_Apply.text, -1).ToString();
                hit_Up.text = upBtnLogic(hit_Stat.text, hit_Apply.text).ToString();
                if (hit_Apply.text == "0") hit_DownBtn.SetActive(false);
                break;
            case 6: //회피치
                int.TryParse(evasion_Apply.text, out i);
                if (i <= 0) return;
                if ((int)ChangeFloat(evasion_Stat.text) + (int)ChangeFloat(evasion_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(evasion_Stat.text) + (int)ChangeFloat(evasion_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                evasion_Apply.text = StringPlusInt(evasion_Apply.text, -1).ToString();
                evasion_Up.text = upBtnLogic(evasion_Stat.text, evasion_Apply.text).ToString();
                if (evasion_Apply.text == "0") evasion_DownBtn.SetActive(false);
                break;
            case 7: //치명타 확률(0-100)
                int.TryParse(criticalChance_Apply.text, out i);
                if (i <= 0) return;
                if ((int)ChangeFloat(criticalChance_Stat.text) + (int)ChangeFloat(criticalChance_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(criticalChance_Stat.text) + (int)ChangeFloat(criticalChance_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                criticalChance_Apply.text = StringPlusInt(criticalChance_Apply.text, -1).ToString();
                criticalChance_Up.text = upBtnLogic(criticalChance_Stat.text, criticalChance_Apply.text).ToString();
                if (criticalChance_Apply.text == "0") criticalChance_DownBtn.SetActive(false);
                break;
            case 8: //경험치 획득 %
                int.TryParse(experienceBonus_Apply.text, out i);
                if (i <= 0) return;
                if ((int)ChangeFloat(experienceBonus_Stat.text) + (int)ChangeFloat(experienceBonus_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(experienceBonus_Stat.text) + (int)ChangeFloat(experienceBonus_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                experienceBonus_Apply.text = StringPlusInt(experienceBonus_Apply.text, -1).ToString();
                experienceBonus_Up.text = upBtnLogic(experienceBonus_Stat.text, experienceBonus_Apply.text).ToString();
                if (experienceBonus_Apply.text == "0") experienceBonus_DownBtn.SetActive(false);
                break;
            case 9: //골드 획득 %
                int.TryParse(goldBonus_Apply.text, out i);
                if (i <= 0) return;
                if ((int)ChangeFloat(goldBonus_Stat.text) + (int)ChangeFloat(goldBonus_Apply.text) >= 11)
                    BonusStatPoint += 4;
                else if ((int)ChangeFloat(goldBonus_Stat.text) + (int)ChangeFloat(goldBonus_Apply.text) >= 6)
                    BonusStatPoint += 2;
                else
                    BonusStatPoint += 1;
                goldBonus_Apply.text = StringPlusInt(goldBonus_Apply.text, -1).ToString();
                goldBonus_Up.text = upBtnLogic(goldBonus_Stat.text, goldBonus_Apply.text).ToString();
                if (goldBonus_Apply.text == "0") goldBonus_DownBtn.SetActive(false);
                break;

            default:
                break;

        }

        BonusStat.text = BonusStatPoint.ToString(); //남은 포인트

        //버튼 활성화 & 비활성화
        UpBtnOnOff();
        DownBtnOnOff();

    }
    public void PointSubmit()
    {
        bool flag = false;

        //공격력(max)
        if (attackDamageMax_Apply.text != "0")
        {
            playerStat.attackDamageMax += (int)ChangeFloat(attackDamageMax_Apply.text);
            playerStat.attackDamageMin += (int)ChangeFloat(attackDamageMax_Apply.text);
            flag = true;
        }

        //체력
        if (Maxhealth_Apply.text != "0")
        {
            playerStat.Maxhealth += (int)ChangeFloat(Maxhealth_Apply.text);
            flag = true;
        }

        //방어력
        if (defense_Apply.text != "0")
        {
            playerStat.defense += (int)ChangeFloat(defense_Apply.text);
            flag = true;
        }

        //공격속도
        if (attackSpeed_Apply.text != "0")
        {
            playerStat.attackSpeed += (int)ChangeFloat(attackSpeed_Apply.text);
            flag = true;
        }

        //명중치
        if (hit_Apply.text != "0")
        {
            playerStat.hit += (int)ChangeFloat(hit_Apply.text);
            flag = true;
        }

        //회피치
        if (evasion_Apply.text != "0")
        {
            playerStat.evasion += (int)ChangeFloat(evasion_Apply.text);
            flag = true;
        }

        //치명타 확률(0-100)
        if (criticalChance_Apply.text != "0")
        {
            playerStat.criticalChance += (int)ChangeFloat(criticalChance_Apply.text);
            flag = true;
        }

        //경험치 획득 %
        if (experienceBonus_Apply.text != "0")
        {
            playerStat.experienceBonus += (int)ChangeFloat(experienceBonus_Apply.text);
            flag = true;
        }

        //골드 획득 %
        if (goldBonus_Apply.text != "0")
        {
            playerStat.goldBonus += (int)ChangeFloat(goldBonus_Apply.text);
            flag = true;
        }

        if (playerStat.BonusStat > 0)
        {
            if (flag)
            {
                SetMainMessageBox("적용되었습니다");
            }
            else
            {
                SetMainMessageBox("선택한 능력치가 없습니다");
            }
        }
        else
        {
            SetMainMessageBox("남은 보너스 포인트가 없습니다");
        }

        //남은 포인트 저장
        playerStat.BonusStat = BonusStatPoint;

        //화면 리로드
        StatsInfo();

    }
    public void PointReset()
    {
        int resetPoint = 0;

        //공격력(max)
        resetPoint += ResetLogic(playerStat.attackDamageMax);

        playerStat.attackDamageMax = 0;
        playerStat.attackDamageMin = 0;

        //체력
        resetPoint += ResetLogic(playerStat.Maxhealth);
        playerStat.Maxhealth = 0;

        //방어력
        resetPoint += ResetLogic(playerStat.defense);
        playerStat.defense = 0;

        //공격속도
        resetPoint += ResetLogic(playerStat.attackSpeed);
        playerStat.attackSpeed = 0;

        //명중치
        resetPoint += ResetLogic(playerStat.hit);
        playerStat.hit = 0;

        //회피치
        resetPoint += ResetLogic(playerStat.evasion);
        playerStat.evasion = 0;

        //치명타 확률(0-100)
        resetPoint += ResetLogic(playerStat.criticalChance);
        playerStat.criticalChance = 0;

        //경험치 획득 %
        resetPoint += ResetLogic(playerStat.experienceBonus);
        playerStat.experienceBonus = 0;

        //골드 획득 %
        resetPoint += ResetLogic(playerStat.goldBonus);
        playerStat.goldBonus = 0;

        SetMainMessageBox("초기화 되었습니다");

        //남은 포인트 저장
        playerStat.BonusStat += resetPoint;

        //화면 리로드
        StatsInfo();

    }

    private int GetUpStat(float Stat)
    {
        if (Stat >= 10)
            return 4;
        else if (Stat >= 5)
            return 2;
        else
            return 1;
    }
    private int StringPlusInt(string str, int i)
    {
        int j=0;

        if (int.TryParse(str, out j))
        {
            return (i + j);
        }
        else
        {
            return j;
        }
    }


    private void UpBtnOnOff()
    {
        attackDamageMax_UpBtn.SetActive(true); //공격력(max)
        Maxhealth_UpBtn.SetActive(true); //체력
        defense_UpBtn.SetActive(true); //방어력
        attackSpeed_UpBtn.SetActive(true);  //공격속도
        hit_UpBtn.SetActive(true); //명중치
        evasion_UpBtn.SetActive(true); //회피치
        criticalChance_UpBtn.SetActive(true); //치명타 확률(0-100)
        experienceBonus_UpBtn.SetActive(true); //경험치 획득 %
        goldBonus_UpBtn.SetActive(true); //골드 획득 %

        if (BonusStatPoint < upBtnLogic(attackDamageMax_Stat.text, attackDamageMax_Apply.text))
            attackDamageMax_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(Maxhealth_Stat.text, Maxhealth_Apply.text))
            Maxhealth_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(defense_Stat.text, defense_Apply.text))
            defense_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(attackSpeed_Stat.text, attackSpeed_Apply.text))
            attackSpeed_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(hit_Stat.text, hit_Apply.text))
            hit_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(evasion_Stat.text, evasion_Apply.text))
            evasion_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(criticalChance_Stat.text, criticalChance_Apply.text))
            criticalChance_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(experienceBonus_Stat.text, experienceBonus_Apply.text))
            experienceBonus_UpBtn.SetActive(false);
        if (BonusStatPoint < upBtnLogic(goldBonus_Stat.text, goldBonus_Apply.text))
            goldBonus_UpBtn.SetActive(false);
    }

    private void DownBtnOnOff()
    {
        attackDamageMax_DownBtn.SetActive(false); //공격력(max)
        Maxhealth_DownBtn.SetActive(false); //체력
        defense_DownBtn.SetActive(false); //방어력
        attackSpeed_DownBtn.SetActive(false);  //공격속도
        hit_DownBtn.SetActive(false); //명중치
        evasion_DownBtn.SetActive(false); //회피치
        criticalChance_DownBtn.SetActive(false); //치명타 확률(0-100)
        experienceBonus_DownBtn.SetActive(false); //경험치 획득 %
        goldBonus_DownBtn.SetActive(false); //골드 획득 %

        if (ChangeFloat(attackDamageMax_Apply.text) > 0)
            attackDamageMax_DownBtn.SetActive(true);
        if (ChangeFloat(Maxhealth_Apply.text) > 0)
            Maxhealth_DownBtn.SetActive(true);
        if (ChangeFloat(defense_Apply.text) > 0)
            defense_DownBtn.SetActive(true);
        if (ChangeFloat(attackSpeed_Apply.text) > 0)
            attackSpeed_DownBtn.SetActive(true);
        if (ChangeFloat(hit_Apply.text) > 0)
            hit_DownBtn.SetActive(true);
        if (ChangeFloat(evasion_Apply.text) > 0)
            evasion_DownBtn.SetActive(true);
        if (ChangeFloat(criticalChance_Apply.text) > 0)
            criticalChance_DownBtn.SetActive(true);
        if (ChangeFloat(experienceBonus_Apply.text) > 0)
            experienceBonus_DownBtn.SetActive(true);
        if (ChangeFloat(goldBonus_Apply.text) > 0)
            goldBonus_DownBtn.SetActive(true);

    }

    private float ChangeFloat(string str)
    {
        float i = 0;

        float.TryParse(str, out i);

        return i;
    }

    private int ResetLogic(int stat)
    {
        int point = 0;

        while (stat > 0)
        {
            if (stat >= 11)
                point += 4;
            else if (stat >= 6)
                point += 2;
            else
                point += 1;

            stat--;
        }

        return point;
    }

    private void SetMainMessageBox(string msg)
    {
        MainMessageBox.text = msg;
        StartCoroutine(AlphaLerp(1, 0));

    }
    private IEnumerator AlphaLerp(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        float lerpTime = 1f;

        while (percent < 1)
        {
            //lerpTime 동안 While()반복문 실행
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            // Text - TextMeshPro의 폰트 투명도를 start에서 end로 변경
            Color color = MainMessageBox.color;
            color.a = Mathf.Lerp(start, end, percent);
            MainMessageBox.color = color;

            yield return null;
        }

    }

        
}

