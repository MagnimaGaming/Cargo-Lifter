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
    [SerializeField] private GameObject analyticsTab;
    [SerializeField] private GameObject leaderBoard;





    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
            ShowLeaderBoard();
    }




    public void StartGame()
    {
        startMenu.SetActive(false);
        diffSelectMenu.SetActive(true);
        analyticsTab.SetActive(false);
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
    }

    public void LoadIntermediateLevel()
    {
        //Load Inter Level Scene
        diffSelectMenu.SetActive(false);
        pauseMenu.SetActive(false);


    }

    public void LoadExpertLevel()
    {

        //Load expert level scene
        diffSelectMenu.SetActive(false);
        pauseMenu.SetActive(false);



    }

    public void GoToStart()
    {
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

    public void ShowAnalyticsTab()
    {
        analyticsTab.SetActive(true);
        leaderBoard.SetActive(false);

    }


    public void ShowLeaderBoard()
    {
        leaderBoard.SetActive(true);
    }


}


