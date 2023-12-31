using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    private char[] numbers =
    {
        '0',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9'
    };
    [SerializeField] UnitMB unitData;
    Dictionary<char, Attack> inputAttackPairs = new Dictionary<char, Attack>();

    [SerializeField] AttackBarScript attackUI;
    private List<Attack> GetAvailableAttacks()
    {
        return unitData.statInstance.attacks;
    }
    private void SetAttacksOnUI(List<Attack> input)
    {
        //Resets it
        attackUI.Wipe();
        foreach (var item in input)
        {
            //adds the attack
            var element = attackUI.AddAttack(item);
            item.progressBar = element.GetComponentInChildren<LoadingBar>(); 
        }
    }
    //todo fixThis
    private void SetAttacksOnUI(Dictionary<string, Attack> input)
    {
        //Resets it
        attackUI.Wipe();
        List<string> inputStrings = new List<string>(input.Keys);
        // so we can refer to the keys without creating a new list every damn loop
        for (int i = 0; i < input.Count; i++)
        {
            var element = attackUI.AddAttack
            (
                input[inputStrings[i]],
                inputStrings[i]
            );
             input[inputStrings[i]].progressBar = element.GetComponentInChildren<LoadingBar>(); 

        }
    }
    void Start()
    {
        //add 1 to i to avoid 0 keybind  
        for (int i = 0; i < GetAvailableAttacks().Count; i++)
        {
            inputAttackPairs.Add(numbers[i+1], GetAvailableAttacks()[i]);
            unitData.OnUpdate += GetAvailableAttacks()[i].TickCooldown;
        }
        SetAttacksOnUI(inputAttackPairs
            .ToDictionary(pair => pair.Key.ToString(), pair => pair.Value));
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }


    private void TakeInput()
    {
        foreach (var item in Input.inputString.ToCharArray())
        {
            if (numbers.Contains(item))
            {
                UseAttack(inputAttackPairs[item]);
            }
        }
    }

    private void UseAttack(Attack attack)
    {
        attack.Use(gameObject);
    }
}
