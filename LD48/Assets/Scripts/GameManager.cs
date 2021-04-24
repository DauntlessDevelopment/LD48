using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.GetLevelGeneration().GenerateFloor();
        ServiceLocator.GetLevelGeneration().BuildFloor(ServiceLocator.GetLevelGeneration().floor_num - 1);
        ServiceLocator.GetLevelGeneration().FillRooms(ServiceLocator.GetLevelGeneration().floor_num - 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
