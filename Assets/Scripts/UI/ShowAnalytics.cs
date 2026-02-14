using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowAnalytics : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI copltetionTime; 
    [SerializeField] TextMeshProUGUI collisionCount; 
    [SerializeField] TextMeshProUGUI highestCargoStack; 
    [SerializeField] TextMeshProUGUI earnings; 
    [SerializeField] TextMeshProUGUI score;

    [SerializeField] TextMeshProUGUI sit2StandReps;
    [SerializeField] TextMeshProUGUI totalHoldTime;
    [SerializeField] TextMeshProUGUI postureBreaks;
    [SerializeField] TextMeshProUGUI reactionTime;
    [SerializeField] TextMeshProUGUI estimatedCalBurned;



    [SerializeField] GameManager gameManager;
    [SerializeField] SessionGameData gameData;


    private void Awake()
    {
        gameData = gameManager.GetSessionResults();

    }


}
