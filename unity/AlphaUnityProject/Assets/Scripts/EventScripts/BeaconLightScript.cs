﻿using UnityEngine;
using System.Collections;

public class BeaconLightScript : MonoBehaviour {
    public float delayTransistion = 4.0f; 
    public GameObject switch1, switch2;

    private HexagonSwitchScript switchScript1, switchScript2;
    private bool isScenePlaying, isSceneFinished, isActive = false;
	
	void Start () {
        switchScript1 = switch1.GetComponent<HexagonSwitchScript>();
        switchScript2 = switch2.GetComponent<HexagonSwitchScript>();
    }

    void Update()
    {
        if (switchScript1.getStatus() && switchScript1.getStatus() && !isScenePlaying && !isSceneFinished)
        {
            PlayActivatitonScene();
        }

        if (Input.GetKey(KeyCode.Y))
        {
            PlayActivatitonScene();
        }

        if(isSceneFinished && !isActive)
        {
            Debug.Log("GameObject is active and scene is finished");
            isActive = true;

            Destroy(switchScript1);
            Destroy(switchScript2);
            Destroy(gameObject.GetComponent<BeaconLightScript>());
        }
    }

    void PlayActivatitonScene() {
        isScenePlaying = true; 
        
        // Play animations 

        // Trigger spherical waves
        StartCoroutine(SendSphericalWaves());
        
        // Play sounds
    }

    IEnumerator SendSphericalWaves()
    {
        Renderer rend = GetComponent<Renderer>();
        Material sphereMaterial = rend.materials[3];
        Color startMColor = sphereMaterial.GetColor("_Color");
        Color startEColor = sphereMaterial.GetColor("_EmissionColor");

        for (float f = startMColor.a; f <= 1; f += 0.005f)
        {
            startMColor.a = f;
            startEColor = Color.HSVToRGB(0, 0, f/6); 
            sphereMaterial.SetColor("_Color", startMColor);
            sphereMaterial.SetColor("_EmissionColor", startEColor);
            yield return null;
        }

        sphereMaterial.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(delayTransistion);

        ActivateRobotHelpers();

        isSceneFinished = true; 
    }

    public void ActivateRobotHelpers()
    {
        //TODO: Implement this
        /**
        for each robot in robot
            trigger awake anim
            trigger awake sounds 
            move robot to destination
            if robot at destination 
                emit change to scripts
                trigger end state
            return
        end for loop
        */
    } 

    public bool GetStatus()
    {
        return isActive; 
    }
}
