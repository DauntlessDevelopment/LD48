using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{

    public GameObject debug_hit_object;

    public GameObject head;
    public AttackCollider ac_prefab;


    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
            AttackCollider ac = Instantiate(ac_prefab, head.transform.position, head.transform.rotation);
            ac.Initialise(damage, weapon_reach, is_owned_by_player, attack_width);
            return true;
        }
        return false;



    }


}
