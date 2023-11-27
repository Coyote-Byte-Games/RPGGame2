using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialouge/DialougeScriptableObject")]
public class DialougeSO : ScriptableObject
{
    [TextArea]public  string[] dialouges;
    public bool HasPortrait;
    public Sprite portrait;
    public string characterName;
    public Response[] responses ;
    //A gameobject spawned when this dialouge ends. Can be used with other scripts to make terrible callbacks, in a way.
    public UnityEvent eventt;
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