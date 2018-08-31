
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Mathematics;

[UpdateAfter(typeof(RidigBodySystem))]
public class CreateBlocksSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<Player> Player;
    }

    [Inject] private Data m_Data;

    public CreateBlocksSystem()
    {

    }
    protected override void OnUpdate()
    {
        GameObject GameConstantsObject = GameObject.Find("GameConstants");
        GameConstants _constants = GameConstantsObject.GetComponent<GameConstants>();


        for (int index = 0; index < m_Data.Length; ++index)
        {


        }
    }
}
