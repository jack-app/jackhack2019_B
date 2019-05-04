using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    public PocketHumanData IconPocketHumanData;

    private ScenePanelManager scenePanelManager;

    private void Start()
    {
        scenePanelManager = GameObject.Find("FooterCanvas").GetComponent<ScenePanelManager>();
    }

    public void OnClick()
    {
        scenePanelManager.ChangeScenePanelToCard(IconPocketHumanData);
    }
}
