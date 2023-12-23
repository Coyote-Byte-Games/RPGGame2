//Provides an way to load scenes within their own containers
using System.Collections;
using UnityEngine;
using static SceneTransitioner;

public class OverWorldDirtyMan : SceneHandsDirtyMan
{
    public override void To()
    {
       //FindObjectOfType<MonoBehaviour>()
       GameObject.Find("Spork").GetComponent<MonoBehaviour>().StartCoroutine(LoadOverworld());
    }
     private IEnumerator LoadOverworld()
    {
        for (; ; )
        {
            //Show the screen doin' the thing

            sceneChanger.SetTrigger("Close");
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            // yield return new WaitForSeconds(0.5f);
            
            yield break;
        }
    }
}
