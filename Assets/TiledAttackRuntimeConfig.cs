using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledAttackRuntimeConfig : MonoBehaviour
{
    // Start is called before the first frame update
    public TiledAttackEditorConfig editor;
    void Awake()
    {
        Destroy(gameObject, editor.AttackDestroyTime);
    }
}
