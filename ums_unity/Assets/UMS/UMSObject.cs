using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UMSObject : ScriptableObject
{
    //Header Section
    public Header header=new Header();
    //Data Section
    public DataQueue dataQueue=new DataQueue();
    
}

[System.Serializable]
public class Header//메타데이터
{
    public string title,artist,genre,comment;
    public double bpm;
    public int playlevel,keycnt;
    public List<KeyValue> keyValues= new List<KeyValue>();

    public T GetValue<T>(string key) where T : UnityEngine.Object
    {
        foreach (var item in keyValues)
        {
            if (item.key.CompareTo(key)==1)
            {
                return item.value as T;
            }
        }
        return default(T);
    }
    public void SetValue(string key,string path)
    {
        //실제파일을 불러오지않고 상대경로만 저장
        KeyValue keyValue=new KeyValue();
        keyValue.key=key;
        keyValue.path=path;
        keyValues.Add(keyValue);
    }
}
[System.Serializable]
public class KeyValue
{
    public string key;
    public string path;
    public UnityEngine.Object value;
}

[System.Serializable]
public class DataQueue
{
    public int cur_bar=0;//현재 진행한 bar
    public List<DataValue> datas =new List<DataValue>();//RawData의 복제품
    public void Enqueue(DataValue value)
    {
        datas.Add(value);
        datas.Sort((a,b)=>a.bar.CompareTo(b.bar));
        //bar우선순위정렬
    }
    public void EditValue(DataValue value)
    {
        var dv = datas.Find(v=>v.bar==value.bar&&v.ch==value.ch);
        dv.data=value.data;
    }
    public void RemoveValue(int bar,int ch)
    {
        var dv = datas.Find(v=>v.bar==bar&&v.ch==ch);
        datas.Remove(dv);
    }
    public List<DataValue> DequeueList()
    {
        var value = datas.Where(v=>v.bar==cur_bar).ToList();
        cur_bar++;
        return value;
    } 
    public List<DataValue> DequeueListBar(int bar)
    {
        var value = datas.Where(v=>v.bar==bar).ToList();
        return value;
    } 
    public int GetLastBar()
    {
        return datas.Last().bar;
    }
}

[System.Serializable]
public class DataValue
{
    public int bar;//0~999
    public int ch;//채널
    public string data;//데이터
}