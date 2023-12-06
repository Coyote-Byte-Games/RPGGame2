using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneTransitioner : SingletonPersistent<SceneTransitioner>
{
    public GuidReference surrogateGUIDRef;
    public CombatContextDataHost combatDataHost;
    Animator sceneChanger;
    // public CombatContextDataHost dataHost;
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

            //loading the surrogate
            surrogateGUIDRef.gameObject.GetComponent<UnitEquipmentScript>().SetFromInstance(GameObject.Find("Spork").GetComponent<UnitEquipmentScript>()); 
            Debug.Log(surrogateGUIDRef.guid + " why me man");
            //Just Change scenes
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)Scenes.COMBAT);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += CombatContextSet;
            yield break;
        }
    }
    private void CombatContextSet(Scene scene, LoadSceneMode mode)
    {
        // FindAnyObjectByType<PlayerAttackScript>().SetEquipment(SaveDataHelper.LoadEquipmentData());
    }

    enum Scenes
    {
        COMBAT,
        OVERWORLD,
        MAINMENU,
        GAMEOVER,
    }
}
