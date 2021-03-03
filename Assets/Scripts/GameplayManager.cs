using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    private int points = 0;
    private List<IRestartableObject> iRestartableObjects;
    private bool endGame;

    public delegate void GameCallBack();
    public event GameCallBack isPlaying;
    public event GameCallBack isPause;

    public enum EGameState
    {
        Playing,
        Pause
    }

    public EGameState gameState;

    void Start()
    {
        gameState = EGameState.Pause;
        isPause();
        iRestartableObjects = new List<IRestartableObject>();
        GetAllRestartableObjects();
        endGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && endGame == false)
            PauseManager();
    }

    public int AddScore(int value)
    {
        return points += value;
    }

    public int ShowScore()
    {
        return points;
    }

    public void PauseManager()
    {
        switch (gameState)
        {
            case EGameState.Playing:       
                isPause();
                gameState = EGameState.Pause;
                break;

            case EGameState.Pause:
                isPlaying();
                gameState = EGameState.Playing;
                break;
        }
    }

    private void GetAllRestartableObjects()
    {
        iRestartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach (IRestartableObject childrenInterace in childrenInterfaces)
            {
                iRestartableObjects.Add(childrenInterace);
            }
        }
    }

    public bool SetEndGaming(bool value)
    {
        return endGame = value;
    }

    public void Restart()
    {
        points = 0;
        SetEndGaming(false);
        foreach(IRestartableObject restartableObject in iRestartableObjects)
        {
            restartableObject.DoRestart();
        }
    }
}
