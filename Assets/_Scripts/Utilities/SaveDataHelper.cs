using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static InventoryScript;
using static UnitEquipmentScript;

public static class SaveDataHelper
{
    public static string InventorySavePath => Application.persistentDataPath + "/inventory.save";
    public static string TempEquipmentSavePath => Application.persistentDataPath + "/equipment.save";
    public static void SaveInventory(InventoryInstanceData inventory)
    {
        SaveSerialized(InventorySavePath, inventory);
    }
    // public static void SaveEquipment(UnitEquipmentScript equipment)
    // {
    //     //equipment uses a special method
    //     var stream = new FileStream(InventorySavePath, FileMode.Create, FileAccess.Write);
    //     var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
    //     formatter.Serialize(stream, equipment);
    //     stream.Close();
    // }


    public static InventoryInstanceData LoadInventory()
    {
        return LoadSerialized<InventoryInstanceData>(InventorySavePath);

    }

    // public static EquipmentData LoadEquipmentData()
    // {
    //     return LoadSerialized<EquipmentData>(TempEquipmentSavePath);
    // }

    private static T? LoadSerialized<T>(string path)
    {
        Debug.Log($"Loading inventory from file @ {InventorySavePath}");
        if (File.Exists(InventorySavePath))
        {
            var stream = new FileStream(InventorySavePath, FileMode.Open, FileAccess.Read);
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)formatter.Deserialize(stream);
            stream.Close();
        }
        else { Debug.LogError("Save file does not exist!"); }
        return default;
    }


    private static void SaveSerialized(string path, object saved)
    {
        Debug.Log($"Saving inventory to file @ {InventorySavePath}");
        var stream = new FileStream(InventorySavePath, FileMode.Create, FileAccess.Write);
        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Serialize(stream, saved);
        stream.Close();
    }


}