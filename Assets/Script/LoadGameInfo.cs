using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameInfo : MonoBehaviour
{
    [SerializeField]
    private Stage10 stage10;//스테이지 정보

    [SerializeField]
    private PlayerStats playerStats;//스테이지 정보

    [SerializeField]
    private Text Level;
    [SerializeField]
    private Text Gold;
    
    [SerializeField]
    private Text Stage1Exp;
    [SerializeField]
    private Text Stage1Gold;
    [SerializeField]
    private Text Stage2Exp;
    [SerializeField]
    private Text Stage2Gold;
    [SerializeField]
    private Text Stage3Exp;
    [SerializeField]
    private Text Stage3Gold;
    [SerializeField]
    private Text Stage4Exp;
    [SerializeField]
    private Text Stage4Gold;
    [SerializeField]
    private Text Stage5Exp;
    [SerializeField]
    private Text Stage5Gold;
    [SerializeField]
    private Text Stage6Exp;
    [SerializeField]
    private Text Stage6Gold;
    [SerializeField]
    private Text Stage7Exp;
    [SerializeField]
    private Text Stage7Gold;
    [SerializeField]
    private Text Stage8Exp;
    [SerializeField]
    private Text Stage8Gold;
    [SerializeField]
    private Text Stage9Exp;
    [SerializeField]
    private Text Stage9Gold;
    [SerializeField]
    private Text Stage10Exp;
    [SerializeField]
    private Text Stage10Gold;

    // Start is called before the first frame update
    void Start()
    {
        Level.text = "Level " + playerStats.stats[0].level.ToString();
        Gold.text = "Gold " + playerStats.stats[0].gold.ToString();

        Stage1Exp.text = stage10.stage[0].experience.ToString();
        Stage1Gold.text = stage10.stage[0].gold.ToString();

        Stage2Exp.text = stage10.stage[1].experience.ToString();
        Stage2Gold.text = stage10.stage[1].gold.ToString();

        Stage3Exp.text = stage10.stage[2].experience.ToString();
        Stage3Gold.text = stage10.stage[2].gold.ToString();

        Stage4Exp.text = stage10.stage[3].experience.ToString();
        Stage4Gold.text = stage10.stage[3].gold.ToString();

        Stage5Exp.text = stage10.stage[4].experience.ToString();
        Stage5Gold.text = stage10.stage[4].gold.ToString();

        Stage6Exp.text = stage10.stage[5].experience.ToString();
        Stage6Gold.text = stage10.stage[5].gold.ToString();

        Stage7Exp.text = stage10.stage[6].experience.ToString();
        Stage7Gold.text = stage10.stage[6].gold.ToString();

        Stage8Exp.text = stage10.stage[7].experience.ToString();
        Stage8Gold.text = stage10.stage[7].gold.ToString();
        
        Stage9Exp.text = stage10.stage[8].experience.ToString();
        Stage9Gold.text = stage10.stage[8].gold.ToString();

        Stage10Exp.text = stage10.stage[9].experience.ToString();
        Stage10Gold.text = stage10.stage[9].gold.ToString();

        /*      
        //스테이지 선택 화면에서 골드 및 경험치 획득 초기화
        playerStats.stats[0].experience = 0;
        playerStats.stats[0].gold = 0;

        for (int i = 0; stage10.stage.Length > i; i++)
        {
            playerStats.stats[0].experience += stage10.stage[i].experience;
            playerStats.stats[0].gold += stage10.stage[i].gold;
        }
        */
    }

}
