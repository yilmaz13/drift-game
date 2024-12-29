using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnClickGotoMenu;
    public static void ClickGotoMenu() { OnClickGotoMenu?.Invoke(); }

    public static event Action OnClickGotoGameScene;
    public static void ClickGotoGameScene() { OnClickGotoGameScene?.Invoke(); }   
    
    public static event Action OnClickLevelRestart;
    public static void ClickLevelRestart() { OnClickLevelRestart?.Invoke(); } 
    
    public static event Action OnClickLevelNext;
    public static void ClickLevelNext() { OnClickLevelNext?.Invoke(); }

    public static event Action OnStartGame;

    public static event Action<bool> OnEndGame;
    public static void EndGame(bool succes) { OnEndGame?.Invoke(succes); }

    public static event Action<Transform> OnSpawnedPlayer;     
    public static void SpawnedPlayer(Transform t) { OnSpawnedPlayer?.Invoke(t); }
    public static event Action OnPlayerDead;
    public static void PlayerDead() { OnPlayerDead?.Invoke(); }

}

