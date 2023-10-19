using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    //todo create dedicated script for attack UI
    //todo create dedicated data for attacks
    // Start is called before the first frame update

    //# needs:
    /*
    Attacks from character datas
    reference to Ui
    */
    [SerializeField] UnitMB unitData;
    [SerializeField] AttackBarScript attackUI;
    private List<Attack> GetAvailableAttacks()
    {
        List<Attack> attacksFromData = unitData.statInstance.attacks;
        return attacksFromData;    
    }
    private void SetAttacksOnUI(List<Attack> input)
    {
        //Resets it
        attackUI.Wipe();
        foreach (var item in input)
        {
            //adds the attack
            attackUI.AddAttack(item);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    private void TakeInput()
    {
        
    }
}
enum AttackCode
{
    //todo implement detection upon keys; also probably helps to define actually possible attack keys somewhere
    
}
