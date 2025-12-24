using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class UMS_Edit : MonoBehaviour
{
    [SerializeField] UnityMS ums_file;


    public int line_start, line_end;
    public float bpm;

    [ContextMenu("SaveEdit")]
    void SaveEdit()
    {
        if (ums_file == null) return;
        ums_file.LineCount = (line_end - line_start + 1);
        ums_file.BPM = bpm;

        Transform[] tnotes = FindObjectsByType<Transform>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        tnotes = (from note in tnotes
                  where note.gameObject.layer == LayerMask.NameToLayer("Note")
                  orderby note.position.y
                  select note).ToArray();

        List<MSNoteD> dnotes = new List<MSNoteD>();
        foreach (var item in tnotes)
        {
            MSNoteD ndata = new MSNoteD();

            int lineIndex = (int)item.position.x - line_start;
            int startY = (int)((item.position.y - (item.localScale.y / 2)) / onebeat_size);
            int endY = (int)((item.position.y + (item.localScale.y / 2)) / onebeat_size);

            ndata.startPos
            = new Vector2Int(lineIndex, startY);

            ndata.endPos
            = new Vector2Int(lineIndex, endY);

            ndata.width = (int)item.localScale.x;

            ndata.noteType = (item.localScale.y > onebeat_size) ? 1 : 0;//  if it is long note or not
            dnotes.Add(ndata);
        }
        ums_file.MSNotedD_Arr = dnotes.ToArray();
        ums_file.NoteCount = dnotes.Count;
        ums_file.Length = (dnotes[dnotes.Count - 1].endPos.y + 1) /bpm *60 ;
    }
    [SerializeField] Transform bpm_bar;
    public float onebeat_size = 0.2f;
    [ContextMenu("Refresh")]
    void Refresh()
    {
        bpm_bar.transform.position =
        new Vector2(0, bpm * onebeat_size);
    }
    [SerializeField] GameObject NotePrefap;
    [ContextMenu("CreateNote")]
    void CreateNote()
    {
        if (ums_file == null) return;
        var gnote = Instantiate(NotePrefap);
        float y = (ums_file.MSNotedD_Arr[ums_file.MSNotedD_Arr.Length - 1].endPos.y+1)*onebeat_size;
        gnote.transform.position
        = new Vector2(line_start, y - onebeat_size / 2);
        SaveEdit();
    }

    [SerializeField] Transform tTimebar;
    float time=0;
    [ContextMenu("Play")]
    void Play()
    {
        
    }
    
    [ContextMenu("Stop")]
    void Stop()
    {
        
    }
}
