using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System.ComponentModel.Design;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Linq;
public class TextAssetContextMenu
{
    // Assets 폴더의 모든 파일 대상
    [MenuItem("Assets/TXT를UMS로변환", false, 20)]
    private static void OpenTxtFile()
    {
        Object selected = Selection.activeObject;
        UMSConverter.TXT2UMS(selected as TextAsset);
    }
    // .txt 확장자일 때만 메뉴 활성화
    [MenuItem("Assets/TXT를UMS로변환", true)]
    private static bool OpenTxtFileValidate()
    {
        return ValidateFileExtension(".txt");
    }

    [MenuItem("Assets/BMS를UMS로변환", false, 21)]
    private static void OpenBmsFile()
    {
        string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string fullPath = Path.Combine(Application.dataPath, assetPath.Replace("Assets/", ""));
        if (File.Exists(fullPath))
        {
            string content = File.ReadAllText(fullPath);
            UMSConverter.BMS2UMS(content);
        }
    }
    // .bms 확장자일 때만 메뉴 활성화
    [MenuItem("Assets/BMS를UMS로변환", true)]
    private static bool OpenBmsFileValidate()
    {
        return ValidateFileExtension(".bms");
    }
    // UMS파일일때만 에딧 씬 열기
    
    [MenuItem("Assets/UMS파일편집", false, 22)]
    private static void OpenUMS()
    {
       var scene = EditorSceneManager.OpenScene("Assets/Scene/scene.unity");
       var objs =scene.GetRootGameObjects();
       for(int i = 0; i < objs.Length; i++)
        {
            if(objs[i].TryGetComponent(out UMSEdit edit))
            {
                edit.Init(Selection.activeObject as UMSObject);
            }
        }
    }
    [MenuItem("Assets/UMS파일편집", true)]
    private static bool OpenUMSValidate()
    {
        return ValidateUMSFile();
    }
    private static bool ValidateUMSFile()
    {
        return Selection.activeObject is UMSObject;
    }
    private static bool ValidateFileExtension(string re1)//req extension
    {
        string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string extension = Path.GetExtension(assetPath).ToLower();
        return extension == re1.ToLower();
    }

    private static bool ValidateFileExtension(string re1,string re2)//req extension
    {

        string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string extension = Path.GetExtension(assetPath).ToLower();
        return extension == re1.ToLower() || extension ==re2.ToLower();
    }
}