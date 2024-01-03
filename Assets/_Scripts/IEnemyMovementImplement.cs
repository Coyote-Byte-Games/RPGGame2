using UnityEngine;

public interface IEnemyMovementImplement
{
    /// <summary>
    /// However this implement does it, finds the direction it should go in
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetPracticalDirection();
}
