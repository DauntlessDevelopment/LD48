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


    public List<GameObject> enemy_types = new List<GameObject>();

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

    public void PopulateFloor()
    {
        //room by room//
        //divide room into grid of evenly spaced points//
        //point by point//
        //check collision sphere//
        //rng spawn based on density//
        //rng select enemy from list of enemy types//

        foreach(var r in rooms)
        {
            List<Vector3> spawn_points = new List<Vector3>();
            Vector3 sw_corner = r.world_pos - new Vector3(25, 0, 25);

            for(int y = 0; y < 50; y += 5)
            {
                for(int x = 0; x < 50; x += 5)
                {
                    spawn_points.Add(sw_corner + new Vector3(x, 2, y));
                }
            }
            foreach(var sp in spawn_points)
            {
                if(!Physics.CheckSphere(sp, 1f))
                {
                    float rng_spawn_chance = Random.Range(0f, 1f);
                    if(rng_spawn_chance < density)
                    {
                        int rng_spawn_type = Random.Range(0, enemy_types.Count);

                        ServiceLocator.GetSpawnManager().SpawnEnemy(enemy_types[rng_spawn_type].gameObject, sp, enemy_types[rng_spawn_type].transform.rotation);
                        
                    }
                }
            }

        }
    }

    public enum EntranceDirection
    {
        North,
        East,
        South,
        West
    }


}
