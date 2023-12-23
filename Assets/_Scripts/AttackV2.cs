using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class AttackCombatAction : ICombatAction

{
    #region Fields
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

    public void CreditsUpdated(object sender, CreditManagerEventArgs e)
    {
        throw new NotImplementedException();
    }

    public bool IsDone()
    {
        throw new NotImplementedException();
    }

    public void GetTelegraphData()
    {
        throw new NotImplementedException();
    }
    #endregion

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
        catch (System.Exception)
        {
        }
    }
    internal void Use(GameObject userGameObject)
    {
        if (coolDownReady)
        {
            userGameObject.GetComponentInChildren<Animator>().SetTrigger(animationName);
            var spawn = GameObject.Instantiate(attackGameObject, userGameObject.transform.position, quaternion.identity);
            spawn.GetComponentInChildren<AttackGameObject>().user = userGameObject;
            coolDownReady = false;
            coolDownRemaining = coolDownLength;

        }

    }

    public int CreditsRequired()
    {
        throw new NotImplementedException();
    }

    TileTelegraphData ICombatAction.GetTelegraphData()
    {
        throw new NotImplementedException();
    }
}