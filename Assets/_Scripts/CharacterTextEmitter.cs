using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTextEmitter : MonoBehaviour
{
   //Purpose is to provide API for emitting damage numbers and the like
   public GameObject textPrefab;

   public float destroyTime = 1;
   public void Awake()
   {

   }
   public void ShowText(string input, TextStyle style = TextStyle.Neutral)
   {
      var instance = Instantiate(textPrefab, gameObject.transform.position, quaternion.identity);
      var textMesh = instance.GetComponentInChildren<TextMesh>();
      textMesh.text = input;
      //changing style settings
      switch (style)
      {
         case TextStyle.Dire:
      textMesh.color = Color.red;
            break;
         default:
         break;
      }

      Destroy(instance, destroyTime);
   }
   public enum TextStyle
   {
      Neutral,
      Dire,
      Great
   }
}
