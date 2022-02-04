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

    private float _vulnerabletime;
    private float _auxtimer;

    private bool _energized;

    [SerializeField]
    public static int _pelletCount;

    [SerializeField]
    private GameObject _generator;

    private Vector3 _pacManPosition;
    private Vector3 _blinkyPosition;
    private Vector3 _inkyPosition;
    private Vector3 _pinkyPosition;
    private Vector3 _clydePosition;


    void Start()
    {

        _audio = gameObject.GetComponent<AudioSource>();
        _pacmanAudio = PacMan.GetComponent<AudioSource>();
        _audio.clip = _clip1;
        _score = GameObject.Find("CurrentScore").GetComponent<CurrentScore>();
        _generator = GameObject.Find("Generator");
        _energized = false;
        _vulnerabletime = 8;
        _auxtimer = _vulnerabletime;

        EventSystem.OnStartButtonPulse += GameStarted;
        EventSystem.OnStartGame += game;
        EventSystem.OnPacManEnergized += Energized;
        EventSystem.OnGhostArrive += Normal;
        EventSystem.OnPacManDeath += Deactivate;
        EventSystem.OnPacManDeathExit += Reset;
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

        for(int i = 0; i < _generator.transform.childCount; i++)
        {
            _pelletCount +=1;
        }

        for(int i = 0; i < GameObject.Find("EnergizerPellets").transform.childCount; i++)
        {
            _pelletCount +=1;
        }

        PacMan.SetActive(true);
        Blinky.SetActive(true);
        Pinky.SetActive(true);
        Inky.SetActive(true);
        Clyde.SetActive(true);
        _score.enabled = true;

        _pacManPosition = PacMan.transform.position;
        _blinkyPosition = Blinky.transform.position;
        _inkyPosition = Inky.transform.position;
        _pinkyPosition = Pinky.transform.position;
        _clydePosition = Clyde.transform.position;

    }

    void Energized()
    {
        _pacmanAudio.mute = true;
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
            _pacmanAudio.mute = false;
            _auxtimer = _vulnerabletime;
            _energized = false;
        }
    }

    void Deactivate()
    {
        EventSystem.GhostArrive(GhostID.Blinlky);
        EventSystem.GhostArrive(GhostID.Inky);
        EventSystem.GhostArrive(GhostID.Pinky);
        EventSystem.GhostArrive(GhostID.Clyde);
        Blinky.SetActive(false);
        Pinky.SetActive(false);
        Inky.SetActive(false);
        Clyde.SetActive(false);

        _audio.loop = false;
        _audio.clip = _clip2;
        _audio.Play();

    }

    void Reset()
    {
        PacMan.transform.position = _pacManPosition;
        Blinky.transform.position = _blinkyPosition;
        Pinky.transform.position = _pinkyPosition;
        Inky.transform.position = _inkyPosition;
        Clyde.transform.position = _clydePosition;

        PacMan.SetActive(true);
        Blinky.SetActive(true);
        Pinky.SetActive(true);
        Inky.SetActive(true);
        Clyde.SetActive(true);
    }

    public static void PelletReduce()
    {
        _pelletCount -=1;

        if(_pelletCount == 0)
        {
            EventSystem.GameOver();
        }
    }

    private void OnDestroy()
    {
        EventSystem.OnStartButtonPulse -= GameStarted;
        EventSystem.OnStartGame -= game;
        EventSystem.OnPacManEnergized -= Energized;
        EventSystem.OnGhostArrive -= Normal;
        EventSystem.OnPacManDeath -= Deactivate;
        EventSystem.OnPacManDeathExit -= Reset;
    }

}
