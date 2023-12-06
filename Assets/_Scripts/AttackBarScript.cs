using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackBarScript : MonoBehaviour
{
    //The template
    public GameObject attackUITemplate;
    public int numberOfAttacksRegistered;
    //todo DRY smell
    public GameObject AddAttack(Attack item)
    {
        GameObject addedAttackUI = Instantiate(attackUITemplate);
        if (addedAttackUI.TryGetComponent<AttackUIData>(out var data))
        {
            //data object called... "data" is now created
            data.SetName(item.attackName);
            //setting the number of the attack to the total attacks registered, including this
            data.SetKeybind((++numberOfAttacksRegistered).ToString());
        }
        else
        {
            Debug.LogError("data isn't there, what the hell?");

        }
        return addedAttackUI;
    }
    internal GameObject AddAttack(Attack attack, string keyBind)
    {

        GameObject addedAttackUI = Instantiate(attackUITemplate, transform);
        if (addedAttackUI.TryGetComponent<AttackUIData>(out var data))
        {
            //data object called... data is now created
            data.SetName(attack.attackName);
            //setting the attack code to the name provided

            data.SetKeybind(keyBind);
        }
        else
        {
            //data isn't there, what the hell?
        }
        return addedAttackUI;
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
