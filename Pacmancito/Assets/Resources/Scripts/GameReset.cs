using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{

    void Start()
    {
        EventSystem.OnGameOver += Reset;
    }

    void Reset()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        EventSystem.OnGameOver -= Reset;
        Reset();
    }
}
