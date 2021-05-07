using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileTabManager : MonoBehaviour
{
    public Image TileInfomation;
    public Text TileInfomationText;
    public Image MonsterInfomation;
    public Text MonsterInfomationText;

    public void SetMonsterInfomation(int Number)
    {
        MonsterInfomationText.text = Number.ToString();
        //몬스터 넘버

        //공력력증가 넘버
        //방어력증가 넘버

        //회복 넘버


    }
    public void SetTileInfomationText(int Number)
    {
        //몬스터 넘버

        //공력력증가 넘버
        //방어력증가 넘버

        //회복 넘버


    }


}


