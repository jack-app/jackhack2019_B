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

    public static PocketHumanData LoadRandomPocketHumanData()
    {
        PocketHumanData result = null;

        int loop_count = 0;
        
        while (result == null)
        {
            loop_count++;
            PocketHumanDataSet pocketHumanDataSet = Resources.Load<PocketHumanDataSet>("PocketHumanDataSet");
            PocketHumanData[] pocketHumanDatas = pocketHumanDataSet.PocketHumanDatas;

            int r = Random.Range(0, pocketHumanDatas.Length);
            if (LoadPocketHuman(pocketHumanDatas[r].Name) == 0)
            {
                result = null;
            }
            else
            {
                result = pocketHumanDatas[r];
            }

            if (loop_count > 100)
            {
                break;
            }
        }

        return result;
    }

    public static void SaveDeck(int index, string human_name)
    {
        PlayerPrefs.SetString("Deck" + index, human_name);
    }
    
    public static PocketHumanData[] LoadDeck()
    {
        string deck0 = PlayerPrefs.GetString("Deck0", "homu");
        string deck1 = PlayerPrefs.GetString("Deck1", "homu");
        string deck2 = PlayerPrefs.GetString("Deck2", "homu");

        PocketHumanData[] deck = new PocketHumanData[3];
        deck[0] = ZassoUtility.FindPocketHumanData(deck0);
        deck[1] = ZassoUtility.FindPocketHumanData(deck1);
        deck[2] = ZassoUtility.FindPocketHumanData(deck2);

        return deck;
    }
}
