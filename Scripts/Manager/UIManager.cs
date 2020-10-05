using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    private GameObject _gameOver, _winGame;

    [SerializeField]
    private Text _score;

    private void Start()
    {
        GameManager.Instance.GameManagerStateChanged += OnGameManagerStateChanged;
    }

    private void OnGameManagerStateChanged(GameManagerState before, GameManagerState after)
    {
        if (after == GameManagerState.GAMEOVER) SetGameOver();
        if (after == GameManagerState.WINGAME) SetWinGame();
    }

    public void SetGameOver()
    {
        _gameOver.SetActive(true);
    }

    public void SetWinGame()
    {
        _winGame.SetActive(true);
    }

    public void SetScore(int score)
    {

        _score.text = "x " + score.ToString();
    }
}
