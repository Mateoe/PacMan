using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentScore : PacManController
{
    [SerializeField]
    private TextMeshProUGUI _score1UP;

    private int _score;

    void Start()
    {
        _score = score;
    }

    // Update is called once per frame
    void Update()
    {
        _score1UP.text = string.Format(score.ToString());
    }
}
