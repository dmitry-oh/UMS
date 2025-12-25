using UnityEngine;
using UnityEditor;
public static class UMSConverter
{
    public static void BMS2UMS(string file)
    {
        
        UMSObject ums = ScriptableObject.CreateInstance<UMSObject>();
        if (ums == null)
        {
            Debug.Log("Failed to create ScriptableObject instance");
            return;
        }
        Header header= ums.header;
        string[] lines = file.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)||line[0]!='#')
                continue;

            string d = line.Substring(1);
            string[] values = d.Split(' ');
            if (values.Length > 1)
            {
                //헤더인경우
                string head = values[0];
                string value = values[1];
                switch (head)
                {
                    case "TITLE":
                    header.title=value;
                    break;
                    case "ARTIST":
                    header.artist=value;
                    break;
                    case "GENRE":
                    header.genre=value;
                    break;
                    case "BPM":
                    header.bpm=double.Parse(value);
                    break;
                    case "PLAYLEVEL":
                    header.playlevel=int.Parse(value);
                    break;
                    case "COMMENT":
                    header.comment=value;
                    break;
                    case "KEYCNT":
                    header.keycnt=int.Parse(value);
                    break;
                    default:
                    header.SetValue(head,value);
                    break;
                }
            }
            else
            {
                //데이터인경우
                DataValue dv=new DataValue();
                string[] datarow = d.Split(':');
                dv.bar=int.Parse(datarow[0].Substring(0,3));
                dv.ch=int.Parse(datarow[0].Substring(3,2));
                dv.data=datarow[1];
                ums.dataQueue.Enqueue(dv);
            }
        }

        string path = $"Assets/{ums.header.title}_ums.asset";
        AssetDatabase.CreateAsset(ums, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();  // 추가
    }
    public static void TXT2UMS(TextAsset asset)
    {
        BMS2UMS(asset.text);
    }
}
