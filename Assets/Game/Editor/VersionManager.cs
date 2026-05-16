#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class VersionManager :
#if UNITY_EDITOR
    IPreprocessBuildWithReport
#endif
{
    public int callbackOrder => 0;

#if UNITY_EDITOR
    public void OnPreprocessBuild(BuildReport report)
    {
        // Повышаем номер патча (1.0.5 -> 1.0.6)
        string[] parts = Application.version.Split('.');
        if (parts.Length == 3 && int.TryParse(parts[2], out int patch))
        {
            patch++;
            string newVersion = $"{parts[0]}.{parts[1]}.{patch}";
            PlayerSettings.bundleVersion = newVersion;
            Debug.Log($"Version bumped to {newVersion}");
        }
    }
#endif
}