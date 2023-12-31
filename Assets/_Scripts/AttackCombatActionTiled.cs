using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static TileTelegraphVFXScript.ShapeUtil;

[Serializable]
///This version of the class is for attacks that use tiles to attack instead of gameobjescts
public class AttackCombatActionTileBased : AttackCombatActionBase

{
    public GameObject tileAttack;
    public override void Use(GameObject userGameObject)
    {
        if (!TryGetSupposedHeading(out int heading))
        {
            Debug.LogWarning("Heading not set on gameobject! Attack may default to downward positon.");
        }
        //Get pattern from attack, rotating accordingly
        Vector2Int[] pattern = attackShape.Select(x => Rotate(x, heading)).ToArray();
        var parent = new GameObject();
        parent.transform.localPosition = Vector2.zero;
        //For each tile, instantiate that attack item 
        foreach (var item in pattern.Select(x => new Vector3(x.x, x.y, 0)))
        {
            var tiledAttack = GameObject.Instantiate(tileAttack, item, quaternion.identity, parent.transform);
            tileAttack.transform.localPosition = item * info.CombatStepDistance;
        }
    }
}