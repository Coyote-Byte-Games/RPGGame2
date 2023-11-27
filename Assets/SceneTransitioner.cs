using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneTransitioner : SingletonPersistent<SceneTransitioner>
{

    Animator sceneChanger;

    public void OW2Combat()
    {
        //Getting the transiiton screen
        sceneChanger = GameObject.Find("sceneCover").GetComponent<Animator>();

        //fixme: sloppy garbage
        FindObjectOfType<MonoBehaviour>().StartCoroutine(LoadCombat());
    }
    private IEnumerator LoadCombat()
    {
        for (; ; )
        {
            //Show the screen doin' the thing
            
            sceneChanger.SetTrigger("Close");
            
            //Show the info screen later

            yield return new WaitForSeconds(2);

            //Just Change scenes
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)Scenes.COMBAT);
            yield break;
        }
    }

    enum Scenes
    {
        COMBAT,
        OVERWORLD,
        MAINMENU,
        GAMEOVER,
    }
}
