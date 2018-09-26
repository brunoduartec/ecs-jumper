using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[AlwaysUpdateSystem]
public class UpdateHudSystem : ComponentSystem
{
    public struct GameStateData
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<GameState> GameState;
    }

    [Inject] GameStateData m_GameStateData;


    public struct ScoreData
    {
        public readonly int Length;
        public ComponentDataArray<MaxHeight> MaxHeight;
    }

    [Inject] ScoreData m_Score;

    public Text scoreText;
    public RectTransform popupTransform;

    public void SetupGameObjects()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        popupTransform = GameObject.Find("Panel_PopUpWindow").GetComponent<RectTransform>();

    }

    protected override void OnUpdate()
    {
        if (m_GameStateData.GameState[0].hasGameEnded <= 0)
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
        // Move a transform to position 1,2,3 in 1 second
        popupTransform.transform.DOMove(new Vector3(Screen.width / 2, Screen.height / 2, 0), 1);
        // popupTransform.transform.position = Vector3.zero;

    }

    private void UpdateAlive()
    {
        popupTransform.transform.position = new Vector3(Screen.width / 2, -Screen.height, 0);
        scoreText.text = Mathf.Round(m_Score.MaxHeight[0].Value).ToString();
    }
}

