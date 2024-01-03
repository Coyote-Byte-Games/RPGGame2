using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static TileTelegraphVFXScript.ShapeUtil;
public abstract class AttackCombatActionBase : MonoBehaviour, ICombatAction
{
    #region Fields
    [SerializeField] internal CombatGlobalInfoSO info;

    internal Vector2Int[] attackShape = TileTelegraphVFXScript.ShapeUtil.LineFroToVertical(0, -1, -5);
    //The legal name assigned to the attack at birth. Deadname!
    public string attackName = "Kill";
    //The trigger set when the attack is used
    public string animationName = "Attack";
    //created when the attack is used
    [OdinSerialize] public GameObject attackGameObject;
    public LoadingBar progressBar;
    [SerializeField] internal float coolDownLength = 1;
    [SerializeField] internal float coolDownRemaining;
    [SerializeField] internal bool coolDownReady;
    [SerializeField] internal int creditsRequired;
    [SerializeField] int tileAggroDistance = 5;
    private int _heading;

    //! smells
    public bool TryGetSupposedHeading(out int heading)
    {
        return (heading = _heading) != -1;
    }
    public void CreditsUpdated(object sender, CreditManagerEventArgs e)
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

    //Clearly, attacks need a new "interface" so that they can turn 
    public abstract void Use(GameObject userGameObject);


    public int CreditsRequired()
    {
        return creditsRequired;
    }
    TileTelegraphData ICombatAction.GetTelegraphData()
    {

        Debug.Log("checked for data");

        return new TileTelegraphData { color = TileTelegraphVFXScript.Palette.Red, tileCoords = attackShape.Select(x => Rotate(x, _heading)).ToArray(), heading = Vector2.down };
    }
    public bool AffirmUseAndDir(GameObject user, GameObject origin)
    {
        Vector2 playerPosition = origin.transform.position;
        Vector2 userPosition = user.transform.position;
        //Try each direction
        int[] dirs = { 0, 90, 180, 270 };
        foreach (var degrees in dirs)
        {
            var rotated = Rotate(playerPosition - userPosition, degrees) / info.CombatStepDistance;
            bool req = attackShape.Contains(rotated);

            //? debugging stuff
            Debug.Log($"Testing at {degrees}degs; The pattern, originally {String.Join(',', attackShape)} - the tested pos is {String.Join(',', rotated)} and does {(req ? "" : "NOT")} work.");
            if (req)
            {
                //because we're rotating the opposite item
                this._heading = -degrees;
                return true && coolDownReady;
            }
        }
        _heading = 0;
        return false;
    }
    int ICombatAction.CreditsRequired()
    {
        return creditsRequired;
    }

    public float GetBaseCooldown()
    {
        return coolDownLength;
    }
}
