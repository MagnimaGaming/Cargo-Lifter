using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private Hook hook;




    private void Update()
    {
        UpdateScore();
    }




    void UpdateScore()
    {
        int score = hook.totalCargoReleased;
        scoreTxt.text = "Score: " + score;
    } 

}
