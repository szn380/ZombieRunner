using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitpoints = 100f;    // total health (hit points) of the enemy instance
    [SerializeField] GameObject deadEnemy;      // the game object for a dead enemy, use it when enemy is dead

    public void TakeDamage(float amountOfDamage)
        // reduce Enemy health by specified amount of damage
        // if Enemy is now dead, replace the enemy with a dead enemy
    {
        hitpoints -= amountOfDamage;
        Debug.Log(gameObject.name + " has damage level " + hitpoints);
        if (hitpoints <= 0)
        {
            deadEnemy.transform.position = gameObject.transform.position;
            deadEnemy.transform.rotation = gameObject.transform.rotation;
            Destroy(gameObject);
            Instantiate(deadEnemy);
        }
    }
}
