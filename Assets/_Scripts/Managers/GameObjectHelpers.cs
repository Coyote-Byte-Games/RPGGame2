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
    public void CardinalizeVector(Vector2 input)
    {
        
    }
}