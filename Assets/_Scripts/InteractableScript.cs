using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    [SerializeField] DialogueObject startingDialouge;
    [SerializeField] DialougeManager dialougeManager;
    // Start is called before the first frame update
    void Start()
    {
        dialougeManager = FindAnyObjectByType<DialougeManager>();
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
