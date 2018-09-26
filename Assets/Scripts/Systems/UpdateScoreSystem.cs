
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Mathematics;


[UpdateAfter(typeof(ComputeColliderAABBSystem))]
public class UpdateScoreSystem : ComponentSystem
{
    public struct PlayerData
    {
        public readonly int Length;
        public ComponentDataArray<Player> Player;
        public ComponentDataArray<Position> Position;
    }

    [Inject] private PlayerData m_PlayerData;


    public struct ScoreData
    {
        public readonly int Length;
        public ComponentDataArray<MaxHeight> MaxHeight;
    }

    [Inject] ScoreData m_Score;

    public UpdateScoreSystem()
    {
    }



    protected override void OnUpdate()
    {
        for (int index = 0; index < m_PlayerData.Length; ++index)
        {
            if (m_PlayerData.Position[index].Value.y > m_Score.MaxHeight[0].Value)
            {
                m_Score.MaxHeight[0] = new MaxHeight
                {
                    Value = m_PlayerData.Position[index].Value.y
                };
            }
        }
    }
}
