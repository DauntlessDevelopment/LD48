using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static PlayerController player;
    public static void SetPlayer(PlayerController p)
    {
        player = p;
    }
    public static PlayerController GetPlayerController()
    {
        return player;
    }


    private static UIManager UI_manager;
    public static void SetUIManager(UIManager UIM)
    {
        UI_manager = UIM;
    }
    public static UIManager GetUIManager()
    {
        return UI_manager;
    }


    private static LevelGeneration levelGeneration;
    public static void SetLevelGeneration(LevelGeneration generation)
    {
        levelGeneration = generation;
    }
    public static LevelGeneration GetLevelGeneration()
    {
        return levelGeneration;
    }


    private static FloorManager currentFloor;
    public static void SetCurrentFloor(FloorManager floor)
    {
        currentFloor = floor;
    }
    public static FloorManager GetCurrentFloor()
    {
        return currentFloor;
    }


    private static SpawnManager spawnManager;
    public static void SetSpawnManager(SpawnManager spawn)
    {
        spawnManager = spawn;
    }
    public static SpawnManager GetSpawnManager()
    {
        return spawnManager;
    }
}
