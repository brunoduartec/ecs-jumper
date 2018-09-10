
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

    private void SetItemBaseComponent(LevelGenerator.Item item)
    {
        PostUpdateCommands.SetComponent(new Position { Value = item.position });
        PostUpdateCommands.SetComponent(default(Item));
    }

    private void createBlock()
    {
        string entityName = "block";

        PostUpdateCommands.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName(entityName));

        MeshInstanceRenderer renderer = EntityLookFactory.Instance.getLook(entityName);
        PostUpdateCommands.SetComponent(EntityFactory.getColliderInfo(renderer));
        PostUpdateCommands.SetSharedComponent(renderer);
    }
    private void createBreakeable()
    {
        GameObject GameConstantsObject = GameObject.Find("GameConstants");
        GameConstants _constants = GameConstantsObject.GetComponent<GameConstants>();

        string entityName = "breakeable";
        PostUpdateCommands.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName(entityName));

        PostUpdateCommands.SetComponent(new BreakComponent
        {
            coolDown = _constants.breakTimeInSeconds,
            started = 0
        });

        MeshInstanceRenderer renderer = EntityLookFactory.Instance.getLook(entityName);
        PostUpdateCommands.SetComponent(EntityFactory.getColliderInfo(renderer));
        PostUpdateCommands.SetSharedComponent(renderer);
    }

    private void createZigZag()
    {
        GameObject GameConstantsObject = GameObject.Find("GameConstants");
        GameConstants _constants = GameConstantsObject.GetComponent<GameConstants>();

        string entityName = "zigzag";

        PostUpdateCommands.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName(entityName));

        PostUpdateCommands.SetComponent(new ZigZagMoveable
        {
            Amplitude = _constants.MoveableBlockAmplitude * _constants.blockSize,
            Speed = _constants.MoveableBlockSpeed,
            CurrentPosition = 0,
            Direction = 1
        });

        MeshInstanceRenderer renderer = EntityLookFactory.Instance.getLook(entityName);
        PostUpdateCommands.SetComponent(EntityFactory.getColliderInfo(renderer));
        PostUpdateCommands.SetSharedComponent(renderer);
    }


    private void createEntity(LevelGenerator.Item item)
    {
        switch (item.itemProperty.entityName)
        {
            case "block":
                createBlock();
                break;
            case "breakeable":
                createBreakeable();
                break;
            case "zigzag":
                createZigZag();
                break;
            default:
                createBlock();
                break;
        }

        SetItemBaseComponent(item);
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
