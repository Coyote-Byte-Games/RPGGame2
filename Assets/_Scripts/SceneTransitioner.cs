using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneTransitioner : StaticInstance<SceneTransitioner>
{
    #region scene changers
    public SceneHandsDirtyMan combatSceneChanger;
    public SceneHandsDirtyMan overWorldSceneChanger;    
    #endregion
    public CombatContextDataHost combatDataHost;
    // public CombatContextDataHost dataHost;
   
    public void OW2Combat()
    {
        combatSceneChanger.To();
    }
    public void ToOW()
    {
        Debug.Log("found ya you lil bastard");
        overWorldSceneChanger.To();
    }

    public enum SCENES
    {
        COMBAT,
        OVERWORLD,
        MAINMENU,
        GAMEOVER,
    }
    
}
