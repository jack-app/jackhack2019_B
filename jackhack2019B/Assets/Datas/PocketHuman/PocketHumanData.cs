using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Zassoes/Create PocketHumanData", fileName = "PocketHumanData" )]
public class PocketHumanData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Sprite Card;
    public string SlackURL;
}
