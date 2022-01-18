using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manage : MonoBehaviour
{
    [SerializeField]
    private GameObject PacMan;

    [SerializeField]
    private GameObject Blinky;

    [SerializeField]
    private GameObject Pinky;

    [SerializeField]
    private GameObject Inky;

    [SerializeField]
    private GameObject Clyde;

    private AudioSource _audio;
    private AudioSource _pacmanAudio;

    private CurrentScore _score;

    [SerializeField]
    private AudioClip _clip1;

    [SerializeField]
    private AudioClip _clip2;

    [SerializeField]
    private AudioClip _clip3;

    private float _vulnerabletime = Vulnerable._staticTimer;
    private float _auxtimer;

    private bool _energized;


    void Start()
    {

        _audio = gameObject.GetComponent<AudioSource>();
        _pacmanAudio = PacMan.GetComponent<AudioSource>();
        _audio.clip = _clip1;
        _score = GameObject.Find("CurrentScore").GetComponent<CurrentScore>();
        _energized = false;
        _auxtimer = _vulnerabletime;

        EventSystem.OnStartButtonPulse += GameStarted;
        EventSystem.OnStartGame += game;
        EventSystem.OnPacManEnergized += Energized;
        EventSystem.OnGhostArrive += Normal;

        EventSystem.StartButtonPulse();
    }
    void GameStarted()
    {
        _audio.Play();
        Invoke("CallEvent",_clip1.length);
    }

    void CallEvent()
    {
        EventSystem.StartGame();
    }

    void game()
    {
        Debug.Log("start");
        PacMan.SetActive(true);
        Blinky.SetActive(true);
        Pinky.SetActive(true);
        Inky.SetActive(true);
        Clyde.SetActive(true);
        _score.enabled = true;
    }

    void Energized()
    {
        _pacmanAudio.volume = 0;
        _audio.clip = _clip3;
        _audio.Play();
        _audio.loop = true;
        _energized = true;
    }

    void Normal()
    {
        _audio.clip = null;
        _audio.loop = false;
    }

    void Update()
    {

        if (_energized)
        {
            _auxtimer-=Time.deltaTime;
        }
        if (_auxtimer < 0)
        {
            EventSystem.VulnerableOut();
            _pacmanAudio.volume = 1;
            _auxtimer = _vulnerabletime;
            _energized = false;
        }
    }

}
