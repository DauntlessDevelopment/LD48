using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Stats stats = new Stats();
    [SerializeField] private int base_health = 100;
    [SerializeField] private int base_mana = 100;
    [SerializeField] private int base_stamina = 100;

    protected int health = 100;
    protected int mana = 100;
    protected int stamina = 100;

    private int str_health_mod = 10;
    private int int_mana_mod = 10;
    private int agi_stamina_mod = 10;

    protected bool is_player = false;

    protected bool alive = true;

    protected float attack_rate = 2.5f;

    protected float last_attack_time = 0;


    public bool IsPlayer() { return is_player; }

    private void Start()
    {
        health = base_health + str_health_mod * stats.GetStrength();
        mana = base_mana + int_mana_mod * stats.GetIntellect();
        stamina = base_stamina + agi_stamina_mod * stats.GetAgility();
    }

    public virtual void ModifyHealth(int amount)
    {
        health += amount;
        Debug.Log("Health modified by " + amount);
        if(health > base_health + str_health_mod * stats.GetStrength())
        {
            health = base_health + str_health_mod * stats.GetStrength();
        }
        else if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Die");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
    }
}
