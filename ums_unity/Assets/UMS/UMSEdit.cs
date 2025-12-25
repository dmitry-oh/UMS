using System.Collections.Generic;
using UnityEngine;

public class UMSEdit : MonoBehaviour
{
    int cur_bar=0;
    [SerializeField]UMSObject ums;
    public void Init(UMSObject ums)
    {
        cur_bar=0;
        this.ums=ums;
        if(edit_ui==null)edit_ui=GetComponent<UMSEdit_UI>();
        LoadBar(cur_bar);
    }
    public void UpBar()
    {
        if(edit_ui==null)edit_ui=GetComponent<UMSEdit_UI>();
        cur_bar++;
        LoadBar(cur_bar);
    }
    public void DownBar()
    {
        if(edit_ui==null)edit_ui=GetComponent<UMSEdit_UI>();
        cur_bar--;
        if(cur_bar<=0)cur_bar=0;
        LoadBar(cur_bar);
    }
    UMSEdit_UI edit_ui;
    public void LoadBar(int bar)
    {
        var bars=DequeueBar(bar);
        edit_ui.BarInit(bars);
    }
    public List<DataValue> DequeueBar(int bar)
    {
        return ums.dataQueue.DequeueListBar(bar);
    }
}
