using UnityEngine;
public class MSNoteD //music system's note data
{
    public Vector2Int startPos, endPos; //y is axis of beat, x is axis of line
    public int size;
    public int noteType;
    public int noteTag;
}

public enum MSNoteDType
{
    one_hit,long_hit   
}

public enum MSNoteDTag
{
    tag0=0,
    tag1=1
}