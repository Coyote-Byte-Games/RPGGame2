using System;
using UnityEngine;

public class AttackSO: ScriptableObject
{
    //Attack SO is responsible for transferring data of an attack.
}
[Serializable]
public class Attack
{
    //The legal name assigned to the attack at birth. Deadname
    public string attackName;
    //The trigger set when the attack is used
    public string animationName = "Attack";
    //Created when the attack is used
    public GameObject attackGameObject;
} 