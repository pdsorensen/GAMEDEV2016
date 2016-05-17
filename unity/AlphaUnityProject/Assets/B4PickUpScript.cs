﻿using UnityEngine;
using System.Collections;

public class B4PickUpScript : MonoBehaviour {

    public AudioClip pickUpSound;
    public float boostAmount;

    private AudioSource sfxSource;
    private GameObject B4, MiMi;
    private PlayerController b4Con, MiMiCon;

	void Start () {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.clip = pickUpSound;

        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

        b4Con = B4.GetComponent<PlayerController>();
        MiMiCon = MiMi.GetComponent<PlayerController>();
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "RedPickUp")
		{
            sfxSource.Play();
            if (!b4Con.isSpeedBoosted() && !MiMiCon.isSpeedBoosted())
            {
                b4Con.setSpeedBoosted(true);
                StartCoroutine(speedBoost());
            }
			other.gameObject.SetActive(false);
		}
	}

    IEnumerator speedBoost()
    {

        var oldSpeedB4 = b4Con.getSpeed();
        var oldSpeedMiMi = MiMiCon.getSpeed();
        b4Con.setSpeed(oldSpeedB4 + boostAmount);
        MiMiCon.setSpeed(oldSpeedMiMi + boostAmount);
        yield return new WaitForSeconds(3.0f);
        b4Con.setSpeed(oldSpeedB4);
        MiMiCon.setSpeed(oldSpeedMiMi);
        b4Con.setSpeedBoosted(false);
    }
}