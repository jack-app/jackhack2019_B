using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketHumanCardObject : MonoBehaviour
{
    public PocketHumanData pocketHumanData;

    public GameObject CardObject;
    public GameObject IconObject;

    public void Start(){
        CardObject.GetComponent<SpriteRenderer>().sprite = pocketHumanData.Card;
        IconObject.GetComponent<SpriteRenderer>().sprite = pocketHumanData.Icon;
    }
}
