
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Used for general utilities relating to gameobjects in the scene.
/// </summary>
class GameObjectHelpers : Singleton<GameObjectHelpers>
{
    [Header("GameObject References")]
    [SerializeField] GameObject explosion;
    public void FixedUpdate()
    {
        CoolDownTick();
    }
    public static void CoolDownTick()
    {
        return;
    }
    public void Kablooey(UnitMB instance)
    {
        var kaboom = Instantiate(explosion, instance.gameObject.transform.position, quaternion.identity);
        Destroy(instance.gameObject);
        //time for the animation to end
        Destroy(kaboom, 25f/60f);
    }
    public static GameObject CreatePopup(Transform caller, GameObject arg, int xSize, int ySize)
    {  var prefab = Instantiate(arg,GameObject.Find("Canvas").transform);
        prefab.transform.position = caller.position - new Vector3((caller.position.x>900 ? 500 : -500), 0,0);
        // Debug.Log(caller.position.x + "bazinga wowowowowo");
        prefab.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);
        return prefab;
    }
  
}