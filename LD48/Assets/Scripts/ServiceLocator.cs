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

}
