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
        int size = 40;
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

        Entity player = EntityFactory.Instance.createEntityByName("player");
        entityManager.SetComponentData(player, new Position { Value = new float3(10, size, 0.0f) });


        LevelGenerator generator = new LevelGenerator(0, 10, 0, 10, 200);

        generator.buildLevel();



    }

    // Update is called once per frame
    void Update()
    {

    }
}
