using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Agent
{
    private float move_speed = 5f;
    private float turn_speed = 200f;
    public GameObject head;

    public MeleeWeapon weapon;

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
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * move_speed * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * move_speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * move_speed * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * move_speed * Time.deltaTime, Space.World);
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
}
