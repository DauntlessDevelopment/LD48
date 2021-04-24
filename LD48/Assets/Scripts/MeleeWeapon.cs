using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public WeaponType weaponType = WeaponType.OneHandedSlash;
    private float weapon_reach = 4f;
    private float attack_width = 2f;

    public GameObject debug_hit_object;

    public GameObject head;
    public AttackCollider ac_prefab;

    private bool is_owned_by_player = false;
    private Animator anim;

    public void AssignOwnership()
    {
        is_owned_by_player = GetComponentInParent<Agent>().IsPlayer();
    }

    public float GetWeaponReach()
    {
        return weapon_reach;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("1HSlash"))
        {
            anim.SetTrigger("Attack");
            AttackCollider ac = Instantiate(ac_prefab, head.transform.position, head.transform.rotation);
            ac.Initialise(damage, weapon_reach, is_owned_by_player, attack_width);
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            //{
            //    Debug.Log("Raycast hit");
            //    if (hit.transform.GetComponent<Agent>() != null)
            //    {
            //        Debug.Log("Raycast hit was an agent");
            //        Instantiate(debug_hit_object, hit.point, transform.rotation);
            //        hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(100 * transform.forward, hit.point);
            //    }
            //}
        }



    }

    public enum WeaponType
    { 
        OneHandedSlash,
        TwoHandedSlash,//slash does cut damage
        OneHandedBlunt,
        TwoHandedBlunt//blunt does blunt+knockback
    }


}
