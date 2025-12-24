using UnityEngine;
using UnityEditor;
using System.IO;

public class TextAssetContextMenu
{
    // Assets 폴더의 모든 파일 대상
    [MenuItem("Assets/UMS로변환", false, 20)]
    private static void OpenBmsFile()
    {
        Object selected = Selection.activeObject;
        UMSConverter.UMS2BMS(selected as TextAsset);
    }
    // .bms 또는 .txt 확장자일 때만 메뉴 활성화
    [MenuItem("Assets/UMS로변환", true)]
    private static bool OpenBmsFileValidate()
    {
        return ValidateFileExtension(".txt",".bms");
    }

    // 확장자를 매개변수로 받아서 여러 확장자 지원
    private static bool ValidateFileExtension(string re1)//req extension
    {
        if (Selection.activeObject is TextAsset textAsset)
        {
            string assetPath = AssetDatabase.GetAssetPath(textAsset);
            string extension = Path.GetExtension(assetPath).ToLower();
            return extension == re1.ToLower();
        }
        return false;
    }

    private static bool ValidateFileExtension(string re1,string re2)//req extension
    {
        if (Selection.activeObject is TextAsset textAsset)
        {
            string assetPath = AssetDatabase.GetAssetPath(textAsset);
            string extension = Path.GetExtension(assetPath).ToLower();
            return extension == re1.ToLower() || extension ==re2.ToLower();
        }
        return false;
    }
}