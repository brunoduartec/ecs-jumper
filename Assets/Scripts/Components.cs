using Unity.Entities;
using UnityEngine;

using Unity.Mathematics;
using Unity.Collections;

using System.Collections.Generic;

using Unity.Transforms;

public struct Jump : IComponentData
{
    public float Value;
}
public struct Player : IComponentData { }
public struct Block : IComponentData { }
public struct BreakComponent : IComponentData
{
    public float coolDown;
    public float started;
}

public struct ZigZagMoveable : IComponentData
{
    public float Amplitude;
    public float CurrentPosition;
    public float Speed;

    public float Direction;
}

public struct PlayerInput : IComponentData
{
    public float FireCooldown;
    public float Direction;
    public float Intensity;
}
public struct Velocity : IComponentData
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

public struct Collider : IComponentData
{
    public float size;
}

public struct CollisionComponent : IComponentData
{
    public float3 direction;
}


public struct MaxHeight : IComponentData
{
    public float Value;
}

public struct Points : IComponentData
{
    public float Value;
}
