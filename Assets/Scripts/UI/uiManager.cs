 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject diffSelectMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject analyticsStatsTab;
    [SerializeField] private GameObject analyticsGraphTab;
    [SerializeField] private GameObject leaderBoard;

    [SerializeField] public CraneRotate crane;
    [SerializeField] public Hook hook;
    [SerializeField] private GameManager gameManager;
    bool leaderboardShown = false;

    private void Start()
    {
       GoToStart();
       startMenu.SetActive(true);
       diffSelectMenu.SetActive(false);
       settingsMenu.SetActive(false);
       pauseMenu.SetActive(false);

       analyticsStatsTab.SetActive(false);
       analyticsGraphTab.SetActive(false);
       leaderBoard.SetActive(false);

    }


    private void Update()
    {
        if (gameManager.sessionEnded && !leaderboardShown)
        {
            Invoke("ShowLeaderBoard", 2F);
            leaderboardShown = true;
        }
        if (leaderboardShown)
        {
            crane.StopRotation();
        }
    }




    public void StartGame()
    {
        startMenu.SetActive(false);
        diffSelectMenu.SetActive(true);
        analyticsStatsTab.SetActive(false);
        analyticsGraphTab.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadBeginnerLevel()
    {
        // Load Beginner Level Scene
        diffSelectMenu.SetActive(false);
        pauseMenu.SetActive(false);
        crane.StartRotation();
        hook.isGameStarted = true;
        crane.rotationSpeed = 15f;
    }

    public void LoadIntermediateLevel()
    {
        //Load Inter Level Scene
        diffSelectMenu.SetActive(false);
        pauseMenu.SetActive(false);
        crane.StartRotation();
        hook.isGameStarted = true;
        crane.rotationSpeed = 25f;

    }

    public void LoadExpertLevel()
    {

        //Load expert level scene
        diffSelectMenu.SetActive(false);
        pauseMenu.SetActive(false);
        crane.StartRotation();
        hook.isGameStarted = true;
        crane.rotationSpeed = 40f;

    }

    public void GoToStart()
    {
        analyticsGraphTab.SetActive(false);
        startMenu.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }


    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        //reload the current scene

        //for now
        pauseMenu.SetActive(false);
    }
    public void ShowLeaderBoard()
    {
        leaderBoard.SetActive(true);
    }

    public void ShowAnalyticsStatsTab()
    {
        leaderBoard.SetActive(false);
        analyticsStatsTab.SetActive(true);

    }

    public void ShowAnalyticsGraphTab()
    {
        analyticsStatsTab.SetActive(false);
        analyticsGraphTab.SetActive(true);
    }




}


