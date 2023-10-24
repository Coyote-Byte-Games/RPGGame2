using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Responsible for managing data of the UI in attacks
public class AttackUIData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackNameUI; 
    [SerializeField] private Image keybindIcon; 

    //todo get resources

    public void SetName(string arg)
    {
        name = arg;
        attackNameUI.text = arg;
    }
    public void SetKeybind(string keyBindLookup)
    {
        Debug.Log(keyBindLookup);
        try
        {
     this.keybindIcon.sprite = Resources.Load($"keys/{keyBindLookup}", typeof(Sprite)) as Sprite; 
            
        }
        catch (System.Exception e)
        {
            
Debug.LogError(e);
        }
    }
    public void OnValidate()
    {
        attackNameUI.text = name;

    }

}