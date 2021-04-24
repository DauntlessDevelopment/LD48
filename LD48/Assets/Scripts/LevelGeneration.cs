using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    List<FloorManager> floors = new List<FloorManager>();


    public GameObject room_prefab;
    public GameObject stairs_prefab;

    public int floor_num = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ServiceLocator.SetLevelGeneration(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateFloor()
    {
        FloorManager new_floor = new FloorManager();
        Room current_room = new_floor.rooms[0];
        if(floors.Count >0)
        {
            new_floor.height = floors[floors.Count - 1].height - 5;
        }
        while (new_floor.rooms.Count < new_floor.num_rooms)
        {
            int north = 0;
            int east = 0;
            List<Vector2Int> directions = new List<Vector2Int>();
            if(new_floor.entrance != FloorManager.EntranceDirection.North)
            {
                directions.Add(new Vector2Int(0, 1));
            }
            if (new_floor.entrance != FloorManager.EntranceDirection.South)
            {
                directions.Add(new Vector2Int(0, -1));
            }
            if (new_floor.entrance != FloorManager.EntranceDirection.East)
            {
                directions.Add(new Vector2Int(1, 0));
            }
            if (new_floor.entrance != FloorManager.EntranceDirection.West)
            {
                directions.Add(new Vector2Int(-1, 0));
            }

            int dir_id = Random.Range(0, directions.Count);

            Room new_room = new Room(current_room.grid_coords + directions[dir_id]);

            north = directions[dir_id].y;
            east = directions[dir_id].x;


            if(Vector2Int.Distance(new_floor.rooms[0].grid_coords, new_room.grid_coords) > new_floor.max_dist)
            {
                current_room = new_floor.rooms[0];
                continue;
            }

            if (east == 1)
            {
                current_room.east_open = true;
            }
            else if (east == -1)
            {
                current_room.west_open = true;
            }
            else if (north == 1)
            {
                current_room.north_open = true;
            }
            else if (north == -1)
            {
                current_room.south_open = true;
            }

            if (new_floor.GetRoom(new_room.grid_coords) == null)
            {
                new_floor.rooms.Add(new_room);
                current_room = new_room;
            }
            else
            {
                current_room = new_floor.GetRoom(new_room.grid_coords);
            }

            if(east == 1)
            {
                current_room.west_open = true;
            }
            else if(east == -1)
            {
                current_room.east_open = true;
            }
            else if(north == 1)
            {
                current_room.south_open = true;
            }
            else if(north == -1)
            {
                current_room.north_open = true;
            }
        }
        Room furthest_room = new_floor.rooms[0];
        foreach(var r in new_floor.rooms)
        {
            if(r.HasClosedDoor() && Vector2Int.Distance(r.grid_coords, new_floor.rooms[0].grid_coords) > Vector2Int.Distance(furthest_room.grid_coords, new_floor.rooms[0].grid_coords))
            {
                furthest_room = r;
            }
        }


        if(!furthest_room.north_open)
        {
            new_floor.exit = FloorManager.EntranceDirection.North;
            furthest_room.north_open = true;
        }
        else if(!furthest_room.south_open)
        {
            new_floor.exit = FloorManager.EntranceDirection.South;
            furthest_room.south_open = true;
        }
        else if(!furthest_room.east_open)
        {
            new_floor.exit = FloorManager.EntranceDirection.East;
            furthest_room.east_open = true;
        }
        else
        {
            new_floor.exit = FloorManager.EntranceDirection.West;
            furthest_room.west_open = true;
        }
        new_floor.exit_room = furthest_room;

        Debug.Log($"Floor count is : {floors.Count}");
        floors.Add(new_floor);
        Debug.Log($"Added floor, floor count is : {floors.Count}");
        floor_num++;
        ServiceLocator.SetCurrentFloor(new_floor);
    }

    public void BuildFloor(int id)
    {
        Debug.Log($"Floor count is : {floors.Count}");
        if (floors.Count <= id)
        {
            return;
        }
        Debug.Log($" {floors.Count} ");

        foreach (var r in floors[id].rooms)
        {

            Vector3 world_pos = new Vector3(r.grid_coords.x, floors[id].height, r.grid_coords.y) * 50f;
            GameObject new_room = Instantiate(room_prefab, world_pos, new Quaternion());

            r.room_object = new_room;

            r.north_door = new_room.GetComponent<MonoRoom>().north;
            r.south_door = new_room.GetComponent<MonoRoom>().south;
            r.east_door = new_room.GetComponent<MonoRoom>().east;
            r.west_door = new_room.GetComponent<MonoRoom>().west;


            r.OpenCloseDoors();
        }

        Vector3 world_p = new Vector3(floors[id].exit_room.grid_coords.x, floors[id].height, floors[id].exit_room.grid_coords.y) * 50f;
        Vector3 offset = new Vector3();
        Quaternion rot = new Quaternion();
        if(floors[id].exit == FloorManager.EntranceDirection.North)
        {
            rot = Quaternion.Euler(new Vector3(0, 180, 0));
            offset = new Vector3(0, 0, 50);
        }
        else if (floors[id].exit == FloorManager.EntranceDirection.South)
        {
            rot = Quaternion.Euler(new Vector3(0, 0, 0));
            offset = new Vector3(0, 0, -50);
        }
        else if (floors[id].exit == FloorManager.EntranceDirection.East)
        {
            rot = Quaternion.Euler(new Vector3(0, -90, 0));
            offset = new Vector3(50, 0, 0);
        }
        else
        {
            rot = Quaternion.Euler(new Vector3(0, 90, 0));
            offset = new Vector3(-50, 0, 0);
        }

        GameObject new_stairs = Instantiate(stairs_prefab, world_p + offset, rot);
    }
}
