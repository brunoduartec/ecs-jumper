using Unity.Entities;
using UnityEngine;

using Unity.Transforms;

using System.Collections;


public class PlayerInputSystem : ComponentSystem
{
    struct PlayerData
    {
        public readonly int Length;

        public ComponentDataArray<PlayerInput> PlayerInput;
        public ComponentDataArray<Rotation> Rotation;
    }

    [Inject] private PlayerData m_Players;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < m_Players.Length; ++i)
        {
            UpdatePlayerInput(i, dt);
        }
    }

    private void UpdatePlayerInput(int i, float dt)
    {
        // Touch touch = Input.touches[0];

        float y = Input.acceleration.y;

        float xDirection = y > 0 ? 1 : 0;

        Vector3 direction = new Vector3(xDirection, 0, 0);

        m_Players.Rotation[i] = new Rotation
        {
            Value = Quaternion.LookRotation(direction, Vector3.up)
        };
    }
}

