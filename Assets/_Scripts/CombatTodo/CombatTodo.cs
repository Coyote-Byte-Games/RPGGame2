using System;
using UnityEngine;

/* 
main job is to provide an interface for all classes that are todos 
Contains data for the requirements for the todo to be finished
The quest is defined by another object that tells us when to update the quest. The implementation 
is responsible for defining when the quest will be finished. We'll start by defining each instance "manually," 
 */
public abstract class CombatTodo : MonoBehaviour
{
    public abstract void UpdateTodo();

    internal abstract bool IsCommplete();

    public event Action _OnTodoComplete;
    public void OnTodoComplete()
    {
        _OnTodoComplete?.Invoke();

    }

}