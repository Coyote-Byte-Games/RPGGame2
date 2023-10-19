using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
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
    public DialougeSO testDialougeSO;
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
    public void ShowDialouge(DialougeSO dialougeObject)
    {

        DialougeActive = true;
        dialougeBox.SetActive(true);

        StartCoroutine(StepThroughDialouge(dialougeObject));
    }

    private IEnumerator StepThroughDialouge(DialougeSO dialougeObject)
    {
        if (dialougeObject.HasPortrait)
        {
            portrait.enabled = true;
            portrait.sprite = dialougeObject.portrait;
            nameText.text = dialougeObject.characterName;
            
        }
        skip.SetActive(true);
        for (int i = 0; i < dialougeObject.dialouges.Length; i++)
        {
            string dialouge = dialougeObject.dialouges[i];
            yield return typeWriter.Run(dialouge, textLabel, 4);
            if (i == dialougeObject.dialouges.Length - 1 && dialougeObject.HasResponses()) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        }
        if (dialougeObject.HasResponses())
        {
            // portrait.enabled = false;
            skip.SetActive(false);

            responseHandler.ShowResponses(dialougeObject.responses);
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
