using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UMSReader : MonoBehaviour
{
    /*
    한마디가 넘어갈때마다 읽음. 
    48분절기준으로 몇번 정확한 입력을 받았는지 체크
    */
    UMSObject ums;
    void Init(string path)
    {
        //ums 세팅
    }
    List<DataValue> curDatas =new List<DataValue>();//한 마디
    double bpm;
    int beat=0;
    void ReadAtBar()
    {
        //curDatas저장
        curDatas=ums.dataQueue.DequeueList();
        var bpm_change = curDatas.Find(v=>v.ch==9);
        var bpm_change_stop = curDatas.Find(v=>v.ch==10);
        if (bpm_change != null)
        {
            bpm=double.Parse(bpm_change.data);
        }
        if (bpm_change_stop != null)
        {
            bpm=ums.header.bpm;
        }

        //short채보 long채보 초기화

        shorts=curDatas.Where(v=>v.ch>0&&v.ch<8).ToList();
        longs=curDatas.Where(v=>v.ch>50&&v.ch<58).ToList();

        //롱노트 인풋 초기화

        longnote_input.Clear();
        for(int i = 0; i < longs.Count; i++)
        {
            longnote_input.Add(false);
        }
    }

    List<bool> longnote_input =new List<bool>();
    List<DataValue> shorts,longs;
    void ReadAtBeat()
    {
        //숏노트 판정
        int short_cnt=0;
        foreach (var item in shorts)
        {
            //매핑된 키를 입력했는지 체크
            bool is1=item.data[beat].CompareTo('1')==1;
            if (is1)
            {
                
            }
        }
    }
}