using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riffle : MonoBehaviour
{
    [Header("Rifle")]
    public Camera camera;
    public float giveDamage = 10f;
    public float shootRange = 100f;
    public float fireCharge = 15f;
    public Animator animator;
    public PlayerScript player;

    [Header("Rifle Ammunition and shooting")]
    public float nextTimeToShoot = 0f;
    private int maximumAmunition = 20;
    private int mag = 15;
    private int currentAmun;
    public float reloadingtime = 1.3f;
    private bool setReloading = false;


    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject impactEffect;


    //[Header("Sounds And UIs")]

    void Awake()
    {
        currentAmun = maximumAmunition;
    }


    // Update is called once per frame
    void Update()
    {
        if (setReloading)
            return;
        if(currentAmun <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);

            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            
            animator.SetBool("FireWalk", false);
        }
    }

    void Shoot()
    {
        if(mag == 0)
        {
            //no Ammo

            return;
        }
        currentAmun--;

        if (currentAmun == 0)
        {
            mag--;
        }
        muzzleSpark.Play();
        RaycastHit hitInfo;

        if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hitInfo,shootRange))
        {
            Debug.Log(hitInfo.transform.name);
            Objects objects = hitInfo.transform.GetComponent<Objects>();

            if(objects != null)
            {
                objects.objectHitDamage(giveDamage);
                GameObject impactGo = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 1f);
            }
            else
            {
                GameObject impactGo = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 20f);
            }
        }
        //muzzleSpark.Stop();
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.sprint = 0f;
        setReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadingtime);
        animator.SetBool("Reloading", false);
        currentAmun = maximumAmunition;
        player.playerSpeed = 1.9f;
        player.sprint = 3f;
        setReloading = false;
    }
}
