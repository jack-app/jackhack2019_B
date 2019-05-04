using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ScenePanelManager : MonoBehaviour
{
    public GameObject DeckScenePanel;
    public GameObject CardScenePanel;

    public void ChangeScenePanelToCard(PocketHumanData pocketHumanData)
    {
        AllSetActiveFalse();
        
    }

    void AllSetActiveFalse()
    {
        DeckScenePanel.SetActive(false);
        CardScenePanel.SetActive(false);
    }
}
