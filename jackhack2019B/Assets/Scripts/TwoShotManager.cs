using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoShotManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool includeMe = false;
        List<GameObject> VisibleIcons = new List<GameObject>();
        GameObject[] icons = GameObject.FindGameObjectsWithTag("Icon");
        foreach(GameObject icon in icons){
            if (icon.GetComponent<Renderer>().isVisible)
            {
                if (icon.GetComponent<IconObject>().pocketHumanData.Name == "やっきぃ")
                {
                    includeMe = true;
                }else{
                    Debug.Log("VisibleIconDetected!");
                    VisibleIcons.Add(icon);
                }
            }
        }

        foreach (GameObject visibleIcon in VisibleIcons)
        {
            if (UserDataManager.LoadPocketHuman(visibleIcon.GetComponent<IconObject>().pocketHumanData.Name) == 0)
            {
                UserDataManager.SavePocketHuman(visibleIcon.GetComponent<IconObject>().pocketHumanData.Name);
            }
        }
    }
}
