using Unity.Entities;
using Unity.Burst;

public struct SongTimer : IComponentData
{
    public double startTime,curTime;
}