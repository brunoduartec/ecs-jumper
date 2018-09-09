
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Rendering;

using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

using Unity.Mathematics;

public class SpawnItemsSystem : ComponentSystem
{
    public struct LevelData
    {
        public readonly int Length;
        public ComponentDataArray<LevelState> LevelState;
    }

    [Inject] private LevelData m_LevelData;


    private void createEntity(LevelGenerator.Item item)
    {
        PostUpdateCommands.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName(item.itemProperty.entityName));
        PostUpdateCommands.SetComponent(new Position { Value = item.position });
        PostUpdateCommands.SetComponent(default(Item));

        PostUpdateCommands.SetSharedComponent(EntityLookFactory.Instance.getLook("block"));
    }


    protected override void OnUpdate()
    {
        LevelState state = m_LevelData.LevelState[0];

        if (state.currentRow < state.higherRow)
        {
            List<LevelGenerator.Item> items = LevelGenerator.Instance.buildValidRow();
            foreach (var item in items)
            {
                createEntity(item);
            }

            m_LevelData.LevelState[0] = new LevelState
            {
                currentRow = state.currentRow + 1,
                isDirty = 0,
                higherRow = state.higherRow
            };
        }
    }
}
