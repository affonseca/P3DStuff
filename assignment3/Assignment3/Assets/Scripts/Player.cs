﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string changeGunKeyName;
    public float pickupMargin;

    private int currentGunIndex;
    private List<GameObject> inventory;
    private List<GameObject> allGuns;


    private void addToInventory(GameObject obj)
    {
        if (!inventory.Contains(obj))
        {
            inventory.Add(obj);
            allGuns.Remove(obj);
            setCurrentGun(obj);
           
        }
    }

    private void setCurrentGun(GameObject gun)
    {
        (gun.GetComponent<Gun>()).picked = true;
        foreach (GameObject invGun in inventory.ToArray())
        {
            if (!invGun.Equals(gun))
            {
                invGun.SetActive(false);
            }
        }
        gun.transform.parent = transform; //set gun as child of player
        gun.transform.position = transform.position + new Vector3(20, 0, 0);
        gun.SetActive(true);
    }

    // Use this for initialization
    void Start() {

        allGuns = new List<GameObject>();
        inventory = new List<GameObject>();
        currentGunIndex = 0;
        allGuns.AddRange(GameObject.FindGameObjectsWithTag("gun"));
    }
	// Update is called once per frame
	void Update () {
        if (inventory.Count > 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                currentGunIndex = (currentGunIndex + 1) % inventory.Count;
                setCurrentGun(inventory[currentGunIndex]);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                currentGunIndex = (currentGunIndex == 0 ? (inventory.Count - 1) : 0);
                setCurrentGun(inventory[currentGunIndex]);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("fire!");
            (inventory[currentGunIndex].GetComponent<Gun>()).fire();
        }

        foreach (GameObject gun in allGuns.ToArray()){
            if( (gun.transform.position.x < transform.position.x + pickupMargin)&&
                (gun.transform.position.z < transform.position.z + pickupMargin))
            {
                addToInventory(gun);
            }
        }
    }
}