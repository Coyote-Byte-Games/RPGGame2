using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirectManager : MonoBehaviour
{
    // This is for managing things directly, like startup
    void Awake()
    {
        int random = UnityEngine.Random.Range(1, 101);
        //we do this for random transitions
        GetComponent<Animator>().SetFloat("why", random);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
