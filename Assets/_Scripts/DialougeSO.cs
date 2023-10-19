using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialouge/DialougeScriptableObject")]
public class DialougeSO : ScriptableObject
{
    [TextArea]public  string[] dialouges;
    public bool HasPortrait;
    public Sprite portrait;
    public string characterName;
    public Response[] responses ;
    public bool HasResponses()
    {
       return responses != null && responses.Length > 0; 
    }
}
[Serializable]
internal struct DialougeData
{
    public string sentence;
}