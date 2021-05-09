using System.Collections;
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
                TabBtnImage[i].sprite = SelectSprite[i]; 
            }
            else
            {
                Tab[i].SetActive(false);
                TabBtnImage[i].sprite = IdleSprite[i];
            }
        }
        if (n == 4)
        {
            Tab[4].SetActive(true);
        }
    }
}
