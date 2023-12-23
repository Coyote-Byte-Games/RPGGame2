using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEditor;

public class ResponseHandler : MonoBehaviour
{
    public RectTransform responseTemplate;
    public RectTransform responseHolder;
    public RectTransform responseBox;

    DialougeManager manager;
    private List<GameObject> tempResponseButtons = new();
    public void Start()
    {
        manager = GetComponent<DialougeManager>();

    }
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0f;
        foreach (var item in responses)
        {
            GameObject button = Instantiate(responseTemplate.gameObject, responseHolder);
            button.SetActive(true);
            button.GetComponent<TMP_Text>().SetText(item.responseText);
            button.GetComponentInChildren<Button>().onClick.AddListener(() => OnPickedResponse(item));

            tempResponseButtons.Add(button);

            responseBoxHeight += responseTemplate.sizeDelta.y;
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }
    private void OnPickedResponse(Response response)
    {

        foreach (var item in tempResponseButtons)
        {
            Destroy(item);
        }
        tempResponseButtons.Clear();
                responseBox.gameObject.SetActive(false);
        manager.ShowDialouge(response.dialouge);

    }

}
