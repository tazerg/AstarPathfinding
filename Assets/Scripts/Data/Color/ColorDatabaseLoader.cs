using UnityEditor;

public static class ColorDatabaseLoader
{
    private const string ColorsInfoPath = "Assets/Data/ColorsInfo.asset";
    
    public static IColorDatabase LoadDatabase()
    {
        var colorsInfo = AssetDatabase.LoadAssetAtPath<ColorsInfo>(ColorsInfoPath);
        return new ColorDatabase(colorsInfo);
    }
}