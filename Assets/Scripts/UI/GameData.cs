[System.Serializable]
public class GameData 
{
    //game related data
    public int cargo;
    public string time;
    public float finalScore;
    public int collisionCount;
    public int highestCargoStack;

    //physical body related data
    public int reps;
    public string totalHoldTime;
    public int postureBreaks;
    public string reactionTime;
    public int calories;

    //normal data
    public int scorePerCargo = 100;
    public int moneyPerCargo = 25;

}
