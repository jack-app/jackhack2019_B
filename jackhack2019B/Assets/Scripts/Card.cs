using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public PocketHumanData pocketHumanData;

    private GameObject gameManager;

    void OnEnable()
    {
        gameManager = GameObject.FindWithTag("GameController");
        GetComponent<Image>().sprite = pocketHumanData.Card;
    }

    public void OnClickSummonButton(){
        gameManager.GetComponent<SummonPocketHumanManager>().SummonPocketHuman(pocketHumanData);
    }
}
