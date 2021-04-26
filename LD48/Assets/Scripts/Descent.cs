using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Generate next floor
            ServiceLocator.GetLevelGeneration().GenerateFloor();
            //Build next floor
            ServiceLocator.GetLevelGeneration().BuildFloor(ServiceLocator.GetLevelGeneration().floor_num - 1);
            //Populate floor
            ServiceLocator.GetLevelGeneration().FillRooms(ServiceLocator.GetLevelGeneration().floor_num - 1);
            //TP to origin of next floor
            Vector3 start_pos = new Vector3();

            if(ServiceLocator.GetCurrentFloor().entrance == FloorManager.EntranceDirection.North)
            {
                start_pos = new Vector3(0, ServiceLocator.GetCurrentFloor().height * 50 + 2, 24);
            }
            else if (ServiceLocator.GetCurrentFloor().entrance == FloorManager.EntranceDirection.South)
            {
                start_pos = new Vector3(0, ServiceLocator.GetCurrentFloor().height * 50 + 2, -24);
            }
            else if (ServiceLocator.GetCurrentFloor().entrance == FloorManager.EntranceDirection.East)
            {
                start_pos = new Vector3(24, ServiceLocator.GetCurrentFloor().height * 50 + 2, 0);
            }
            else if (ServiceLocator.GetCurrentFloor().entrance == FloorManager.EntranceDirection.West)
            {
                start_pos = new Vector3(-24, ServiceLocator.GetCurrentFloor().height * 50 + 2, 0);
            }

            collision.gameObject.transform.position = start_pos;

            ServiceLocator.GetUIManager().LevelUp();
        }


    }
}
