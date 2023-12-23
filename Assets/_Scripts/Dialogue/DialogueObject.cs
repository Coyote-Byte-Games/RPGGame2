using UnityEngine;
using UnityEngine.Events;
public class DialogueObject : MonoBehaviour, IDialougeObject
{
    [SerializeField] string[] dialogueStrings;
    [SerializeField] Response[] responses;
    public UnityEvent endingEvent;
    public string[] GetStrings()
    {
     return dialogueStrings;
    }

    public UnityEvent GetEvent()
    {
        return endingEvent;
    }

    public Response[] GetResponses()
    {
        return responses;
    }

    public bool HasResponses()
    {
        return !(responses[0] is null);
    }

    public void ShowPortrait()
    {
     Debug.Log("Show the portrait, Shaun! DO IT NOOOOOOOOOOOOW");
    }
}