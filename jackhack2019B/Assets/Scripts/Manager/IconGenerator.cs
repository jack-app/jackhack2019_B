using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconGenerator : MonoBehaviour
{
    public GameObject IconPrefab;
    public GameObject Content;

    private PocketHumanDataSet pocketHumanDataSet;
    private PocketHumanData[] pocketHumanDatas;
    
    void Start()
    {
        pocketHumanDataSet = Resources.Load<PocketHumanDataSet>("PocketHumanDataSet");
        pocketHumanDatas = pocketHumanDataSet.PocketHumanDatas;
        MakeIcons();
    }

    void MakeIcons()
    {
        foreach (var pocketHuman in UserDataManager.LoadAllPocketHuman())
        {
            //レベルが0じゃなかったらアイコンを作る(Keyに名前、Valueにレベルが入っている
            if (pocketHuman.Value != 0)
            {
                GameObject obj = Instantiate(IconPrefab, Content.transform);
                PocketHumanData targetPocketHumanData = ZassoUtility.FindPocketHumanData(pocketHuman.Key);
                obj.GetComponent<Image>().sprite = targetPocketHumanData.Card;
                obj.GetComponent<Icon>().IconPocketHumanData = targetPocketHumanData;
            }
        }
    }

    /*
    PocketHumanData FindPocketHumanData(string human_name)
    {
        foreach (var pocketHumanData in pocketHumanDatas)
        {
            if (pocketHumanData.Name == human_name)
            {
                return pocketHumanData;
            }
        }

        return null;
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
