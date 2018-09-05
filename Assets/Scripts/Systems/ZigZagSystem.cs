
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;

using Unity.Mathematics;

public class ZigZagSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<ZigZagMoveable> ZigZagMoveable;
        public ComponentDataArray<Position> Position;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int index = 0; index < m_Data.Length; ++index)
        {
            ZigZagMoveable zigzag = m_Data.ZigZagMoveable[index];

            float speed = 0;

            if (zigzag.CurrentPosition >= zigzag.Amplitude)
            {
                zigzag.Direction = -1;
            }
            else if (zigzag.CurrentPosition <= -zigzag.Amplitude)
            {
                zigzag.Direction = 1;
            }

            speed = zigzag.Direction * zigzag.Speed;


            float3 currentPosition = m_Data.Position[index].Value;
            float deltaPosition = dt * speed;
            float newPosition = currentPosition.x + deltaPosition;

            m_Data.ZigZagMoveable[index] = new ZigZagMoveable
            {
                Amplitude = zigzag.Amplitude,
                CurrentPosition = zigzag.CurrentPosition + deltaPosition,
                Speed = zigzag.Speed,
                Direction = zigzag.Direction
            };

            m_Data.Position[index] = new Position
            {
                Value = new float3(newPosition, currentPosition.y, currentPosition.z)
            };
        }


    }

}
