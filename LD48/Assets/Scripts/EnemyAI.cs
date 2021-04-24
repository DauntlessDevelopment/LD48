using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Agent
{
    private PlayerController player = null;
    private float move_speed = 2f;
    private float turn_speed = 360f;
    [SerializeField]private Vector3 player_pos;


    private float death_time = 0;
    private float despawn_time = 2f;

    private MeleeWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<MeleeWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            //if ray can hit player
            if (player != null)
            {
                //player_pos = player.transform.position;
                TurnToPlayer();
                if (Vector3.Distance(transform.position, player.transform.position) < weapon.GetWeaponReach() && Time.timeSinceLevelLoad > last_attack_time + 1 / attack_rate)
                {
                    //attack
                    last_attack_time = Time.timeSinceLevelLoad;
                    weapon.Attack();
                }
                else
                {
                    MoveToPlayer();
                }
            }
            else
            {
                //wander
            }
            
        }
        else
        {
            if(Time.timeSinceLevelLoad > death_time + GlobalVariables.BODY_PERSISTENCE)
            {
                Destroy(this.gameObject);
            }
        }
        

    }

    protected void TurnToPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion look_rot = Quaternion.LookRotation(direction, transform.up);
        if (direction != transform.forward)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look_rot, turn_speed * Time.deltaTime);
        }
    }

    protected void MoveToPlayer()
    {



        //transform.Translate(direction * move_speed * Time.deltaTime);
        transform.Translate(transform.forward * move_speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            player = other.GetComponent<PlayerController>();
        }
    }

    protected override void Die()
    {
        base.Die();
        death_time = Time.timeSinceLevelLoad;
    }
}
