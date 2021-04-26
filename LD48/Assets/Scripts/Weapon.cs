using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    public WeaponType weaponType = WeaponType.OneHandedSlash;
    [SerializeField] protected float weapon_reach = 4f;
    [SerializeField] protected float attack_width = 2f;

    protected bool is_owned_by_player = false;
    protected Animator anim;

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
        
        if(GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        else
        {
            Debug.Log("No animator");
        }
    }

    public virtual bool Attack()
    {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum WeaponType
    {
        OneHandedSlash,
        TwoHandedSlash,//slash does cut damage
        OneHandedBlunt,
        TwoHandedBlunt,//blunt does blunt+knockback
        Ranged
    }
}
