
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

using Unity.Mathematics;

[UpdateAfter(typeof(BroadphaseSystem))]
public class RidigBodySystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<Rotation> Rotation;
        public ComponentDataArray<CollisionComponent> Collision;
        public ComponentDataArray<RigidBodyComponent> RigidBody;


        public ComponentDataArray<Velocity> Velocity;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;
        for (int index = 0; index < m_Data.Length; ++index)
        {
            float3 gravity = new float3(0, -20, 0);

            var position = m_Data.Position[index].Value;
            if (m_Data.Collision[index].Value > 0)
            {
                gravity = float3.zero;
            }

            Velocity velocity = new Velocity
            {
                Value = m_Data.Velocity[index].Value + gravity * dt
            };
            m_Data.Velocity[index] = velocity;

            position += (m_Data.Velocity[index].Value * dt + 0.5f * gravity * dt * dt);

            m_Data.Position[index] = new Position { Value = position };
        }
    }
}
