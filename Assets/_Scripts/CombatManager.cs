using System;
using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneTransitioner;
public class CombatManager : Singleton<CombatManager>
{
    //We want to store a clock within this class and notify everything whenever the clock finishes.
    public float ClockDuration;
    public delegate void ClockCycleAction();
    public event ClockCycleAction clockCycleEnd;
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(ClockCycle());
    }
    public void Start()
    {
        CombatTodoManager.instance.OnAllTodosDone += EndCombat;
    }

    public void EndCombat()
    {
        SceneTransitioner.GetInstance().ToOW();
    }

    private IEnumerator ClockCycle()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(ClockDuration);
            clockCycleEnd?.Invoke();

        }
    }
}