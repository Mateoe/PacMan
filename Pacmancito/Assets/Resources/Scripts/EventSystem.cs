using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{

    public static GhostID ghost;
    public static event Action OnPacManDeath;
    public static void PacManDeath(){
        OnPacManDeath?.Invoke();
    }

    public static event Action OnPacManDeathExit;
    public static void PacManDeathExit(){
        OnPacManDeathExit?.Invoke();
    }

    public static event Action OnPacManEnergized;
    public static void PacManEnergized(){
        OnPacManEnergized?.Invoke();
    }

    public static event Action OnVulnerableOut;
    public static void VulnerableOut(){
        OnVulnerableOut?.Invoke();
    }

    public static event Action OnStartGame;
    public static void StartGame(){
        OnStartGame?.Invoke();
    }

    public static event Action OnGhostArrive;
    public static void GhostArrive(GhostID id){
        ghost = id;
        OnGhostArrive?.Invoke();     
    }

    public static event Action OnGhostDisperse;
    public static void GhostDisperse(GhostID id){
        ghost = id;
        OnGhostDisperse?.Invoke();
    }

    public static event Action OnGhostDeath;
    public static void GhostDeath(GhostID id){
        ghost = id;
        OnGhostDeath?.Invoke();
    }

    public static event Action OnGameOver;
    public static void GameOver(){
        OnGameOver?.Invoke();
    }

    public static event Action OnStartButtonPulse;
    public static void StartButtonPulse(){
        OnStartButtonPulse?.Invoke();
    }

    public static event Action OnGhostInit;
    public static void GhostInit(){
        OnGhostInit?.Invoke();
    }
}