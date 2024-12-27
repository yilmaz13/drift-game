using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnClickGotoMenu;
    public static void ClickGotoMenu() { OnClickGotoMenu?.Invoke(); }

    public static event Action OnClickGotoGameScene;
    public static void ClickGotoGameScene() { OnClickGotoGameScene?.Invoke(); }

    public static event Action OnStartGame;
    public static event Action<bool> OnEndGame; 

    public static event Action<Transform> OnSpawnedPlayer;     
    public static void SpawnedPlayer(Transform t) { OnSpawnedPlayer?.Invoke(t); }
    public static event Action OnPlayerDead;
    public static void PlayerDead() { OnPlayerDead?.Invoke(); }

}

