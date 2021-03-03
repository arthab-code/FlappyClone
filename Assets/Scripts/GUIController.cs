using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public Button restartButton;
    public Button startButton;
    public Button menuButton;
    public Button quitButton;
    public GameObject menuPanel;
    public GameObject pauseMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.Instance.isPlaying += DoPlay;
        GameplayManager.Instance.isPause += DoPause;

        pauseMenuPanel.SetActive(false);
       
        restartButton.onClick.AddListener(delegate {
            GameplayManager.Instance.Restart();
            pauseMenuPanel.SetActive(false);
        });

        startButton.onClick.AddListener(delegate
        {
            GameplayManager.Instance.PauseManager();
            GameplayManager.Instance.Restart();
            menuPanel.SetActive(false);

        });

        menuButton.onClick.AddListener(delegate
        {
            menuPanel.SetActive(true);

        });

        quitButton.onClick.AddListener(delegate
        {
            Application.Quit();

        });
    }

    public void DoPause()
    {
        pauseMenuPanel.SetActive(true);
    }

    public void DoPlay()
    {
        pauseMenuPanel.SetActive(false);
    }

}
