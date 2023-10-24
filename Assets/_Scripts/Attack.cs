using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Attack")]

public class Attack : ScriptableObject
{
      //The legal name assigned to the attack at birth. Deadname
    public string attackName;
    //The trigger set when the attack is used
    public string animationName = "Attack";
    //created when the attack is used
    public GameObject attackGameObject;
    public LoadingBar progressBar;


    [SerializeField] float coolDownLength;
    [SerializeField] private float coolDownRemaining;
    [SerializeField] bool coolDownReady;


    public void Awake()
    {
        
    }

    public void TickCooldown()
    {

        coolDownRemaining -= Time.deltaTime;

        progressBar.SetFillAmount((coolDownLength - coolDownRemaining));

        if (coolDownRemaining > 0)
        {
            coolDownReady = false;
        }
        if (coolDownRemaining < 0)
        {
        coolDownReady = true;
            
        }
    }

     internal void Use(GameObject gameObject)
    {
        if (coolDownReady)
        {
              var spawn = GameObject.Instantiate(attackGameObject, gameObject.transform.position, quaternion.identity);
        spawn.GetComponent<AttackGameObject>().user = gameObject;
        coolDownReady= false;
        coolDownRemaining = coolDownLength;

        }
        
    }
}