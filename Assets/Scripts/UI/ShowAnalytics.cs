using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowAnalytics : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI compltetionTime; 
    [SerializeField] TextMeshProUGUI collisionCount; 
    [SerializeField] TextMeshProUGUI highestCargoStack; 
    [SerializeField] TextMeshProUGUI earnings; 
    [SerializeField] TextMeshProUGUI score;

    [SerializeField] TextMeshProUGUI sit2StandReps;
    [SerializeField] TextMeshProUGUI totalHoldTime;
    [SerializeField] TextMeshProUGUI postureBreaks;
    [SerializeField] TextMeshProUGUI reactionTime;
    [SerializeField] TextMeshProUGUI estimatedCalBurned;

   

    public void UpdateAnalyticsDisplay(GameData gameData)
    {
        if (gameData == null) return;
        //game related 
        compltetionTime.text = gameData.time;
        collisionCount.text = gameData.collisionCount.ToString();
        highestCargoStack.text = gameData.highestCargoStack.ToString();
        earnings.text = (gameData.cargo * gameData.moneyPerCargo).ToString();
        score.text = gameData.finalScore.ToString();

        //exercise related
        sit2StandReps.text = gameData.reps.ToString();
        totalHoldTime.text = gameData.totalHoldTime.ToString();
        postureBreaks.text = gameData.postureBreaks.ToString();
        reactionTime.text = gameData.reactionTime.ToString();
        estimatedCalBurned.text = gameData.calories.ToString();
    }


}
