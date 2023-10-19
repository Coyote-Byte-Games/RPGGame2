using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReverseVerticalLayoutScript : MonoBehaviour
{
    private RectTransform rectTrans;
    // Start is called before the first frame update
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    void Update()
    {
        int childCount = rectTrans.childCount;
        //-1 to account for template
        transform.position = Vector3.up * (100 * (childCount - 1));
    }
}
