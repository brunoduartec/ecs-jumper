
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Mathematics;

public class FollowPlayerSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Position> Position;
        public ComponentDataArray<Player> Player;
    }

    [Inject] private Data m_Data;

    public FollowPlayerSystem()
    {
    }

    protected override void OnUpdate()
    {
        for (int index = 0; index < m_Data.Length; ++index)
        {
            Camera cam = Camera.main;

            if (cam)
            {
                cam.transform.position = new Vector3(m_Data.Position[index].Value.x, m_Data.Position[index].Value.y + 40, cam.transform.position.z);
            }

        }
    }
}
