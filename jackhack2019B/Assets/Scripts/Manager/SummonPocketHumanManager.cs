using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPocketHumanManager : MonoBehaviour
{
    public GameObject PocketHumanCardObject;
    public void SummonPocketHuman(PocketHumanData pocketHumanData){
        Vector3 summonPosition = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1.5f;

        GameObject obj = Instantiate(PocketHumanCardObject, summonPosition, Quaternion.identity);
        obj.GetComponent<PocketHumanCardObject>().pocketHumanData = pocketHumanData;
    }
}
