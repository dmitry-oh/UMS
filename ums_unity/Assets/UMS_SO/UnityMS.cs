using UnityEngine;

[CreateAssetMenu(fileName = "new UMS", menuName = "Create UMS", order = 0)]
public class UnityMS : ScriptableObject
{
    public float BPM;
    public float Length;

    public AudioClip source;
    public Sprite Jacket;
    public string artist;
    public string label;
    
    public int LineCount;
    public int NoteCount;
    public MSNoteD[] MSNotedD_Arr;
}