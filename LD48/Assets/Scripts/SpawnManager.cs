using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<EnemyAI> spawned_enemies = new List<EnemyAI>();
    // Start is called before the first frame update

    private void Awake()
    {
        ServiceLocator.SetSpawnManager(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnEnemy(GameObject prefab, Vector3 location, Quaternion rotation)
    {
        GameObject spawned = Instantiate(prefab, location, rotation);
        spawned_enemies.Add(spawned.GetComponent<EnemyAI>());
        return spawned;
    }
}
