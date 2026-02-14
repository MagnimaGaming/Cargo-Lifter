using UnityEngine;

[System.Serializable]
public class SessionGameData : MonoBehaviour
{
    //game related data
    public int cargo;
    public string time;
    public float finalScore;
    public int collisionCount;
    public int highestCargoStack;

    //physical body related data
    public int reps;
    public float totalHoldTime;
    public int postureBreaks;
    public float reactionTime;
    public float calories;

    //normal data
    public int scorePerCargo = 100;
    public int moneyPerCargo = 25;

}
