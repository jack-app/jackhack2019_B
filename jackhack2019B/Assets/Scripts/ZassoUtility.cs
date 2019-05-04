using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZassoUtility : MonoBehaviour
{
    private static PocketHumanDataSet pocketHumanDataSet;
    private static PocketHumanData[] pocketHumanDatas;

    void Start()
    {
        pocketHumanDataSet = Resources.Load<PocketHumanDataSet>("PocketHumanDataSet");
        pocketHumanDatas = pocketHumanDataSet.PocketHumanDatas;
        
        UserDataManager.SavePocketHuman("やっきぃ");
    }

    public static PocketHumanData FindPocketHumanData(string human_name)
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
}
