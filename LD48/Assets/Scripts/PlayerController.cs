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
            transform.Translate(transform.forward * move_speed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * move_speed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * move_speed * Time.deltaTime);
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

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.Attack();
        }
    }
}
