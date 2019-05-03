using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static void SavePocketHuman(string human_name)
    {
        int level = LoadPocketHuman(human_name);
        //ポケットヒューマンのレベルをセーブ
        PlayerPrefs.SetInt("HumanLevel" + human_name, level + 1);
    }

    public static int LoadPocketHuman(string human_name)
    {
        return PlayerPrefs.GetInt("HumanLevel" + human_name, 0);
    }

    public static Dictionary<string, int> LoadAllPocketHuman()
    {
        Dictionary<string, int> resultDic = new Dictionary<string, int>();
        
        PocketHumanDataSet pocketHumanDataSet = Resources.Load<PocketHumanDataSet>("PocketHumanDataSet");
        PocketHumanData[] pocketHumanDatas = pocketHumanDataSet.PocketHumanDatas;

        foreach (var pocketHumanData in pocketHumanDatas)
        {
            resultDic[pocketHumanData.Name] = LoadPocketHuman(pocketHumanData.Name);
        }
        
        return resultDic;
    }
}
