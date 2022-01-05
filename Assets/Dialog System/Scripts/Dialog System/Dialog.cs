using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string dialogName;
    public string title;
    public bool onlyShowTitleOnFirstLine;
    [TextArea(2,10)] public string[] lines;
}
