using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class SetDirtyEditor 
{
    public static void SetDirty(Object target)
    {
        EditorUtility.SetDirty(target);
    }

    public static string GetAssetPath(GameObject prefab)
    {
        return AssetDatabase.GetAssetPath(prefab);
    }

    public static AddressableAssetSettings GetDefaultSettings()
    {
        return AddressableAssetSettingsDefaultObject.Settings;
    }
    
    
}
