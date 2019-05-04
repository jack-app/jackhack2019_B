using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    public PocketHumanData pocketHumanData;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = pocketHumanData.Card;
        Debug.Log(pocketHumanData.Name);
    }
}
