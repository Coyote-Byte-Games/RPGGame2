
using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class TypeWriterEffect : MonoBehaviour
{
    public Coroutine Run(string typed, TMP_Text textLabel, float speed = 1.0f)
    {
        return StartCoroutine(TypeText(typed, textLabel, speed));
    }
    private IEnumerator TypeText(string typed, TMP_Text textLabel, float speed = 1.0f)
    {
        int breakNo=0;
        float t = 0;
        int charIndex = 0;

        while (charIndex < typed.Length)
        {
            
            t += Time.deltaTime * 8 * speed;
            charIndex = Mathf.FloorToInt(t);
            //So we don't overflow the capacity
            charIndex = Mathf.Clamp(charIndex, 0, typed.Length);

            textLabel.text = typed.Substring(0, charIndex);
            if (  Input.GetKeyDown(KeyCode.E))
            {
                if (++breakNo >= 2)
                {
                    break;
                }
                
            
            }
            
            yield return null;

        }
        //To ensure we have the desired text by the end of things.
        textLabel.text = typed;
        yield break;
    }
}