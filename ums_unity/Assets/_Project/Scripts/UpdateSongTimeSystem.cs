using Unity.Entities;
using Unity.Burst;
using UnityEngine; // AudioSettings 사용 위해 필요

[BurstCompile]
public partial struct RhythmUpdateSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        
        if (SystemAPI.TryGetSingletonRW<SongTimer>(out var timer))
        {
            timer.ValueRW.curTime = AudioSettings.dspTime-timer.ValueRO.startTime;
        }
    }
}