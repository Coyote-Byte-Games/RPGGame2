using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialougeManager : MonoBehaviour
{

    [SerializeField] TypeWriterEffect typeWriter;
    [SerializeField] TMP_Text textLabel;
    [SerializeField] GameObject dialougeBox;
    [SerializeField] GameObject skip;
    [SerializeField] Image portrait;
    [SerializeField] TMP_Text nameText;
    public bool DialougeActive { get; private set; }
    public ResponseHandler responseHandler;
    // Start is called before the first frame update
    void Start()
    {
        CloseDialouge();

        responseHandler = GetComponent<ResponseHandler>();
        typeWriter = GetComponent<TypeWriterEffect>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowDialouge(IDialougeObject dialougeObject)
    {

        DialougeActive = true;
        dialougeBox.SetActive(true);

        StartCoroutine(StepThroughDialouge(dialougeObject));
    }

    private IEnumerator StepThroughDialouge(IDialougeObject dialougeObject)
    {
        dialougeObject.ShowPortrait();

        skip.SetActive(true);
        for (int i = 0; i < dialougeObject.GetStrings().Length; i++)
        {
            string _string = dialougeObject.GetStrings()[i];
            yield return typeWriter.Run(_string, textLabel, 4);
            if (i == dialougeObject.GetStrings().Length - 1 && dialougeObject.HasResponses()) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        dialougeObject.GetEvent()?.Invoke();
        if (dialougeObject.HasResponses())
        {
            // portrait.enabled = false;
            skip.SetActive(false);

            responseHandler.ShowResponses(dialougeObject.GetResponses());
        }
        else
        {
            CloseDialouge();
        }
    }
    public void CloseDialouge()
    {
        dialougeBox.SetActive(false);
        textLabel.text = string.Empty;
        DialougeActive = false;

    }
}

public interface IDialougeObject
{
    public String[] GetStrings();
    UnityEvent GetEvent();

    bool HasResponses();
    void ShowPortrait();
    Response[] GetResponses();
}
