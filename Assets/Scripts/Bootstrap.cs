using System.Collections;
using System.Collections.Generic;

using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

using Unity.Mathematics;

using Unity.Transforms;



public class Bootstrap : MonoBehaviour
{

    // Use this for initialization
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void Start()
    {
        EntityLookFactory.Instance.Init();

        GameObject GameConstantsObject = GameObject.Find("GameConstants");
        GameConstants constants = GameConstantsObject.GetComponent<GameConstants>();
        LevelGenerator.Instance.Init(
            constants.minX,
            constants.maxX,
            constants.minY,
            constants.maxY,
            constants.distanceHeightBetweenRows,
            3,
            constants.blocksTogether,
            constants.blockSize);

        List<LevelGenerator.Item> items = LevelGenerator.Instance.buildItems();
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

        World.Active.GetOrCreateManager<UpdateHudSystem>().SetupGameObjects();

        Entity player = EntityFactory.Instance.createEntityByName("player");
        entityManager.SetComponentData(player, new Position { Value = new float3(0, constants.minY + constants.blockSize * 2, 0.0f) });


        foreach (var item in items)
        {
            Entity block = EntityFactory.Instance.createEntityByName(item.itemProperty.entityName);
            entityManager.SetComponentData(block, new Position { Value = item.position });
        }


        for (int i = constants.minX; i < constants.maxX; i += constants.blockSize)
        {
            Entity block = EntityFactory.Instance.createEntityByName("block");
            float3 blockPosition = new float3(i, constants.minY, 0);
            entityManager.SetComponentData(block, new Position { Value = blockPosition });
        }

        Entity score = EntityFactory.Instance.createEntityByName("score");
        entityManager.SetComponentData(score, new Points { Value = 0 });
        entityManager.SetComponentData(score, new MaxHeight { Value = 0 });


    }

    // Update is called once per frame
    void Update()
    {

    }
}
