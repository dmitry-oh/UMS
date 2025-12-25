using System.Collections.Generic;
using UnityEngine;

public class UMSEdit_UI : MonoBehaviour
{
    //1마디의 길이:900, -300부터 시작, 한 채널길이 150
    //1마디->48개 분절
    [SerializeField] Transform BeatParent;
    [SerializeField] GameObject pShort;
    List<RectTransform> object_pool=new List<RectTransform>();
    void Pooling()
    {
        if (object_pool.Count>0)
        {
            foreach (var item in object_pool)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            if(BeatParent.childCount>0){
                for(int i = 0; i < BeatParent.childCount; i++)
                {
                    BeatParent.GetChild(i).gameObject.SetActive(false);
                    object_pool.Add((RectTransform)BeatParent.GetChild(i));
                }
            }
            /*
            for(int i = 0; i < 100; i++)
            {
                var obj=Instantiate(pShort);
                obj.transform.SetParent(BeatParent);
                object_pool.Add((RectTransform)obj.transform);
                obj.SetActive(false);
            }
            */
        }
    }
    void PoolLoad(int i,Vector2 anchPos)
    {
        object_pool[i].gameObject.SetActive(true);
        object_pool[i].anchoredPosition=anchPos;
    }
    float bar_distance=45,ch_distance=150,ch_offset=-300;
    public void BarInit(List<DataValue> bars)
    {
        Pooling();
        int k=0;
        for(int i = 0; i < bars.Count; i++)
        {
            int ch=bars[i].ch;
            if(ch>5)continue;
            for(int j = 0; j < bars[i].data.Length; j++)
            {
                bool active = bars[i].data[j]=='1';
                if (active)
                {
                    Vector2 pos =new Vector2(ch_offset+ch_distance*(ch-1),bar_distance*j);
                    PoolLoad(k,pos);
                    k++;
                }
            }
        }
    }
}
