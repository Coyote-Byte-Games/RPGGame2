using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarScript : MonoBehaviour
{
    //The template
    private List<Attack> attacksAdded;
    public GameObject attackUITemplate;
    public int numberOfAttacksRegistered;
    public void AddAttack(Attack item)
    {
        GameObject addedAttackUI = Instantiate(attackUITemplate);
        if (addedAttackUI.TryGetComponent<AttackUIData>(out var data))
        {
            //data object called... data is now created
            data.SetName(item.attackName);
            //setting the number of the attack to the total attacks registered, including this
            data.SetKeybind((++numberOfAttacksRegistered).ToString());
        }
        else
        {
            //data isn't there, what the hell?
        }
    }

    private void RemoveAttack()
    {
        //todo reset the numbers on the remaining attacks
    }
    public void Wipe()
    {
     foreach (Transform child in GetComponent<RectTransform>())
     {
        Destroy(child.gameObject);
     }
     numberOfAttacksRegistered = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
