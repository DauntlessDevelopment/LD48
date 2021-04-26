using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Agent
{
    private PlayerController player = null;
    [SerializeField] private float move_speed = 2f;
    [SerializeField] private float turn_speed = 360f;
    [SerializeField] private Vector3 player_pos;
    private float turning_move_speed_mod = 0.5f;
    [SerializeField]private EnemyType type = EnemyType.soldier;

    public GameObject xp_prefab;

    private Vector3 origin;
    private Vector3 wander_location;

    private float death_time = 0;
    private float despawn_time = 2f;

    private Weapon weapon;

    private ProgressBar health_bar;
    // Start is called before the first frame update
    protected override void Start()
    {
        origin = transform.position;
        wander_location = origin;
        weapon = GetComponentInChildren<Weapon>();
        health_bar = GetComponentInChildren<ProgressBar>();
        base.Start();
    }

    public EnemyType GetEnemyType()
    {
        return type;
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
                //TurnToPlayer();
                TurnToGoal(player.transform.position);
                Vector3 erot = transform.rotation.eulerAngles;
                erot = new Vector3(erot.x, erot.y, 0);
                transform.rotation = Quaternion.Euler(erot);
                if (Vector3.Distance(transform.position, player.transform.position) < weapon.GetWeaponReach() && Time.timeSinceLevelLoad > last_attack_time + 1 / attack_rate)
                {
                    //attack
                    if (weapon is RangedWeapon)
                    {
                        weapon.transform.LookAt(player.transform);
                        
                    }
                    if (weapon.Attack())
                    {
                        last_attack_time = Time.timeSinceLevelLoad;
                        if (GetComponentInChildren<ParticleSystem>() != null)
                        {
                            GetComponentInChildren<ParticleSystem>().Stop();
                            GetComponentInChildren<ParticleSystem>().Play();
                        }
                    }
       
                }
                else
                {
                    //MoveToPlayer();
                    Move((player.transform.position - transform.position).normalized);
                }
            }
            else
            {
                //wander

                Wander();

            }
            
        }
        else
        {
            if(Time.timeSinceLevelLoad > death_time + GlobalVariables.BODY_PERSISTENCE)
            {
                if(type == EnemyType.werm)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
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
        float angle = Vector3.Angle((player.transform.position - transform.position).normalized, transform.forward);

    }

    protected void Wander()
    {
        if(Vector3.Distance(transform.position, wander_location) < 2)
        {
            Vector2 dir = Random.insideUnitCircle;
            float dist = Random.Range(0, 10);

            Vector3 dest = origin + new Vector3(dir.x, 0, dir.y)* dist;

            if(Physics.Raycast(transform.position + new Vector3(0,2,0), new Vector3(dir.x, 0, dir.y), dist))
            {
                //try again
            }
            else
            {
                wander_location = dest;
            }

            ////
            
        }
        else
        {
            //Debug.Log($"Wander Position : {wander_location} |||| Current Position {transform.position} |||| Y change is : {direction.y}");

            TurnToGoal(wander_location);

            Move(transform.forward * 0.2f);

        }

    }

    private void TurnToGoal(Vector3 goal_pos)
    {
        Vector3 direction = (goal_pos - transform.position).normalized;
        Quaternion look_rot = Quaternion.LookRotation(direction, transform.up);
        if (direction != transform.forward)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look_rot, turn_speed * Time.deltaTime);
        }
    }

    private void Move(Vector3 dir)
    {
        float angle = Vector3.Angle(dir, transform.forward);

        if (angle > 90 || angle < -90)
        {
            transform.Translate(transform.forward * move_speed * Time.deltaTime, Space.World);

        }
        else
        {
            transform.Translate(transform.forward * move_speed * turning_move_speed_mod * Time.deltaTime, Space.World);

        }
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
        Instantiate(xp_prefab, transform.position, transform.rotation);
        
    }

    public override void ModifyHealth(int amount)
    {
        base.ModifyHealth(amount);
        UpdateHealthUI(health, GetMaxHealth());
    }

    public void UpdateHealthUI(int current, int max)
    {
        health_bar.current = current;
        health_bar.max = max;
    }


    public enum EnemyType
    {
        werm,
        soldier,
        fly
    }
}
