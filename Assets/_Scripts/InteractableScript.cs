using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    [SerializeField] DialougeSO startingDialouge;
    [SerializeField] DialougeManager dialougeManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialouge()
    {
        Debug.Log("trying here");
        dialougeManager.ShowDialouge(startingDialouge);
    }
}
