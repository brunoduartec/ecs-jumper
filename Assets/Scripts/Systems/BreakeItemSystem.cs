
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

using Unity.Mathematics;

public class BreakeItemSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<CollisionComponent> Collision;
        public EntityArray Entities;

        public ComponentDataArray<BreakComponent> BreakComponent;

    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;
        for (int index = 0; index < m_Data.Length; ++index)
        {
            BreakComponent breakComponent = m_Data.BreakComponent[index];
            if (breakComponent.started == 0)
            {
                if (m_Data.Collision[index].direction.y > 0)
                {

                    m_Data.BreakComponent[index] = new BreakComponent
                    {
                        started = 1,
                        coolDown = breakComponent.coolDown
                    };
                }
            }
            else
            {
                float coolDown = m_Data.BreakComponent[index].coolDown;
                coolDown -= dt;
                if (coolDown <= 0.0f)
                {
                    PostUpdateCommands.DestroyEntity(m_Data.Entities[index]);
                }
                else
                {
                    m_Data.BreakComponent[index] = new BreakComponent
                    {
                        started = 1,
                        coolDown = coolDown
                    };
                }
            }
        }
    }
}
