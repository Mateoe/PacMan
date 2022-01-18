using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _score1UP;

    public int _score;

    private GameObject _pacman;

    void Start()
    {
        _pacman = GameObject.Find("PacMan");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        _score = _pacman.GetComponent<PacManController>().scoreRead();
        _score1UP.text = string.Format(_score.ToString());
    }
}

