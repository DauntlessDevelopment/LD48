using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject projectile;
    public Transform projectile_origin;

    bool shooting = false;

    // Update is called once per frame
    void Update()
    {
        if(shooting && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            GameObject proj = Instantiate(projectile, projectile_origin.position, projectile_origin.rotation);
            if (proj.GetComponent<Projectile>() != null)
            {
                proj.GetComponent<Projectile>().damage = damage;
                proj.GetComponent<Projectile>().owned_by_player = is_owned_by_player;
            }
            shooting = false;
        }
    }

    public override bool Attack()
    {

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot") && !shooting)
        {
            anim.SetTrigger("Shoot");
            shooting = true;
            return true;
        }
        return false;
        
        
    }
}
