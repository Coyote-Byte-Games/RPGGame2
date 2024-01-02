using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Performs some QoL functions for an attack that may be strenuous to do in-game.
/// </summary>
[ExecuteInEditMode]
public class TiledAttackEditorConfig : MonoBehaviour
{
    public Animator ownAnimator;
    public float AttackDestroyTime;
    void OnEnable()
    {
        ownAnimator = GetComponent<Animator>();
        AttackDestroyTime = ownAnimator.runtimeAnimatorController.animationClips[0].length;
    }
}
