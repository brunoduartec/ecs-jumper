
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

    private EntityManager _entityManager;
    public BroadphaseSystem()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();
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
                Value = 0
            };
            for (int id = 0; id < m_Data.Length; ++id)
            {
                if (index == id)
                    continue;

                if (checkAABB(m_Data.AABB[index], m_Data.AABB[id]))
                {
                    m_Data.Collision[index] = new CollisionComponent
                    {
                        Value = 1
                    };
                }
            }
        }
    }
}
