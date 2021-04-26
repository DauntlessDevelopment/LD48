using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Agent
{
    private float move_speed = 5f;
    private float sprint_modifier = 2f;
    private float turn_speed = 200f;
    public GameObject head;
    public GameObject feet;

    public Weapon ranged;
    public Weapon melee;

    public bool on_ground;
    public bool sprinting = false;

    private int xp = 0;

    private int level = 0;

    private int stat_ponts = 0;

    public bool levelled = false;

    public void SpendStatPoint()
    {
        stat_ponts--;
    }

    public int GetStatPoints()
    {
        return stat_ponts;
    }

    protected override void Start()
    {
        base.Start();
        ServiceLocator.SetPlayer(this);
        is_player = true;
        Cursor.lockState = CursorLockMode.Locked;
        ranged.AssignOwnership();
        melee.AssignOwnership();
    }

    void Update()
    {
        if(alive)
        {
            HandleKeyboardInput();
            HandleMouseInput();

        }

    }

    public int GetLevel()
    {
        return level;
    }

    private void CalculateLevel()
    {
        if(xp > level*20 + 20)
        {
            level++;
            stat_ponts++;
            if(level % 5 == 0)
            {
                stat_ponts += 2;
            }
            levelled = true;
        }
    }

    private void HandleKeyboardInput()
    {
        if(on_ground)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sprinting = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sprinting = false;
            }

            float speed = move_speed;
            if (sprinting)
            {
                speed *= sprint_modifier;
                ModifyStamina(-10 * Time.deltaTime);
                last_stamina_tick = Time.timeSinceLevelLoad;
            }

            Vector3 move_amount = new Vector3();

            if (Input.GetKey(KeyCode.W))
            {
                move_amount += transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move_amount += -transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move_amount += transform.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                move_amount +=  -transform.right * speed * Time.deltaTime;
            }
            transform.Translate(move_amount, Space.World);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForce(transform.up * 200f + move_amount.normalized * 50f * speed);
                on_ground = false;
                ModifyStamina(-30);
            }
        }

        

        



    }

    private void HandleMouseInput()
    {
        if(Input.GetAxis("Mouse X") !=0)
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * turn_speed * Time.deltaTime,0));
        }
        if(Input.GetAxis("Mouse Y") != 0)
        {
            head.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * turn_speed * Time.deltaTime,0,0));
        }

        if(Input.GetAxis("Mouse ScrollWheel") !=0)
        {
            SwitchWeapon();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeSinceLevelLoad > last_attack_time + 1 / attack_rate)
        {
            if(ranged.gameObject.activeSelf)
            {
                if(mana > 10)
                {
                    ModifyMana(-10);
                    ranged.Attack();
                    ranged.GetComponentInChildren<ParticleSystem>().Play();
                    last_attack_time = Time.timeSinceLevelLoad;

                    
                    
                }

            }
            else
            {
                if(stamina > 10)
                {
                    //decrease stam
                    ModifyStamina(-10);
                    melee.Attack();
                    last_attack_time = Time.timeSinceLevelLoad;
                }

            }

            
        }
    }

    protected override void Die()
    {
        base.Die();
        ServiceLocator.GetUIManager().ShowDeathScreen();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void SwitchWeapon()
    {
        ranged.gameObject.SetActive(!ranged.gameObject.activeSelf);
        
        melee.gameObject.SetActive(!melee.gameObject.activeSelf);

    }


    public override void ModifyHealth(int amount)
    {
        base.ModifyHealth(amount);
        ServiceLocator.GetUIManager().UpdateHealthUI(health,GetMaxHealth());
    }

    public override void ModifyMana(int amount)
    {
        base.ModifyMana(amount);
        ServiceLocator.GetUIManager().UpdateManaUI(mana, GetMaxMana());
    }

    public override void ModifyStamina(float amount)
    {
        base.ModifyStamina(amount);
        ServiceLocator.GetUIManager().UpdateStaminaUI((int)stamina, GetMaxStamina());

    }

    

    private void AddXP(int amount)
    {
        xp++;
        CalculateLevel();
        ServiceLocator.GetUIManager().UpdateXPUI(xp, level * 20, 20 + level * 20);

    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Walkable" && alive)
        {
            on_ground = true;
            GetComponent<Rigidbody>().velocity = new Vector3();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "XP")
        {
            Destroy(other.gameObject);
            AddXP(1);
            
        }
    }
}
