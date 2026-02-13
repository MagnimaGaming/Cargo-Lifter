using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public Transform entriesContainer;
    public Transform entryTemplate;
    private List<LeaderboardEntryData> leaderboardEntryDataList = new List<LeaderboardEntryData>();
    private List<Transform> leaderboardEntryTransformList = new List<Transform>();
    public  GameManager gameManager;
    private SessionGameData gameData;


    private void Awake()
    {

        gameData = gameManager.GetSessionResults();

        entryTemplate.gameObject.SetActive(false);

        AddNewEntry("Bobby", gameData.time, gameData.moneyPerCargo * gameData.cargo, gameData.finalScore);

        string jsonString = PlayerPrefs.GetString("leaderboardEntries");

        LeaderboardEntries leaderboard = JsonUtility.FromJson<LeaderboardEntries>(jsonString);

        leaderboardEntryDataList = leaderboard.leaderboardEntryDataList;

        //sorting based on score
        leaderboardEntryDataList.Sort((a, b) => b.score.CompareTo(a.score));

        foreach (LeaderboardEntryData entry in leaderboardEntryDataList)
        {
            CreateLeaderboardEntryTransform(entry, entriesContainer, leaderboardEntryTransformList);
        }

    }

    private void AddNewEntry(string name, float time, int earnings, float score)
    {
        //create entry
        LeaderboardEntryData entry = new LeaderboardEntryData { playerName = name, time = time, earnings = earnings, score = score };

        //load current entries data
        string jsonString = PlayerPrefs.GetString("leaderboardEntries", "");

        LeaderboardEntries entries;
        if (jsonString == null)
        {
            entries = new LeaderboardEntries();
            entries.leaderboardEntryDataList = new List<LeaderboardEntryData>();
        }
        else
        {
            entries = JsonUtility.FromJson<LeaderboardEntries>(jsonString);
        }

        //update and save
        entries.leaderboardEntryDataList.Add(entry);
        string json = JsonUtility.ToJson(entries);
        PlayerPrefs.SetString("leaderboardEntries", json);
        PlayerPrefs.Save();

    }


    private void CreateLeaderboardEntryTransform(LeaderboardEntryData leaderboardEntryData, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;

        Transform entryTransform = Instantiate(entryTemplate, container);

        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -transformList.Count * templateHeight);

        entryRectTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
            default: rankString = rank + "th"; break;
        }
        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;

        string name = leaderboardEntryData.playerName;
        entryTransform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = name;

        float time = leaderboardEntryData.time;
        entryTransform.Find("CargoDelivered").GetComponent<TextMeshProUGUI>().text = time.ToString();

        int earnings = gameData.cargo * gameData.moneyPerCargo;
        entryTransform.Find("NetEarnings").GetComponent<TextMeshProUGUI>().text = earnings.ToString();

        float score = gameData.finalScore;
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();





        transformList.Add(entryTransform);
    }


    //created this separate class to make our list of entries to an obj, so we convert them into string using jsonUtility and save in playerPrefs
    private class LeaderboardEntries
    {
        public List<LeaderboardEntryData> leaderboardEntryDataList;
    }


    [System.Serializable] //added this to make it able to pass in functions, in my case setString
    public class LeaderboardEntryData
    {
        public string playerName;
        public float time;
        public int earnings;
        public float score;
    }
}
