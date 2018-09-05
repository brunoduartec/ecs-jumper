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
    private GameConstants _constants;
    private delegate Entity Delegate1();
    private Dictionary<string, Delegate1> entities = new Dictionary<string, Delegate1>();
    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static EntityFactory()
    {

    }


    private EntityFactory()
    {
        GameObject GameConstantsObject = GameObject.Find("GameConstants");
        this._constants = GameConstantsObject.GetComponent<GameConstants>();


        this.entities.Add("player", new Delegate1(CreatePlayer));
        this.entities.Add("block", new Delegate1(CreateBlock));
        this.entities.Add("breakeable", new Delegate1(CreateBreakeable));
        this.entities.Add("score", new Delegate1(CreateScore));
    }

    private Entity CreatePlayer()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();

        var entity = this._entityManager.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName("player"));

        MeshInstanceRenderer renderer = GetLookFromPrototype("PlayerRenderPrototype");
        this._entityManager.SetComponentData(entity, getColliderInfo(renderer));
        this._entityManager.SetSharedComponentData(entity, renderer);

        this._entityManager.SetComponentData(entity, new Jump
        {
            Value = this._constants.jumpVelocityY
        });

        this._entityManager.SetComponentData(entity, new PlayerInput
        {
            Intensity = this._constants.jumpVelocityX
        });


        return entity;
    }


    private Entity CreateBreakeable()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();

        var entity = this._entityManager.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName("breakeable"));

        this._entityManager.SetComponentData(entity, new BreakComponent
        {
            coolDown = this._constants.breakTimeInSeconds,
            started = 0
        });

        MeshInstanceRenderer renderer = GetLookFromPrototype("BreakeableRenderPrototype");
        this._entityManager.SetComponentData(entity, getColliderInfo(renderer));
        this._entityManager.SetSharedComponentData(entity, renderer);

        return entity;
    }
    private Entity CreateBlock()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();

        var entity = this._entityManager.CreateEntity(ArchetypeFactory.Instance.getArchetypeByName("block"));

        MeshInstanceRenderer renderer = GetLookFromPrototype("SolidRenderPrototype");
        this._entityManager.SetComponentData(entity, getColliderInfo(renderer));
        this._entityManager.SetSharedComponentData(entity, renderer);

        return entity;
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

    private static Collider getColliderInfo(MeshInstanceRenderer renderer)
    {
        Collider collider = new Collider
        {
            size = renderer.mesh.bounds.size.x / 2
        };
        return collider;
    }
    private static MeshInstanceRenderer GetLookFromPrototype(string protoName)
    {
        var proto = GameObject.Find(protoName);
        var result = proto.GetComponent<MeshInstanceRendererComponent>().Value;
        Object.Destroy(proto);
        return result;
    }


}