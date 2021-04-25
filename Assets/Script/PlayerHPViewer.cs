using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHPViewer : MonoBehaviour
{
    private Player player;
    private Slider hpSlider;
    private int type = -1;
    private TextMeshProUGUI textPosition;//Text - 현재 위치 표시

    public void Setup(Player player, int type)
    {
        this.player = player;
        this.type = type;

        hpSlider = GetComponent<Slider>();
        textPosition = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        if (player != null)
        {
            if (type == 0)
            {
                hpSlider.value = player.CurrentHP / player.MaxHP;
            }
            else if (type == 1)
            {
                textPosition.SetText("( " + Math.Round(player.transform.position.x, 2) + " / " + Math.Round(player.transform.position.y, 2) + " )");
            }
        }
    }

}
