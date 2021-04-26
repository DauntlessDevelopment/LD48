using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 30f;
    public bool owned_by_player = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Agent>() != null && owned_by_player != collision.gameObject.GetComponent<Agent>().IsPlayer())
        {
            collision.gameObject.GetComponent<Agent>().ModifyHealth(-damage);
        }
        Destroy(gameObject);
    }
}
