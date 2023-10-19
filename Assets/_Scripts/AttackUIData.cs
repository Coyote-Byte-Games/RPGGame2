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
     this.keybindIcon = (Image)Resources.Load($"keys/{keyBindLookup}");   
    }
    public void OnValidate()
    {
        attackNameUI.text = name;

    }

}