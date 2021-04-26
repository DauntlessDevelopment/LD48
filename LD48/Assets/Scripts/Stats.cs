using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private int strength = 1; //health large weapons armour
    private int intellect = 1; //spells robes 
    private int agility = 1; //bows daggers thrown


    public int GetStrength()
    {
        return strength;
    }

    public int GetIntellect()
    {
        return intellect;
    }

    public int GetAgility()
    {
        return agility;
    }

    public void ModifyStrength(int amt)
    {
        strength += amt;
    }

    public void ModifyIntellect(int amt)
    {
        intellect += amt;
    }

    public void ModifyAgility(int amt)
    {
        agility += amt;
    }

}
