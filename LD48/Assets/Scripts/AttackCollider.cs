using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private int damage = 10;
    private float weapon_reach = 3f;
    private bool is_owned_by_player = false;
    private Vector3 initial_pos = new Vector3();
    private int enemies_hit = 0;

    private float travel_speed = 10f;

    void Start()
    {
        initial_pos = transform.position;
    }

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * travel_speed, Space.World);
        if (enemies_hit >= 2 || Vector3.Distance(initial_pos, transform.position) >= weapon_reach)
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialise(int dmg, float reach, bool owned_by_player, float width)
    {
        damage = dmg;
        weapon_reach = reach;
        is_owned_by_player = owned_by_player;
        transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");

        if (other.GetComponent<Agent>() != null)
        {
            Agent a = other.GetComponent<Agent>();
            if (a.IsPlayer() != is_owned_by_player)
            {
                a.ModifyHealth(-damage);
                enemies_hit++;

                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if(hit.transform.GetComponent<Agent>() != null)
                    {
                        a.GetComponent<Rigidbody>().AddForceAtPosition(100 * transform.forward, hit.point);
                    }
                }

                
            }
        }
    }

}
