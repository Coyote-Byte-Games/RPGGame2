using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static InventoryScript;

public static class SaveDataHelper
{
    public static string InventorySavePath => Application.persistentDataPath + "/inventory.save";
 public static void SaveInventory(InventoryInstanceData inventory)
    {
        Debug.Log($"Saving inventory to file @ {InventorySavePath}");
        var stream = new FileStream(InventorySavePath, FileMode.Create, FileAccess.Write);
        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Serialize(stream, inventory);
        stream.Close();
        Debug.Log(InventorySavePath);
    }

    public static InventoryInstanceData LoadInventory()
    {
        Debug.Log($"Loading inventory from file @ {InventorySavePath}");
        if (File.Exists(InventorySavePath))
        {
            var stream = new FileStream(InventorySavePath, FileMode.Open, FileAccess.Read);
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (InventoryInstanceData)formatter.Deserialize(stream);
            stream.Close();
        }
        else { Debug.LogError("Save file does not exist!"); }
        return null;
    }
}