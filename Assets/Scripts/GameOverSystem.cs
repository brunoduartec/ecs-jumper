
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Mathematics;


public class GameOverSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Player> Player;
        public ComponentDataArray<Position> Position;
    }

    [Inject] private Data m_Data;


    protected override void OnUpdate()
    {
        for (int index = 0; index < m_Data.Length; ++index)
        {

        }
    }
}
