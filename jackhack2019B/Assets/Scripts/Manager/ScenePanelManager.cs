using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class ScenePanelManager : MonoBehaviour
{
    public GameObject DeckScenePanel;
    public GameObject CardScenePanel;

    public GameObject CardParent;

    public void ChangerScenePanelToCamera(){
        AllSetActiveFalse();
    }

    public void ChangeScenePanelToDeck(){
        AllSetActiveFalse();
        DeckScenePanel.SetActive(true);
    }

    public void ChangeScenePanelToDuel()
    {
        AllSetActiveFalse();
        DuelMatchMaker.StartMatchMaking();
    }

    public void ChangeScenePanelToCard(PocketHumanData pocketHumanData)
    {
        AllSetActiveFalse();
        CardParent.GetComponent<Card>().pocketHumanData = pocketHumanData;
        Debug.Log(pocketHumanData.Card);
        CardScenePanel.SetActive(true);
    }

    void AllSetActiveFalse()
    {
        DeckScenePanel.SetActive(false);
        CardScenePanel.SetActive(false);
    }
}
