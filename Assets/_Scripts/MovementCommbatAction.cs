using UnityEngine;

internal class MovementCommbatAction : ICombatAction
{
    //todo make this extendable
    int distance = 1;

    public TileTelegraphData GetTelegraphData()
    {
        return new TileTelegraphData { color = TileTelegraphVFXScript.Palette.Yellow, tileCoords = TileTelegraphVFXScript.ShapeUtil.LineFroToVertical(0, 0, 2), heading = Vector2.down };
    }
    public int CreditsRequired()
    {
       return 1;
    }

    
}
