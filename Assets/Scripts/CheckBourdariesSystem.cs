
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Mathematics;

[UpdateAfter(typeof(RidigBodySystem))]
public class CheckBourdaries : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<Velocity> Velocity;
        public ComponentDataArray<RigidBodyComponent> RigidBody;

    }

    [Inject] private Data m_Data;

    public CheckBourdaries()
    {

    }
    protected override void OnUpdate()
    {
        GameObject GameConstantsObject = GameObject.Find("GameConstants");
        GameConstants _constants = GameConstantsObject.GetComponent<GameConstants>();
        for (int index = 0; index < m_Data.Length; ++index)
        {
            if (m_Data.Position[index].Value.x > (float)(_constants.maxX + _constants.blockSize))
            {
                float3 currentPosition = m_Data.Position[index].Value;
                Position newPosition = new Position
                {
                    Value = new float3(
                        (float)_constants.minX,
                        currentPosition.y,
                        currentPosition.z)
                };
                m_Data.Position[index] = newPosition;
            }
            else if (m_Data.Position[index].Value.x < (float)(_constants.minX - _constants.blockSize))
            {
                float3 currentPosition = m_Data.Position[index].Value;
                Position newPosition = new Position
                {
                    Value = new float3(
                        (float)_constants.maxX,
                        currentPosition.y,
                        currentPosition.z)
                };
                m_Data.Position[index] = newPosition;
            }
            if (m_Data.Position[index].Value.y < _constants.minY)
            {
                float3 currentPosition = m_Data.Position[index].Value;

                Position newPosition = new Position
                {
                    Value = new float3(
                        (float)_constants.playerInitX,
                        (float)_constants.playerInitY,
                        currentPosition.z)
                };
                m_Data.Position[index] = newPosition;
                m_Data.Velocity[index] = new Velocity
                {
                    Value = float3.zero
                };
            }

        }
    }
}
