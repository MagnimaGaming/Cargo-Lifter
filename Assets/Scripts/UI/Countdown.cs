using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float countdownTimer = 60f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;

        }
        else
        {
            countdownTimer = 0;
        }


        float min = Mathf.Floor(countdownTimer / 60);
        float sec = Mathf.Floor(countdownTimer % 60);
        timerText.text=string.Format("{0:00} : {1:00} ", min,sec);
    }
}
