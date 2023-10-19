using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitMB : MonoBehaviour
{
   /*
   This class is responsible for handling INSTANCE DATA: 
   Attacks available (which are derived from the StatBase) ,
   access to the instance data Stats
   */
   //todo fix privacy of field
   [SerializeField] private UnitStats baseStats ;
   public UnitStats statInstance {get; private set;}
   public void Start()
   {
      statInstance = baseStats;
   }
}
