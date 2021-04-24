using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager
{
    public List<Room> rooms = new List<Room>();
    public int num_rooms = 10;
    public float max_dist = 6;

    public int height = 0;

    public Room exit_room;
    public EntranceDirection exit = EntranceDirection.North;
    public EntranceDirection entrance = EntranceDirection.South;

    float density = 0.1f;


    List<EnemyAI> enemy_types = new List<EnemyAI>();

    public FloorManager()
    {
        rooms.Add(new Room(new Vector2Int()));
    }

    public Room GetRoom(Vector2Int coords)
    {
        foreach(var r in rooms)
        {
            if(r.grid_coords == coords)
            {
                return r;
            }
        }
        return null;
    }

    public enum EntranceDirection
    {
        North,
        East,
        South,
        West
    }


}
