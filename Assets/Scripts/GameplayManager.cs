using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    private int points = 0;
    private List<IRestartableObject> iRestartableObjects;

    public delegate void GameCallBack();
    public event GameCallBack isPlaying;
    public event GameCallBack isPause;

    private enum EGameState
    {
        Playing,
        Pause
    }

    private EGameState gameState;

    void Start()
    {
        gameState = EGameState.Playing;
        iRestartableObjects = new List<IRestartableObject>();
        GetAllRestartableObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Restart();

        if (Input.GetKeyDown(KeyCode.Space))
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

    private void PauseManager()
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

        Debug.Log(gameState);
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

    private void Restart()
    {
        points = 0;

        foreach(IRestartableObject restartableObject in iRestartableObjects)
        {
            restartableObject.DoRestart();
        }
    }
}
