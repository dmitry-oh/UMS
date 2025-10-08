using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class UMS_Read : MonoBehaviour
{
    //Parameters
    float allowBeat = 0.5f;
    int maxCount = 10;
    double beat;
    bool almostEnd = false;
    bool End = false;
    bool Start = false;
    int End_Delay = 1;//in beat
    double End_Time;//Second
    double y_system;//-> second
    double y_delay;
    //Parameters
    Queue<MSNoteD> noteQueueAll = new Queue<MSNoteD>();
    Queue<MSNoteD> noteQueuePart = new Queue<MSNoteD>();

    int PartingQueue(int maxCount)
    {
        if (noteQueueAll.Count < maxCount)
        {
            foreach (var item in noteQueueAll)
            {
                EnqueueThatPart(item);
            }
            noteQueueAll.Clear();
            almostEnd = true;
            return -1;
        }
        else
        {
            for (int i = 0; i < maxCount; i++)
            {
                var item = noteQueueAll.Dequeue();
                EnqueueThatPart(item);
            }
        }

        return 0;
    }
    protected virtual void EnqueueAll(MSNoteD item)
    {
        noteQueueAll.Enqueue(item);
    }
    protected virtual void EnqueueThatPart(MSNoteD item)
    {
        noteQueuePart.Enqueue(item);
    }
    protected virtual void DequeueThatPart(int state)
    {
        noteQueuePart.Dequeue();
        if (noteQueuePart.Count == 0)
        {
            PartingQueue(maxCount);
        }
    }
    int RangeFlagger(MSNoteD item, double y)
    {
        y = y / beat;//system-> beat 
        int RangeFlag = -1;//-1> do not judge, 0>before judge, but it allowed 1> in Range ,2> Out Range, but it allowed 3> Out of range.
        if (y < item.startPos.y)
        {
            if (y >= item.startPos.y - allowBeat)
            {
                RangeFlag = 0;
            }
        }
        else
        {
            if (y > item.endPos.y)
            {
                if (y <= item.endPos.y + allowBeat)
                {
                    RangeFlag = 2;
                }
                else
                {
                    RangeFlag = 3;
                }
            }
            else
            {
                RangeFlag = 1;
            }
        }
        return RangeFlag;
    }
    void ProcessPartedNote(double y)
    {
        var item = noteQueuePart.Peek();
        int RangeFlag = RangeFlagger(item, y);
        if (RangeFlag < 0) return;
        if (RangeFlag == 3)
        {
            //Out Range
            DequeueThatPart(-1);
            return;
        }
        int state = 0;//-1: out range, 0:no hit 1: hit in half 2:hit perfect

        if ((MSNoteDType)item.noteType == MSNoteDType.one_hit)
        {
            bool judge = Judge(item);
            if (judge)
            {
                if (RangeFlag == 1)
                {
                    state = 1;
                }
                else
                {
                    state = 2;
                }
            }
        }
        else if ((MSNoteDType)item.noteType == MSNoteDType.long_hit)
        {
            bool judge = Judge(item);

            if (RangeFlag == 2)
            {
                LongHit_Exit_AE(item.startPos.x, 2);
                state = 2;
            }
            else if (RangeFlag > 0)
            {
                if (!judge)
                {
                    LongHit_Exit_AE(item.startPos.x, 1);
                    state = 1;
                }
                else
                {
                    LongHit_On_AE(item.startPos.x, item.endPos.y - (int)(y / beat));
                }
            }
            else
            {
                LongHit_Encounter_AE(item.startPos.x);
            }
        }


        if (state > 0)
        {
            DequeueThatPart(state);
        }
    }
    //AE: AFTER EFFECT//
    protected virtual void OneHit_AE(int x, int state)
    {

    }

    protected virtual void LongHit_Encounter_AE(int x)
    {

    }
    protected virtual void LongHit_On_AE(int x, int y_relative)
    {

    }
    protected virtual void LongHit_Exit_AE(int x, int state)
    {

    }
    //              //
    protected virtual bool Judge(MSNoteD item)
    {
        return false;
    }
    protected virtual void EndMSRead()
    {
        if (almostEnd)
        {
            if (y_system >= End_Time + End_Delay * beat)
            {
                //end
                End = true;
            }
        }
    }

    protected virtual void ReadMS(string path)
    {
        AsyncOperationHandle<UnityMS> handle = Addressables.LoadAssetAsync<UnityMS>(path);
        handle.Completed += (op) =>
        {
            ReadOpEnd(op.Result);
        };
    }
    double BPM2BEAT(float bpm)
    {
        return 1 / (bpm / 60);//or spb
    }
    protected virtual void ReadOpEnd(UnityMS result)
    {
        y_system = 0;
        y_delay = Time.time;
        beat = BPM2BEAT(result.BPM);
        Start = true;
        End_Time = result.Length;

        foreach (var item in result.MSNotedD_Arr)
        {
            EnqueueAll(item);
        }
    }

    protected virtual void TickProcess()
    {
        if (End || !Start) return;
        y_system = Time.time - y_delay;
        if (almostEnd)
        {
            EndMSRead();
            if (End) return;
        }
        ProcessPartedNote(y_system);
    }
}
