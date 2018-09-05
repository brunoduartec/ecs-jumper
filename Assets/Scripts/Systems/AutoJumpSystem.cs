
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
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<Velocity> Velocity;
        public ComponentDataArray<Jump> Jump;
        public ComponentDataArray<CollisionComponent> CollisionComponent;
    }

    [Inject] private Data m_Data;

    public struct ScoreData
    {
        public readonly int Length;
        public ComponentDataArray<MaxHeight> MaxHeight;
    }

    [Inject] private ScoreData m_Score;

    public struct Job : IJobParallelFor
    {
        public Data data;
        public ScoreData score;

        public void Execute(int index)
        {
            if (data.Velocity[index].Value.y < 0)
            {
                if (data.CollisionComponent[index].direction.y < 0)
                {
                    float jumpVelocityY = data.Jump[index].Value;
                    float3 velocity = new float3(0, jumpVelocityY, 0);

                    data.Velocity[index] = new Velocity { Value = velocity };
                }

            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Job job = new Job()
        {
            data = m_Data,
            score = m_Score
        };
        return job.Schedule(m_Data.Length, 64, inputDeps);
    }
}
