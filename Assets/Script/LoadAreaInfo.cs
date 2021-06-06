using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAreaInfo : MonoBehaviour
{
    [SerializeField]
    private Stage10 stage10;//스테이지 정보

    [SerializeField]
    private PlayerStats playerStats;//플래이어 정보

    [SerializeField]
    private Text Level;
    [SerializeField]
    private Text Gold;

    [SerializeField]
    private Text Areaexp1;
    [SerializeField]
    private Text AreaGold1;

    // Start is called before the first frame update
    void Start()
    {
        float experience = 0;
        float gold = 0;

        for (int i = 0; stage10.stage.Length > i; i++)
        {
            experience += stage10.stage[i].experience;
            gold += stage10.stage[i].gold;
        }

        Level.text = "Level " + playerStats.stats[0].level.ToString();
        Gold.text = "Gold " + playerStats.stats[0].gold.ToString();

        Areaexp1.text = experience.ToString();
        AreaGold1.text = gold.ToString();


    }
}
