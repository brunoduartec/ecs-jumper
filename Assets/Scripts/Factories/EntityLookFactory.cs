using System.Collections;
using System.Collections.Generic;

using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;



public sealed class EntityLookFactory
{
    private static readonly EntityLookFactory instance = new EntityLookFactory();


    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static EntityLookFactory()
    {

    }

    private Dictionary<string, MeshInstanceRenderer> _looks = new Dictionary<string, MeshInstanceRenderer>();

    private EntityLookFactory()
    {

    }

    private void addLook(string entityName)
    {
        this._looks.Add(entityName, getLookFromEntityName(entityName));
    }

    public MeshInstanceRenderer getLook(string entityName)
    {
        return this._looks[entityName];
    }

    public void Init()
    {
        addLook("player");
        addLook("block");
        addLook("breakeable");
        addLook("zigzag");
    }

    public static EntityLookFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private MeshInstanceRenderer GetLookFromPrototype(string protoName)
    {
        var proto = GameObject.Find(protoName);
        var result = proto.GetComponent<MeshInstanceRendererComponent>().Value;
        Object.Destroy(proto);
        return result;
    }

    private MeshInstanceRenderer getLookFromEntityName(string entityName)
    {
        entityName = entityName + "RenderPrototype";
        return GetLookFromPrototype(entityName);
    }


}