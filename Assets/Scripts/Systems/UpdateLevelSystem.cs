
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Rendering;

using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

using Unity.Mathematics;

public class UpdateLevelSystem : ComponentSystem
{
    public struct PlayerData
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<Player> Player;
        [ReadOnly] public ComponentDataArray<Position> Position;
    }

    [Inject] private PlayerData m_PlayerData;

    public struct ItemData
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Item> Item;
        public ComponentDataArray<Position> Position;
    }
    [Inject] private ItemData m_ItemData;

    public struct LevelData
    {
        public readonly int Length;
        public ComponentDataArray<LevelState> LevelState;
    }

    [Inject] private LevelData m_LevelData;

    protected override void OnUpdate()
    {
        for (int index = 0; index < m_ItemData.Length; ++index)
        {
            if (m_PlayerData.Position[0].Value.y - m_ItemData.Position[index].Value.y > 50)
            {
                PostUpdateCommands.DestroyEntity(m_ItemData.Entities[index]);

                LevelState state = m_LevelData.LevelState[0];

                if (state.isDirty == 0 && state.currentRow > 0)
                {
                    m_LevelData.LevelState[0] = new LevelState
                    {
                        currentRow = state.currentRow - 1,
                        isDirty = 1,
                        higherRow = state.higherRow
                    };
                }
            }
        }
    }
}
