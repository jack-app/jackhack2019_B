using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    public PocketHumanData pocketHumanData;
    
    public void SetPocketHumanData(PocketHumanData pocket_human_data)
    {
        pocketHumanData = pocket_human_data;
        GetComponent<SpriteRenderer>().sprite = pocketHumanData.Card;
        GetComponent<PhotonView>().RPC("PunSetPocketHumanData", RpcTarget.Others, pocketHumanData.Name);
    }

    [PunRPC]
    void PunSetPocketHumanData(string human_name)
    {
        pocketHumanData = ZassoUtility.FindPocketHumanData(human_name);
        GetComponent<SpriteRenderer>().sprite = pocketHumanData.Card;
    }
}
