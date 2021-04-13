using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string NPCName;

    [TextArea(3, 5)]
    public string[] sentances;

}
