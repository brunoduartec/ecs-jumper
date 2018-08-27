
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

using Unity.Mathematics;


public class ComputeColliderAABBSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<AABBComponent> AABB;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        for (int index = 0; index < m_Data.Length; ++index)
        {
            var position = m_Data.Position[index].Value;
            float size = 2.5f;

            AABBComponent aabb = new AABBComponent
            {
                center = position,
                halfwidths = new float3(size, size, size)
            };
            m_Data.AABB[index] = aabb;
        }
    }
}
