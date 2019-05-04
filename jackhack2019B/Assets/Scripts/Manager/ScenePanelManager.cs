using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class ScenePanelManager : MonoBehaviour
{
    public GameObject DeckScenePanel;
    public GameObject CardScenePanel;

    public Image CardImage;

    public void ChangerScenePanelToCamera(){
        AllSetActiveFalse();
    }

    public void ChangeScenePanelToDeck(){
        AllSetActiveFalse();
        DeckScenePanel.SetActive(true);
    }

    public void ChangeScenePanelToCard(PocketHumanData pocketHumanData)
    {
        AllSetActiveFalse();
        CardScenePanel.SetActive(true);
        CardImage.sprite = pocketHumanData.Card;
    }

    void AllSetActiveFalse()
    {
        DeckScenePanel.SetActive(false);
        CardScenePanel.SetActive(false);
    }
}
