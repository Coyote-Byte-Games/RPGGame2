using System;
using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
public class CombatManager : Singleton<CombatManager>
{
    //We want to store a clock within this class and notify everything whenever the clock finishes.
    [SerializeField] float clockDuration;
    public delegate void ClockCycleAction();
    public event ClockCycleAction clockCycleEnd;
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(ClockCycle());
    }
    private IEnumerator ClockCycle()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(clockDuration);
            clockCycleEnd?.Invoke();
            Debug.Log("clock cycle triggered");

        }
    }
}