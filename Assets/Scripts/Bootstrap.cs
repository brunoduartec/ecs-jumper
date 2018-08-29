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

        Entity player = EntityFactory.Instance.createEntityByName("player");
        entityManager.SetComponentData(player, new Position { Value = new float3(constants.playerInitX, constants.playerInitY, 0.0f) });
        entityManager.SetComponentData(player, new Heading { Value = new float3(1, 0, 0) });


        LevelGenerator generator = new LevelGenerator(
            constants.minX,
            constants.maxX,
            constants.minY,
            constants.maxY,
            constants.distanceHeightBetweenRows,
            constants.blocksTogether,
            constants.blockSize);

        List<float3> level = generator.buildLevel();

        foreach (var blockPosition in level)
        {
            Entity block = EntityFactory.Instance.createEntityByName("block");
            entityManager.SetComponentData(block, new Position { Value = blockPosition });
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
