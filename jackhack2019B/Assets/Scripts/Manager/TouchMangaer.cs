using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class TouchMangaer : MonoBehaviour
{
    private void Update()
    {
        // クリックした位置へのRay取得
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // クリックしたら
        if (Input.GetMouseButtonDown(0))
        {
            // Ray飛ばす
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log(hit.collider.name);
                if(hit.transform.tag == "Icon"){
                    hit.collider.GetComponent<IconObject>().OpenProfile();
                }else if (hit.transform.tag == "HandCard")
                {
                    if (hit.transform.GetComponent<PhotonView>().IsMine)
                    {
                        GameObject[] handCards = GameObject.FindGameObjectsWithTag("HandCard");
                        foreach (var handCard in handCards)
                        {
                            if (handCard.GetComponent<PhotonView>().IsMine && handCard != hit.collider.gameObject)
                            {
                                handCard.SetActive(false);
                            }
                        }

                        DuelMatchMaker.SetReady(hit.collider.GetComponent<HandCard>().pocketHumanData.BloodType);
                    }
                }
            }
        }
    }
}
