using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconObject : MonoBehaviour
{
    public PocketHumanData pocketHumanData;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = pocketHumanData.Card;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenProfile(){
        Application.OpenURL("https://jack-app.slack.com/team/U7H6X9ETW");
    }
}
