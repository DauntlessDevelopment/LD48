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

    public MeleeWeapon weapon;

    public bool on_ground;
    public bool sprinting = false;

    void Start()
    {
        ServiceLocator.SetPlayer(this);
        is_player = true;
        Cursor.lockState = CursorLockMode.Locked;
        weapon.AssignOwnership();
    }

    void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();
        
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

        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeSinceLevelLoad > last_attack_time + 1 / attack_rate)
        {
            weapon.Attack();
            last_attack_time = Time.timeSinceLevelLoad;
        }
    }



    public override void ModifyHealth(int amount)
    {
        base.ModifyHealth(amount);
        ServiceLocator.GetUIManager().UpdateHealthUI(health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Walkable" && alive)
        {
            on_ground = true;
            GetComponent<Rigidbody>().velocity = new Vector3();
        }
    }
}
