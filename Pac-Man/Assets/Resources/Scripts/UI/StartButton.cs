using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Button _start;

    [SerializeField]
    private GameObject _gameManager;

    void Start()
    {
        _start = gameObject.GetComponent<UnityEngine.UI.Button>();
        _start.onClick.AddListener(GameStart);
    }

    void GameStart()
    {
        gameObject.SetActive(false);
        _gameManager.SetActive(true);
    }
}
