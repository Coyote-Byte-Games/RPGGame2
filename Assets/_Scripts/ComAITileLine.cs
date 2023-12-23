using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAITileLine : MonoBehaviour
{
    public TileTelegraphVFXScript vFXScript;
    void Start()
    {
        //later we'll do lifecycle
        var shape = vFXScript.CreateShape(TileTelegraphVFXScript.Palette.Yellow, TileTelegraphVFXScript.GetShape(TileTelegraphVFXScript.DefaultShape.FOUR_LONG));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    }
