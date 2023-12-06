using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static InventoryItem;
using static UnitEquipmentScript;

public class PlayerAttackScript : MonoBehaviour
{
    public GuidReference guidRef;

    //todo create dedicated script for attack UI
    //todo create dedicated data for attacks
    // Start is called before the first frame update
    public CombatContextDataHost contextDataHost;
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
    Dictionary<ItemType, Attack> EquippedAttacks;
    [SerializeField] AttackBarScript attackUI;
    [SerializeField]private UnitEquipmentScript equipment;

    private List<Attack> GetBasicAvailableAttacks()
    {
        return unitData.statInstance.attacks;
    }
    public void SetEquipment(UnitEquipmentScript arg)
    {
        this.equipment.SetFromInstance( arg);
        UpdateEquippedAttacks();
    }
   
    private void UpdateEquippedAttacks()
    {
        //Gain access to equipped items
        
        //For now, only worrying about weapons

        List<Attack> nativeAndEquippedAttacks = GetBasicAvailableAttacks().Union(equipment.weapon.attacks).ToList();
        
        //Reset becasue yucky state
        inputAttackPairs = new Dictionary<char, Attack>();
       //coalesce the attacks from equipment with those native with keybinds
        for (int i = 0; i < nativeAndEquippedAttacks.Count(); i++)
        {
            inputAttackPairs.Add(numbers[i+1], nativeAndEquippedAttacks[i]);
            unitData.OnUpdate += nativeAndEquippedAttacks[i].TickCooldown;
        }
          SetAttacksOnUI(inputAttackPairs
            .ToDictionary(pair => pair.Key.ToString(), pair => pair.Value));
        
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
    public void Awake()
    {
    }
    private IEnumerator IMightJustKillMyself()
    {
        for(;;)
        {
            yield return new WaitForSeconds(0.1f);
        this.SetEquipment(guidRef.gameObject.GetComponent<UnitEquipmentScript>());

        }
        yield break;
    }
    public void Start()
    {
        Debug.Log((guidRef.gameObject is null )+ "I'm gonna deepthroat a shotgun");
        StartCoroutine((IMightJustKillMyself()));
        //add 1 to i to avoid 0 keybind  
        for (int i = 0; i < GetBasicAvailableAttacks().Count; i++)
        {
            inputAttackPairs.Add(numbers[i+1], GetBasicAvailableAttacks()[i]);
            unitData.OnUpdate += GetBasicAvailableAttacks()[i].TickCooldown;
        }
        SetAttacksOnUI(inputAttackPairs
            .ToDictionary(pair => pair.Key.ToString(), pair => pair.Value));
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        Debug.Log((guidRef.gameObject is null )+ "I'm gonna deepthroat a magnum");

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
        attack.Use(unitData.gameObject);
    }
}