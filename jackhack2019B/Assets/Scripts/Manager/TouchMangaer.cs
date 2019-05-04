using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
                    DuelMatchMaker.SetReady();
                }
            }
        }
    }
}
