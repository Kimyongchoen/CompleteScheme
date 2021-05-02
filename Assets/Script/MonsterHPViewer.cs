using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;

public class MonsterHPViewer : MonoBehaviour
{
    private Monster monster;
    private Slider hpSlider;

    private int type = -1;
    private TextMeshProUGUI textPosition;//Text - 현재 위치 표시

    public void Setup(Monster monster,int type)
    {
        this.monster = monster;
        this.type = type;

        hpSlider = GetComponent<Slider>();
        textPosition = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        if (monster != null)
        {
            if (type == 0)
            {
                hpSlider.value = monster.CurrentHP / monster.MaxHP;
            }
            else if (type == 1)
            {
                textPosition.text = "( " + Math.Round(monster.transform.position.x, 2) + " / " + Math.Round(monster.transform.position.y, 2) + " )";
            }
            else if (type == 2)
            {
                textPosition.text = monster.monsterStats.name.ToString();
            }
        }
    }
}
