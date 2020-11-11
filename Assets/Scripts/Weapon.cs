using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;               // need player camera to know where to raycast from
    [SerializeField] float range = 100f;            // how far to extend raycast (how far away should weapon hit)
    [SerializeField] float damage = 10f;            // how much health damage does the weapon do
    [SerializeField] ParticleSystem muzzleFlash;    // particle effect for muzzle flash
    [SerializeField] GameObject hitEffect;          // use game object for hit particle effect so it can be destroyed (not needed after some time)
    public AudioSource gunShot;                     // Audio clip for gun shot sound

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // player is shooting his gun
        {
            Shoot();
        }
    }

    private void Shoot()
        // use a ray cast to see if an enemy has been hit
        // if object hit is an enemy, apply appropriate amount of damage
    {
        PlayMuzzleFlash();
        gunShot.Play();
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hit, range)) {
            Transform rootTransform = GetRootParentTransform(hit.transform);
            CreateHitImpact(hit);
                // Debug.Log("We hit " + rootTransform.name);
                // Debug.Log("Tag is " + rootTransform.gameObject.tag);
            if (rootTransform.CompareTag("Enemy")) {
                EnemyHealth target = rootTransform.GetComponent<EnemyHealth>();
                    // Debug.Log("Damage Increment is " + damage);
                target.TakeDamage(damage);
            }

        }
    }

    private void CreateHitImpact(RaycastHit hit)
        // instantiate the hit particle effect at the position that the raycast hit
        // destroy the effect after some time so that the hit effects do not keep collecting up
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f); // destory hit effect after 1 second
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private Transform GetRootParentTransform(Transform currentTransform)
        // returns the transform of the top level (root) game object 
        // associated with the game object that has been hit
    {
        Debug.Log("Starting with " + currentTransform.name);
        while (currentTransform != null)
        {
            if (currentTransform.parent != null)
            {
                // Debug.Log("Move to level " + lastTransform.name);
                currentTransform = currentTransform.parent;
            }
            else
            {
                return (currentTransform);
            }
        }
        return (currentTransform);
    }
}
