using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatTodoGO : MonoBehaviour
{
    //Unity properties
    public TMP_Text text; 
    //Sets the text of the widget
    public void SetText(string text)
    {
        //what the hell?
        this.text.text = text;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
