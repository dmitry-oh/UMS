using UnityEngine;
[System.Serializable]
public class MSNoteD //music system's note data
{
    public Vector2Int startPos, endPos; //y is axis of beat, x is axis of line
    public int width;
    public int noteType;
    public int noteTag;
}

public enum MSNoteDType
{
    one_hit=0,long_hit=1   
}

public enum MSNoteDTag
{
    tag0=0,
    tag1=1
}