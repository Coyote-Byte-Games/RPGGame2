//Provides an way to load scenes within their own containers
using UnityEngine;
/* So like the current happs is as follows: 
the concern is that there will be errors when trying to get the singleton instance of combatdirtyman becasue the way the singleton class even works
it might be fine, since its pretty much as cast to its base type? That should be fine considering the very nature of inheritance
UNless something ridiclous like a runtime error for accidental conversion to that type :/ 
*/

public abstract class SceneHandsDirtyMan : MonoBehaviour
{
    public Animator sceneChanger;

    private void SceneChangeDisplay()
    {
        sceneChanger = GameObject.Find("sceneCover").GetComponent<Animator>();

    }
    public abstract void To();
}
