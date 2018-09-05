
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

using Unity.Jobs;

using Unity.Mathematics;


public class ComputeColliderAABBSystem : JobComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<Collider> Collider;
        public ComponentDataArray<AABBComponent> AABB;
    }

    [Inject] private Data m_Data;

    public struct Job : IJobParallelFor
    {
        public Data data;
        public void Execute(int index)
        {
            var position = data.Position[index].Value;
            float size = data.Collider[index].size;

            AABBComponent aabb = new AABBComponent
            {
                center = position,
                halfwidths = new float3(size, size, size)
            };
            data.AABB[index] = aabb;
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
