using System.Collections;
using System.Collections.Generic;

using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

using UnityEngine;



public sealed class ArchetypeFactory
{
    private static readonly ArchetypeFactory instance = new ArchetypeFactory();


    private EntityManager _entityManager;
    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static ArchetypeFactory()
    {
    }

    private ArchetypeFactory()
    {
        this._entityManager = World.Active.GetExistingManager<EntityManager>();
        var playerArchetype = this._entityManager.CreateArchetype(
            typeof(Position),
            typeof(Rotation),
            typeof(PlayerInput),
            typeof(Jump),
            typeof(Velocity),
            typeof(Player),
            typeof(RigidBodyComponent),
            typeof(AABBComponent),
            typeof(Collider),
            typeof(CollisionComponent),
            typeof(MeshInstanceRenderer)
        );
        this.archetypes.Add("player", playerArchetype);


        var blockArchetype = this._entityManager.CreateArchetype(
            typeof(Position),
            typeof(Rotation),
            typeof(Item),
            typeof(AABBComponent),
            typeof(Collider),
            typeof(CollisionComponent),
            typeof(MeshInstanceRenderer)
        );
        this.archetypes.Add("block", blockArchetype);

        var brackeableArchetype = this._entityManager.CreateArchetype(
            typeof(Position),
            typeof(Item),
            typeof(BreakComponent),
            typeof(AABBComponent),
            typeof(Collider),
            typeof(CollisionComponent),
            typeof(MeshInstanceRenderer)
        );
        this.archetypes.Add("breakeable", brackeableArchetype);

        var zigzagArchetype = this._entityManager.CreateArchetype(
            typeof(Position),
            typeof(Item),
            typeof(ZigZagMoveable),
            typeof(AABBComponent),
            typeof(Collider),
            typeof(CollisionComponent),
            typeof(MeshInstanceRenderer)
        );
        this.archetypes.Add("zigzag", zigzagArchetype);

        var scoreArchetype = this._entityManager.CreateArchetype(
            typeof(Points),
            typeof(MaxHeight)
        );
        this.archetypes.Add("score", scoreArchetype);


        var levelArchetype = this._entityManager.CreateArchetype(
            typeof(LevelState),
            typeof(LevelInfo)
        );
        this.archetypes.Add("level", levelArchetype);

        var gameArchetype = this._entityManager.CreateArchetype(
            typeof(GameState)
        );
        this.archetypes.Add("game", gameArchetype);
    }

    public static ArchetypeFactory Instance
    {
        get
        {
            return instance;
        }
    }
    private Dictionary<string, EntityArchetype> archetypes = new Dictionary<string, EntityArchetype>();
    public EntityArchetype getArchetypeByName(string archetypeName)
    {
        return archetypes[archetypeName];
    }



}