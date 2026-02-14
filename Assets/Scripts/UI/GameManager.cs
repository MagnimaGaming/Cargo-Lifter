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
    [SerializeField] private GameData data;
    [SerializeField] private ShowAnalytics showAnalytics;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;



    private void Update()
    {
        if (sessionEnded) 
            return;


        if(totalCargo == hook.totalCargoReleased)
        {
            sessionEnded = true;
            UpdateSessionResults();
            showAnalytics.UpdateAnalyticsDisplay(data);
        }

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



    public void UpdateSessionResults()
    {

        data.time = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        data.cargo = hook.totalCargoReleased;
        data.finalScore = Mathf.RoundToInt(liveScore + (hook.totalCargoReleased / time) * 100) * 10;
        data.collisionCount = hook.ObstacleCollisionCount;
        data.highestCargoStack = hook.highestCargoStack;
        data.reps = (int)((hook.repCount) / 2) / 60;
        data.totalHoldTime = System.TimeSpan.FromSeconds(hook.holdTimer).ToString(@"mm\:ss");
        data.postureBreaks = (int) hook.postureBreaks / 120;
        float approxReactionTime = 1 - (hook.totalCargoReleased / (hook.repCount / 2)) * 1.5f;
        data.reactionTime = System.TimeSpan.FromSeconds(approxReactionTime).ToString(@"mm\:ss");
        data.calories = (int)((((hook.repCount) / 2) / 60) * 0.4f);
    }


    public GameData GetSessionResults()
    {
        return data;
    }




}
