using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;


[AlwaysUpdateSystem]
public class UpdateHudSystem : ComponentSystem
{
    public struct PlayerData
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<Player> Player;
    }

    [Inject] PlayerData m_Players;


    public struct ScoreData
    {
        public readonly int Length;
        public ComponentDataArray<MaxHeight> MaxHeight;
    }

    [Inject] ScoreData m_Score;

    public Text ScoreText;

    public void SetupGameObjects()
    {
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    protected override void OnUpdate()
    {
        if (m_Players.Length > 0)
        {
            UpdateAlive();
        }
        else
        {
            UpdateDead();
        }
    }

    private void UpdateDead()
    {
    }

    private void UpdateAlive()
    {
        ScoreText.text = Mathf.Round(m_Score.MaxHeight[0].Value).ToString();
    }
}

