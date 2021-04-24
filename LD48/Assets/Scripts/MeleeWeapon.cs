using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public WeaponType weaponType = WeaponType.OneHandedSlash;

    private bool is_owned_by_player = false;

    public void AssignOwnership()
    {
        is_owned_by_player = GetComponentInParent<Agent>().IsPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {

            GetComponent<Animator>().SetTrigger("Attack");

    }

    public enum WeaponType
    { 
        OneHandedSlash,
        TwoHandedSlash,//slash does cut damage
        OneHandedBlunt,
        TwoHandedBlunt//blunt does blunt+knockback
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Agent>() != null)
        {
            Agent a = collision.transform.GetComponent<Agent>();
            if(a.IsPlayer() != is_owned_by_player)
            {
                a.ModifyHealth(-damage);
            }
        }
    }
}
