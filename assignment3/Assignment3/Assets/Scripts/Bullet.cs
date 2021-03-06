﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    public float massFactor;
    public float maxBulletTime;
    public int bulletDamage;
    private bool collided;
    public GunType gunType;
    public bool destroyable; 

    public Collider ignorable;

    void Start()
    {
        collided = false;
        if (destroyable)
        {
            Destroy(gameObject, maxBulletTime);
        }
    }

    void Update()
    {
        if (destroyable && !collided)
        {
            transform.Translate(new Vector3(0, -bulletSpeed, 0) * Time.deltaTime);
            //hardcoded but better for collision detection
            transform.Translate(new Vector3(0, -massFactor, 0) * Time.deltaTime, Space.World);
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col == ignorable)
            return;

        collided = true;
        if (this.GetComponent<ParticleSystem>())
        {
            this.GetComponent<ParticleSystem>().Play();
        }
        if (destroyable)
        {
            this.GetComponent<Collider>().enabled = false;
            foreach (MeshRenderer childMesh in this.GetComponentsInChildren<MeshRenderer>())
            {
                childMesh.enabled = false;
            }
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        if (col.gameObject.GetComponent<Player>())
        {
            col.gameObject.GetComponent<Player>().injure(this.bulletDamage);
        }
        if (col.gameObject.GetComponent<Enemy>())
        {
            col.gameObject.GetComponent<Enemy>().injure(this.bulletDamage, gunType, col);
        }
        if (col.gameObject.GetComponent<Box>())
        {
            col.gameObject.GetComponent<Box>().explode();
        }
    } 
}
