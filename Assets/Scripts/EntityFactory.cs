using System.Collections;
using System.Collections.Generic;

using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;



public sealed class EntityFactory
{
    private static readonly EntityFactory instance = new EntityFactory();


    private EntityManager _entityManager;
    private delegate Entity Delegate1();
    private Dictionary<string, Delegate1> entities = new Dictionary<string, Delegate1>();
    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static EntityFactory()
    {

    }


    private EntityFactory()
    {
        this.entities.Add("player", new Delegate1(CreatePlayer));
        this.entities.Add("block", new Delegate1(CreateBlock));
        this.entities.Add("score", new Delegate1(CreateScore));
    }

    private Entity CreatePlayer()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();

        var playerEntity = this._entityManager.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName("player"));
        this._entityManager.SetSharedComponentData(playerEntity, GetLookFromPrototype("PlayerRenderPrototype"));

        return playerEntity;
    }

    private Entity CreateBlock()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();

        var blockEntity = this._entityManager.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName("block"));
        this._entityManager.SetSharedComponentData(blockEntity, GetLookFromPrototype("BlockRenderPrototype"));

        return blockEntity;
    }

    private Entity CreateScore()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();

        var scoreEntity = this._entityManager.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName("score"));

        return scoreEntity;
    }

    public static EntityFactory Instance
    {
        get
        {
            return instance;
        }
    }
    public Entity createEntityByName(string entityName)
    {
        return (Entity)entities[entityName].DynamicInvoke(null);
    }

    private static MeshInstanceRenderer GetLookFromPrototype(string protoName)
    {
        var proto = GameObject.Find(protoName);
        var result = proto.GetComponent<MeshInstanceRendererComponent>().Value;
        Object.Destroy(proto);
        return result;
    }


}