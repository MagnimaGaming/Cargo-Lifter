using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    // Start Menu components
    //[SerializeField] private Button playBtn;
    //[SerializeField] private Button quitBtn;
    [SerializeField] private GameObject startMenu;


    // Diff Menu components
    //[SerializeField] private Button beginnerBtn;
    //[SerializeField] private Button intermediateBtn;
    //[SerializeField] private Button expertBtn;
    //[SerializeField] private Button diffBackBtn;
    [SerializeField] private GameObject diffSelectMenu;


    // In Game Menu components
    //[SerializeField] private Button settingBtn;


    // Settings Menu components



    [SerializeField] private GameObject settingsMenu;




    



    private void Start()
    {
        //playBtn.onClick.AddListener(StartGame);
        //quitBtn.onClick.AddListener(QuitGame);
        //beginnerBtn.onClick.AddListener(LoadBeginnerLevel);
        //intermediateBtn.onClick.AddListener(LoadIntermediateLevel);
        //expertBtn.onClick.AddListener(LoadExpertLevel);
        //diffBackBtn.onClick.AddListener(GoToStart);
        //settingBtn.onClick.AddListener(OpenSettingsMenu);
    }




    public void StartGame()
    {
        startMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadBeginnerLevel()
    {
        // Load Beginner Level Scene
        diffSelectMenu.SetActive(false);
    }

    public void LoadIntermediateLevel()
    {
        //Load Inter Level Scene
        diffSelectMenu.SetActive(false);

    }

    public void LoadExpertLevel()
    {

        //Load expert level scene
        diffSelectMenu.SetActive(false);


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
}


