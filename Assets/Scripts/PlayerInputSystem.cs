using Unity.Entities;
using UnityEngine;

public class PlayerInputSystem : ComponentSystem
{
    struct PlayerData
    {
        public readonly int Length;
        public ComponentDataArray<PlayerInput> PlayerInput;
    }

    [Inject] private PlayerData m_Data;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < m_Data.Length; ++i)
        {
            UpdatePlayerInput(i, dt);
        }
    }

    private void UpdatePlayerInput(int i, float dt)
    {
        float dirX = Input.acceleration.x;

        float xDirection = dirX > 0 ? 1 : -1;

        m_Data.PlayerInput[i] = new PlayerInput
        {
            Direction = xDirection
        };
    }
}