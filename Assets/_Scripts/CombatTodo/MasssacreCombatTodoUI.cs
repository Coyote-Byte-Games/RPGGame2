using System;
using TMPro;
/// <summary>
/// Responsible for massacre UIs.
/// </summary>
public class MasssacreCombatTodoUI : CombatTodoUI
{
    public TMP_Text titleBox;
    public TMP_Text textBox;
    public void Awake()
    {
        
    }
    public override void Exterminate()
    {
        Destroy(gameObject);
    }

    public override void UpdateUI(int v)
    {
        textBox.text = $" \"Destroy\" {v} enemies.";
    }
}