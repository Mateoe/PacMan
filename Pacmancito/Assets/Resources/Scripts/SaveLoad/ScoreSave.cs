using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSave : MonoBehaviour
{
    [SerializeField]
    private PacManController _pacman;
    private int _score;
    public ScoreData _scoredata;

    [SerializeField]
    private TextMeshProUGUI _highscore;

    public const string pathData = "Data/score";
    public const string nameFile = "UserData";
    void Start()
    {
        _highscore = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();

        var DataFound = SaveLoad.Load<ScoreData>(pathData,nameFile);

        if(DataFound != null)
        {
            _scoredata = DataFound;
        }
        else
        {
            _scoredata = new ScoreData();
        }

        EventSystem.OnGameOver += HighScore;
        EventSystem.OnStartGame += LoadData;
    }

    void Update()
    {
        _score = _pacman.GetComponent<PacManController>().scoreRead();
    }

    void HighScore()
    {
        if(_scoredata.highscore < _score)
        {
            _scoredata.highscore = _score;
        }
        SaveData();
    }

    void SaveData()
    {
        SaveLoad.Save(_scoredata,pathData,nameFile);
    }

    void LoadData()
    {
        _highscore.text = _scoredata.highscore.ToString();
    }
}
