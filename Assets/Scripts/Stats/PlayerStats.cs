﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        if (newItem != null) 
        {
            armour.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armour.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
    // Update is called once per frame
    /*void Update()
    { 
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);

        }
        
    }*/
}
