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
        GameObject GameConstantsObject = GameObject.Find("GameConstants");
        GameConstants constants = GameConstantsObject.GetComponent<GameConstants>();

        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

        World.Active.GetOrCreateManager<UpdateHudSystem>().SetupGameObjects();

        Entity player = EntityFactory.Instance.createEntityByName("player");
        entityManager.SetComponentData(player, new Position { Value = new float3(0, constants.minY + constants.blockSize, 0.0f) });

        LevelGenerator generator = new LevelGenerator(
            constants.minX,
            constants.maxX,
            constants.minY,
            constants.maxY,
            constants.distanceHeightBetweenRows,
            constants.blocksTogether,
            constants.blockSize);

        List<float3> level = generator.buildBlocks();

        foreach (var blockPosition in level)
        {
            Entity block = EntityFactory.Instance.createEntityByName("block");
            entityManager.SetComponentData(block, new Position { Value = blockPosition });
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
