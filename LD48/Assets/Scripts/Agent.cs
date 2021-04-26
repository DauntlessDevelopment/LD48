using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Stats stats = new Stats();
    [SerializeField] private int base_health = 100;
    [SerializeField] private int base_mana = 100;
    [SerializeField] private float base_stamina = 100;

    [SerializeField] protected int health = 100;
    [SerializeField] protected int mana = 100;
    [SerializeField] protected float stamina = 100;

    private int str_health_mod = 10;
    private int int_mana_mod = 10;
    private int agi_stamina_mod = 10;

    protected bool is_player = false;

    protected bool alive = true;

    protected float attack_rate = 2.5f;

    protected float last_attack_time = 0;
    protected float last_health_tick = 0;
    protected float last_stamina_tick = 0;


    private float last_mana_tick = 0;

    public bool IsPlayer() { return is_player; }

    protected virtual void Start()
    {
        health = base_health + str_health_mod * stats.GetStrength();
        mana = base_mana + int_mana_mod * stats.GetIntellect();
        stamina = base_stamina + agi_stamina_mod * stats.GetAgility();
        ModifyHealth(0);
        ModifyMana(0);
        ModifyStamina(0);
    }

    public virtual void ModifyHealth(int amount)
    {
        health += amount;
        Debug.Log("Health modified by " + amount);
        if(health > GetMaxHealth())
        {
            health = GetMaxHealth();
        }
        else if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void LateUpdate()
    {
        if(alive)
        {
            UpdateResources();

        }
    }

    private void UpdateResources()
    {
        if (Time.timeSinceLevelLoad > last_mana_tick + 10)
        {
            ModifyMana(10 + stats.GetIntellect() * 2);

            last_mana_tick = Time.timeSinceLevelLoad;
        }
        if (Time.timeSinceLevelLoad > last_health_tick + 10)
        {
            ModifyHealth(10 + stats.GetStrength() * 2);

            last_health_tick = Time.timeSinceLevelLoad;
        }
        if (Time.timeSinceLevelLoad > last_stamina_tick + 1)
        {
            ModifyStamina(10 + stats.GetAgility() * 2);

            last_stamina_tick = Time.timeSinceLevelLoad;
        }
    }

    public virtual void ModifyMana(int amount)
    {
        mana += amount;

        if(mana > GetMaxMana())
        {
            mana = GetMaxMana();
        }
        else if(mana <0)
        {
            mana = 0;
        }
    }

    public virtual void ModifyStamina(float amount)
    {
        stamina += amount;

        if (stamina > GetMaxStamina())
        {
            stamina = GetMaxStamina();
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }
    }

    public int GetMaxHealth()
    {
        return base_health + str_health_mod * stats.GetStrength();
    }

    public int GetMaxMana()
    {
        return base_mana + int_mana_mod * stats.GetIntellect();
    }

    public int GetMaxStamina()
    {
        return (int)base_stamina + agi_stamina_mod * stats.GetAgility();
    }



    protected virtual void Die()
    {
        Debug.Log("Die");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        alive = false;

        rb.AddForceAtPosition(-transform.forward * 10, transform.position + new Vector3(0,1,0));
    }
}
