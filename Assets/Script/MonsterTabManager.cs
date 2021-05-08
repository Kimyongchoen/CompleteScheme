using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTabManager : MonoBehaviour
{
    public Image MonsterImage;
    public Text MonsterName;
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
    public Text experience;
    public Text gold;
    public Text buff;
    public Text recovery;

    public MonsterStats monsterStats;

    private int number=0;
    private int MonsertNumber;

    public void SetMonsterInfomation(int Number)
    {
        this.MonsertNumber = Number; //몬스터Number (보통근거리(1), 강한근거리(2), 원거리(3), 버퍼(4), 회복(5), Stage 보스(6) ) 1201 (1=지역 2=type 01은 Number)

        //number로 stats 몇번째인지 검사
        for (int j = 0; j < monsterStats.stats.Length; j++)
        {
            if (monsterStats.stats[j].number == Number)
            {
                number = j;
            }
        }

        ChangeMonsterInfomation(number);
    }

    public void ChangeMonsterInfomationButton(bool LeftRight)
    {


        if (LeftRight)
        {
            number++;
        }
        else
        {
            number--;
        }

        if (number < 0)
            number = 0;
        if (number > monsterStats.stats.Length - 1)
            number = monsterStats.stats.Length - 1;
         
        ChangeMonsterInfomation(number);
    }

    public void ChangeMonsterInfomation(int number)
    {
        //몬스터 정보
        MonsterStats.Stats monsterstats = monsterStats.stats[number];

        Color color = MonsterImage.color;
        color.a = 1.0f;
        MonsterImage.color = color;

        MonsterImage.sprite = monsterstats.MonsterPrefab.GetComponentInChildren<Image>().sprite;// 몬스터 프리팹

        MonsterName.text = "몬스터 이름 : " + monsterstats.name;//몬스터 이름
        attackDamage.text = "공력력 : " + monsterstats.attackDamageMin.ToString() + " - " + monsterstats.attackDamageMax.ToString(); //공격력(min)
        health.text = "체력 : " + monsterstats.health.ToString();//체력
        defense.text = "방어력 : " + monsterstats.defense.ToString();//방어력
        attackSpeed.text = "공격속도 : " + monsterstats.attackSpeed.ToString(); //공격속도
        attackRange.text = "공격범위 : " + monsterstats.attackRange.ToString(); //공격범위
        hit.text = "명중치 : " + monsterstats.hit.ToString();//명중치
        evasion.text = "회피치 : " + monsterstats.evasion.ToString();//회피치
        criticalChance.text = "치명타 확률 : " + monsterstats.criticalChance.ToString();//치명타 확률
        criticalDamagema.text = "치명타 데미지 : " + monsterstats.criticalDamagema.ToString();//치명타 데미지
        vampire.text = "체력 흡혈 % : " + monsterstats.vampire.ToString();//체력 흡혈 %
        experience.text = "드랍 경험치 : " + monsterstats.experience.ToString();//드랍 경험치
        gold.text = "드랍 골드 : " + monsterstats.gold.ToString();//드랍 골드
        buff.text = "버프 : " + monsterstats.buff.ToString();//버프 공격력 추가 0.2 = 20%
        recovery.text = "힐 : " + monsterstats.recovery.ToString();//힐 추가 초당 +hp
    }
}
