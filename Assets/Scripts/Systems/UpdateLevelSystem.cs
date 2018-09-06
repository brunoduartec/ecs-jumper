
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Rendering;

using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

using Unity.Mathematics;


[UpdateAfter(typeof(ComputeColliderAABBSystem))]
public class UpdateLevelSystem : ComponentSystem
{
    public struct PlayerData
    {
        public readonly int Length;
        public ComponentDataArray<Player> Player;
        public ComponentDataArray<Position> Position;
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

    protected override void OnUpdate()
    {
        for (int index = 0; index < m_ItemData.Length; ++index)
        {
            if (m_PlayerData.Position[0].Value.y - m_ItemData.Position[index].Value.y > 50)
            {
                PostUpdateCommands.DestroyEntity(m_ItemData.Entities[index]);

                List<LevelGenerator.Item> items = LevelGenerator.Instance.buildValidRow();
                foreach (var item in items)
                {
                    PostUpdateCommands.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName(item.itemProperty.entityName));
                    PostUpdateCommands.SetComponent(new Position { Value = item.position });
                    PostUpdateCommands.SetComponent(default(Item));
                    PostUpdateCommands.AddSharedComponent(EntityLookFactory.Instance.getLook(item.itemProperty.entityName));
                }
            }
        }
    }
}
