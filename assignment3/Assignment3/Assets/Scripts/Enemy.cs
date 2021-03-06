﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip hurtSound;
    public AudioClip deathSound;

    private ScoreController scoreController;

    public GameObject player;
    private GameObject myGun;
    public float firingDelay;
    private float lastShot;
    public int life;

    public float fireMargin;

    public bool isStatic;

    protected bool isDead = false;
    protected GunType lastGunToShoot;
    protected bool lastShotWasHeadShot;
    private int headShotMultiplayer = 4;
    public Collider headCollider;

    public virtual void injure(int hp, GunType gun, Collider collider)
    {
        if (isDead)
            return;
        if (collider == headCollider)
        {
            life -= headShotMultiplayer * hp;
            lastShotWasHeadShot = true;
        }
        else
        {
            lastShotWasHeadShot = false;
            life -= hp;
        }
        lastGunToShoot = gun;
        if (life > 0)
            GetComponent<Animator>().SetTrigger("gotHurt");
        else
        {
            this.isDead = true;
            if (lastGunToShoot == GunType.Bazooka)
            {
                GetComponent<Animator>().SetBool("explosionKilled", true);
                scoreController.addScore("XPlosion!!!", 500);
            }
            else if (lastShotWasHeadShot)
            {
                GetComponent<Animator>().SetBool("headshotKilled", true);
                scoreController.addScore("Headshot!", 500);
            }
            else
                GetComponent<Animator>().SetBool("normalKilled", true);
            scoreController.addScore("Kill", 250);

        }
    }

    protected void initialize()
    {
        myGun = GetComponentInChildren<Gun>().gameObject;
        player = GameObject.Find("FPSController");
        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        fireMargin = 200;
    }

    // Use this for initialization
    void Start()
    {
        initialize();
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (this.isDead)
            return;

        if (isStatic)
        {
            //rotate enemy to always look at the player
            Vector3 lookAt = transform.position - player.transform.GetChild(0).position;
            lookAt.y = 0;
            transform.rotation = Quaternion.LookRotation(lookAt, Vector3.up);
        }
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        Debug.DrawRay(transform.position, direction);
        if (direction.magnitude < fireMargin)
        {
            if ((Time.realtimeSinceStartup - lastShot) > firingDelay)
            {
                RaycastHit objHit;
                if (Physics.Raycast(transform.position, direction.normalized, out objHit)
                    && objHit.transform.gameObject == player)
                {
                    lastShot = Time.realtimeSinceStartup;
                    //avoid bugs
                    if (myGun != null)
                    {
                        myGun.GetComponentInChildren<Gun>().fire();
                    }
                }

            }
        }

    }

    public void endDeathAnimation()
    {
        //let go the gun
        if (myGun != null)
        {
            myGun.GetComponent<Gun>().picked = false;
            myGun.gameObject.transform.parent = null;
            myGun.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            myGun.transform.eulerAngles = new Vector3(-90, 0, 0);
        }
        this.gameObject.SetActive(false);
    }

    public void playHurtSound()
    {
        GetComponent<AudioSource>().clip = hurtSound;
        GetComponent<AudioSource>().Play();
    }

    public void playDeathSound()
    {
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().Play();
    }

    public void explodeHeadParticle()
    {
        Transform headExplosion = transform.Find("ExplosionHead");
        headExplosion.GetComponent<ParticleSystem>().Play();
        headExplosion.GetComponent<AudioSource>().Play();
        playDeathSound();
    }

    public void explodeBodyParticle()
    {
        Transform bodyExplosion = transform.Find("ExplosionParticle");
        bodyExplosion.GetComponent<ParticleSystem>().Play();
        bodyExplosion.GetComponent<AudioSource>().Play();
        playDeathSound();
    }
}
