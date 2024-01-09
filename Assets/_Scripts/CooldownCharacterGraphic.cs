using System;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class CooldownCharacterGraphic : MonoBehaviour
{

    float percentage;
    [SerializeField] Shader shader;
    public string propName = "_FillPercent";
    public Renderer render;
    [SerializeField] Material mat;
    [SerializeField] Material innerMat;
    //the seconds taken off the cooldown
    public float secondsPassed = 0;
    //the goal seconds to charge up to
    public float secondsTotal = 0;
    /// <summary>
    /// Sets the duration of this cooldown and resets it to empty.
    /// </summary>
    /// <param name="seconds"></param>
    public void SetCooldown(float seconds)
    {
        this.secondsTotal = seconds;
        this.secondsPassed = 0;
        // block.SetFloat( propName, 0);

        render.material.SetFloat(propName, 0);
    }
    //! PLEASE PLEASE MAKE THIS IN THE EDITORS
    public void Awake()
    {
        // innerMat = new Material(mat);
        render.material = new Material(shader);
    }

    public void Update()
    {
        this.secondsPassed += Time.deltaTime;
        percentage = Mathf.Clamp(this.secondsPassed / secondsTotal, 0, 1.0f);
        // this.SetPercentage(percentage);
        Debug.Log("AAAAAAAAAAAH MY SKIIIIIIIN " + render.material.HasFloat(propName));
        // block.SetFloat(propName, percentage);
        render.material.SetFloat(propName, percentage);
    }

    internal bool IsDone()
    {
        return percentage >= 1.0;
    }
}