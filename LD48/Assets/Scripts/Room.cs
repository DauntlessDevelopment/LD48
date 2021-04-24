using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public bool north_open = false;
    public bool south_open = false;
    public bool east_open = false;
    public bool west_open = false;

    public Vector2Int grid_coords = new Vector2Int();
    public Vector3 world_pos = new Vector3();

    public GameObject room_object;

    public GameObject north_door;
    public GameObject south_door;
    public GameObject east_door;
    public GameObject west_door;

    public Room(Vector2Int coords)
    {
        grid_coords = coords;
    }


    public bool HasClosedDoor()
    {
        return !(north_open && south_open && east_open && west_open);
    }
    public void OpenCloseDoors()
    {
        if(north_open)
        {
            north_door.SetActive(false);
        }
        if(south_open)
        {
            south_door.SetActive(false);
        }
        if (east_open)
        {
            east_door.SetActive(false);
        }
        if (west_open)
        {
            west_door.SetActive(false);
        }
    }

}
