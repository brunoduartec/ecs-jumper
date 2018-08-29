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
            typeof(Transform),
            typeof(Position),
            typeof(Heading),
            typeof(Velocity),
            typeof(Player),
            typeof(RigidBodyComponent),
            typeof(AABBComponent),
            typeof(CollisionComponent),
            typeof(MeshInstanceRenderer)
        );

        this.archetypes.Add("player", playerArchetype);

        var blockArchetype = this._entityManager.CreateArchetype(
            typeof(Transform),
            typeof(Position),
            typeof(Block),
            typeof(AABBComponent),
            typeof(CollisionComponent),
            typeof(MeshInstanceRenderer)
        );

        this.archetypes.Add("block", blockArchetype);


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