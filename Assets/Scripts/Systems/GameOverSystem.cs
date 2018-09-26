
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Unity.Mathematics;


public class GameOverSystem : ComponentSystem
{
    public struct Data
    {
        public readonly int Length;
        public ComponentDataArray<Player> Player;
        public ComponentDataArray<Velocity> Velocity;
        public EntityArray Entities;
    }

    [Inject] private Data m_Data;

    public struct GameStateData
    {
        public readonly int Length;
        public ComponentDataArray<GameState> GameState;
    };

    [Inject] private GameStateData m_GameStateData;

    protected override void OnUpdate()
    {
        if (m_GameStateData.Length <= 0 || m_Data.Length <= 0)
            return;

        if (m_Data.Velocity[0].Value.y < -100)
        {
            GameState currentGameState = m_GameStateData.GameState[0];
            currentGameState = new GameState
            {
                hasGameEnded = 1,
                hasStarted = currentGameState.hasStarted
            };

            m_GameStateData.GameState[0] = currentGameState;

            PostUpdateCommands.RemoveComponent<Velocity>(m_Data.Entities[0]);
        }
    }
}
