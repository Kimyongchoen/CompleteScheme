﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{

    public GameObject[] Tab;
    public Image[] TabBtnImage;
    public Sprite[] IdleSprite, SelectSprite;

    //void Start() => TabClick(1);

    public void TabClick(int n)
    {
        for (int i =0; i < 4; i++)
        {
            if (i == n)
            {
                Tab[4].SetActive(false);
                Tab[i].SetActive(true);
                Color color = TabBtnImage[i].color;
                color.r = 0.7137255f;
                color.g = 0.4509804f;
                color.b = 0.1764706f;
                TabBtnImage[i].color = color;
                //TabBtnImage[i].sprite = SelectSprite[i]; 
            }
            else
            {
                Tab[i].SetActive(false);
                Color color = TabBtnImage[i].color;
                color.r = 0.4f;
                color.g = 0.3f;
                color.b = 0.2f;
                TabBtnImage[i].color = color;
                Debug.Log(color.r + " / " + color.g + " / " + color.b);
                //TabBtnImage[i].sprite = IdleSprite[i];
            }
        }
        if (n == 4)
        {
            Tab[4].SetActive(true);
        }
    }
}
