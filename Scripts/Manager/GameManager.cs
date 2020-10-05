using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GameManagerStateDelegate(GameManagerState before, GameManagerState after);
public enum GameManagerState {PAUSE, TUTORIAL, SELECTING, RUNNING, GAMEOVER, PREGAME, WINGAME }
public class GameManager : Singleton<GameManager>
{
    private GameManagerState _currentManagerState = GameManagerState.PREGAME;
    private int _activeActionsCount = 0;
    public event GameManagerStateDelegate GameManagerStateChanged;

    public bool RestartedLevel = false;

    [SerializeField]
    private string _currentLevelName;
    private int _score = 0;

    private void Start()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        GameManager.Instance.SetCurrentLevelName("Menu");
    }

    public void LoadLevel(string levelName)
    {
        
        SceneManager.UnloadSceneAsync(GameManager.Instance.GetCurrentLevelName());
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        GameManager.Instance.SetCurrentLevelName(levelName);
        _score = 0;
        _activeActionsCount = 0;
        GameManager.Instance.TryChangeManagerState(GameManagerState.RUNNING, this.gameObject.name + ":LoadLevel");
    }

  
    public void SetCurrentLevelName(String level)
    {
        _currentLevelName = level;
    }

    public string GetCurrentLevelName()
    {
        return _currentLevelName;
    }

    public void RestartLevel()
    {
        LoadLevel(_currentLevelName);
        RestartedLevel = true;

    }
 
    public void AddScore()
    {
        _score += 1;
        UIManager.Instance.SetScore(_score);

    }


    private void ChangeGameManagerState(GameManagerState state)
    {
        GameManagerState before = _currentManagerState;
        switch (state)
        {
            case GameManagerState.PREGAME:
                Time.timeScale = 1;
                _currentManagerState = state;
                break;
            case GameManagerState.PAUSE:
                Time.timeScale = 0;
                _currentManagerState = state;
                break;
            case GameManagerState.RUNNING:
                Time.timeScale = 1;
                _currentManagerState = state;
                break;
            case GameManagerState.SELECTING:
                Time.timeScale = 0;
                _currentManagerState = state;
                break;
            case GameManagerState.TUTORIAL:
                Time.timeScale = 0;
                _currentManagerState = state;
                break;
            case GameManagerState.GAMEOVER:
                Time.timeScale = 0;
                _currentManagerState = state;
                break;
            case GameManagerState.WINGAME:
                Time.timeScale = 0;
                _currentManagerState = state;
                break;
        }

        if(GameManagerStateChanged != null)
        {
            GameManagerStateChanged.Invoke(before, _currentManagerState);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void TryChangeManagerState(GameManagerState state, string source)
    {

        if (_currentManagerState == state) return; 
        if (_currentManagerState == GameManagerState.TUTORIAL && state == GameManagerState.SELECTING) return;

        ChangeGameManagerState(state);
    }

    public GameManagerState GetGameManagerState()
    {
        return _currentManagerState;
    }

    internal void OnActiveActionChanged(bool active)
    {
        _activeActionsCount += active == true ? 1 : -1;
 
        CheckGameOver();
    }

    public void WinGame()
    {
        TryChangeManagerState(GameManagerState.WINGAME,"GameManager:WinGame");
    }

    private void CheckGameOver()
    {
        if(_activeActionsCount == 0)
        {
            TryChangeManagerState(GameManagerState.GAMEOVER, "GameManager:GameOver");
        }
    }


}
