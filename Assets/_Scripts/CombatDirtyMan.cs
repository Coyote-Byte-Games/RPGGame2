//Provides an way to load scenes within their own containers
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneTransitioner;
public class CombatDirtyMan : SceneHandsDirtyMan
{
    public GuidReference surrogateGUIDRef;
    private void CombatContextSet(Scene scene, LoadSceneMode mode)
    {
        // FindAnyObjectByType<PlayerAttackScript>().SetEquipment(SaveDataHelper.LoadEquipmentData());
    }
    public override void To()
    {
        GameObject.Find("Spork").GetComponent<MonoBehaviour>().StartCoroutine(LoadCombat());

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
            //Just Change scenes
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SCENES.COMBAT);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += CombatContextSet;
            yield break;
        }
    }
}