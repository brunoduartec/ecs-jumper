
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Mathematics;


[UpdateAfter(typeof(ComputeColliderAABBSystem))]
public class BroadphaseSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<AABBComponent> AABB;
        public ComponentDataArray<CollisionComponent> Collision;
    }

    [Inject] private Data m_Data;

    public BroadphaseSystem()
    {
    }

    private float3 getCollisionDirection(AABBComponent a, AABBComponent b)
    {
        float3 direction = float3.zero;

        if (a.center.y > b.center.y)
        {
            return new float3(0, -1, 0);
        }
        else if (a.center.y < b.center.y)
        {
            return new float3(0, 1, 0);
        }
        else if (a.center.x > b.center.x)
        {
            return new float3(-1, 0, 0);
        }
        else if (a.center.x < b.center.x)
        {
            return new float3(1, 0, 0);
        }

        return direction;
    }
    private bool checkAABB(AABBComponent a, AABBComponent b)
    {
        bool x = Mathf.Abs(a.center[0] - b.center[0]) <= (a.halfwidths[0] + b.halfwidths[0]);
        bool y = Mathf.Abs(a.center[1] - b.center[1]) <= (a.halfwidths[1] + b.halfwidths[1]);
        bool z = Mathf.Abs(a.center[2] - b.center[2]) <= (a.halfwidths[2] + b.halfwidths[2]);

        return x && y && z;
    }


    protected override void OnUpdate()
    {
        for (int index = 0; index < m_Data.Length; ++index)
        {
            m_Data.Collision[index] = new CollisionComponent
            {
                direction = float3.zero
            };

            for (int id = 0; id < m_Data.Length; ++id)
            {
                if (index == id)
                    continue;
                AABBComponent a = m_Data.AABB[index];
                AABBComponent b = m_Data.AABB[id];

                if (checkAABB(a, b))
                {

                    m_Data.Collision[index] = new CollisionComponent
                    {
                        direction = getCollisionDirection(a, b)
                    };
                }
            }
        }
    }
}
