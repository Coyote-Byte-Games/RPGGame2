using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// [CreateAssetMenu(menuName = "Attack")]

[Serializable]
public class Attack

{
    public Vector2Int[] attackShape = TileTelegraphVFXScript.GetShape(TileTelegraphVFXScript.DefaultShape.ONE_LONG);
    //The legal name assigned to the attack at birth. Deadname!
    public string attackName = "Kill";
    //The trigger set when the attack is used
    public string animationName = "Attack";
    //created when the attack is used
    public GameObject attackGameObject;
    public LoadingBar progressBar;
    [SerializeField] float coolDownLength = 1;
    [SerializeField] private float coolDownRemaining;
    [SerializeField] bool coolDownReady;
    public void TickCooldown()
    {

        coolDownRemaining -= Time.deltaTime;
        if (coolDownRemaining > 0)
        {
            coolDownReady = false;
        }
        if (coolDownRemaining < 0)
        {
            coolDownReady = true;
        }
        try
        {
            progressBar.SetFillAmount((coolDownLength - coolDownRemaining) / coolDownLength);
        }
        catch (Exception)
        {
        }
    }

    internal void Use(GameObject userGameObject)
    {
        if (coolDownReady)
        {
            userGameObject.GetComponentInChildren<Animator>().SetTrigger(animationName);
            var spawn = GameObject.Instantiate(attackGameObject, userGameObject.transform.position, quaternion.identity);
            spawn.GetComponentInChildren<AttackGameObject>().SetUser(userGameObject);
            coolDownReady = false;
            coolDownRemaining = coolDownLength;

        }

    }
}
