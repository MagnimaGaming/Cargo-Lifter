using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float time;
    public bool sessionEnded;
    int liveScore;
    public int totalCargo = 3;

    [SerializeField] Hook hook;
    [SerializeField] private SessionGameData data;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;


    private void Update()
    {
        if (sessionEnded) 
            return;


        if(totalCargo == hook.totalCargoReleased)
            sessionEnded = true;

        UpdateLiveScore();
        UpdateTimer();

    }




    void UpdateLiveScore()
    {
        int cargo = hook.totalCargoReleased;
        liveScore = cargo * data.scorePerCargo;


        scoreTxt.text = liveScore.ToString();
    } 

    void UpdateTimer()
    {
        time += Time.deltaTime;
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.Floor(time % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public SessionGameData GetSessionResults()
    {

        SessionGameData data = new SessionGameData();
        data.time = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        data.cargo = hook.totalCargoReleased;
        data.finalScore = Mathf.RoundToInt(liveScore + (hook.totalCargoReleased / time) * 100) * 10;

        return data;
    }



}
