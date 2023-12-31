using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//Main purpose is to display the shape and color of a tile display
public class TileTelegraphVFXScript : MonoBehaviour
{
  public CombatGlobalInfoSO combInfo;
  public GameObject tilePrefab;

  public GameObject CreateShape(Color color, Vector2Int[] tileCoords)
  {
    //For each coordinate, we create a tile based off of that coordingate
    GameObject[] tileGameobjects = new GameObject[tileCoords.Length];
    GameObject output = Instantiate(original: new GameObject(), position: transform.position, rotation: quaternion.identity);
    for (int i = 0; i < tileCoords.Length; i++)
    {
      tileGameobjects[i] = Instantiate(tilePrefab, (Vector3)((Vector2)tileCoords[i] * combInfo.CombatStepDistance) + transform.position, Quaternion.identity, output.transform);
      tileGameobjects[i].GetComponent<SpriteRenderer>().color = color;
    }
    //return
    return output;
  }

  public GameObject CreateShape(TileTelegraphData data)
  {
    return CreateShape(data.color, data.tileCoords);
  }

  /// <summary>
  /// This method is mainly used to receive shape templates. It might need to be rotated by the implementer.
  /// </summary>
  /// <returns></returns>
  public static Vector2Int[] GetShape(DefaultShape shape)
  {
    return defaultShapes[(int)shape];
  }
  public enum DefaultShape
  {
    NONE,
    ONE_LONG,
    TWO_LONG,
    THREE_LONG,
    FOUR_LONG
  }
  private static Vector2Int[][] defaultShapes = new Vector2Int[][]
  {
    new Vector2Int[]{},//none
    new Vector2Int[]{Vector2Int.down},//one
    new Vector2Int[]{Vector2Int.down, Vector2Int.down * 2},//two
    new Vector2Int[]{Vector2Int.down, Vector2Int.down * 2, Vector2Int.down * 3},//three
    new Vector2Int[]{Vector2Int.down, Vector2Int.down * 2, Vector2Int.down * 3,Vector2Int.down * 4 }//four
  };
  public static class ShapeUtil
  {
    public static Vector2Int Rotate(Vector2 v, float delta)
    {
      return new Vector2Int(
          (int)(v.x * Mathf.Cos(delta * Mathf.Deg2Rad) - v.y * Mathf.Sin(delta * Mathf.Deg2Rad)),
          (int)(v.x * Mathf.Sin(delta * Mathf.Deg2Rad) + v.y * Mathf.Cos(delta * Mathf.Deg2Rad))
      );
    }
    public static Vector2Int[] LineFroToVertical(int x, int startY, int endY)
    {
      //todo reshape this algorithm to work with both directions. Currently only goes in one direction
      var length = Mathf.Abs(endY - startY);
      var sign = MathF.Sign(endY - startY);
      Debug.Log("Length of the McDonalds Sea Joy Meal is " + length);
      var output = new Vector2Int[length];
      for (int i = 0; i < length; i++)
      {
        output[i] = new Vector2Int(x, startY + i * sign);
      }
      return output;
    }
  }
  public static class Palette
  {
    public static Color32 Red => new Color(0.95f, .3f, .3f, 0.5f);
    public static Color32 Yellow => new Color(0.988f, 0.906f, 0.533f, 0.5f);
  }


}
public struct TileTelegraphData
{
  public Color color; public Vector2Int[] tileCoords; public Vector2 heading;
}