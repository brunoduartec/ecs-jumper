
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;

using Unity.Mathematics;


[UpdateAfter(typeof(ComputeColliderAABBSystem))]
public class AutoJumpSystem : JobComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Player> Player;
        public ComponentDataArray<PlayerInput> PlayerInput;
        public ComponentDataArray<Velocity> Velocity;
        public ComponentDataArray<CollisionComponent> CollisionComponent;
    }

    [Inject] private Data m_Data;

    public struct Job : IJobParallelFor
    {
        public Data data;
        public void Execute(int index)
        {
            if (data.CollisionComponent[index].Value > 0)
            {
                float3 velocity = new float3(0, 30, 0);

                data.Velocity[index] = new Velocity { Value = velocity };
            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Job job = new Job()
        {
            data = m_Data
        };
        return job.Schedule(m_Data.Length, 64, inputDeps);
    }
}
