
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class ChangeDirectionSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Player> Player;
        public ComponentDataArray<Rotation> Rotation;
        public ComponentDataArray<Velocity> Velocity;
        public ComponentDataArray<PlayerInput> PlayerInput;

    }

    [Inject] private Data m_Data;

    public ChangeDirectionSystem()
    {

    }
    protected override void OnUpdate()
    {
        for (int index = 0; index < m_Data.Length; ++index)
        {
            float directionX = m_Data.PlayerInput[index].Direction;

            Velocity currentVelocity = m_Data.Velocity[index];

            m_Data.Velocity[index] = new Velocity
            {
                Value = currentVelocity.Value + 0.5f * (new float3(directionX, 0, 0))
            };

            Vector3 rotationDirection = new Vector3(0, 0, -directionX);
            m_Data.Rotation[index] = new Rotation
            {
                Value = Quaternion.LookRotation(rotationDirection, Vector3.up)
            };
        }
    }
}
