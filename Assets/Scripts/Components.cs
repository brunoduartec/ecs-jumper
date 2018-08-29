using Unity.Entities;
using UnityEngine;

using Unity.Mathematics;
using Unity.Collections;

using System.Collections.Generic;

public struct Player : IComponentData { }
public struct Block : IComponentData { }

public struct Velocity : IComponentData
{
    public float3 Value;
}

public struct Heading : IComponentData
{
    public float3 Value;
}

public struct RigidBodyComponent : IComponentData
{
    public float Mass;
}

public struct AABBComponent : IComponentData
{
    public float3 center;
    public float3 halfwidths;
}

public struct Collider : IComponentData { }

public struct CollisionComponent : IComponentData
{
    public float Value;
}
