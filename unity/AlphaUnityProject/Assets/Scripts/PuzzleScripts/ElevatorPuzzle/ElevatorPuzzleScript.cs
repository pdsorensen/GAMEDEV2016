﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class ElevatorPuzzleScript : MonoBehaviour {

    public GameObject elevator, buttons;
    public float elevatorSpeed = 1.0f;
    public float timeBeforeStopping;
    public float bootTime = 1.0f;
    public float yOffset = 1.0f; 
    public Color MiMiColor;
    public Color B4Color;
    public KeyCode bootElevatorKey;
    public KeyCode moveElevatorKey;

    private GameObject B4, MiMi;
    private AudioSource m_source; 
    private List <GameObject> m_buttons = new List<GameObject>();
    private bool isActive, isBooting, isMoving, shouldMove, isBeingPlayed = false;
    private Animator anim;
    private Vector3 nextPosition;
    private List<GameObject> m_activeButtons = new List<GameObject>();

    private enum PlayerCodes
    {
        MiMi,
        B4
    }

    /**
     * 
     *    MiMi = ¤ , B4 = # 

            A | B | C_
           ____________
        1 |_0_|_1_|_2_|   
        2 |_3_|_4_|_5_| 
        3 |_6_|_7_|_8_|  
     * 
     * 
     */

    void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        nextPosition = transform.position + (Vector3.up * yOffset);
        anim = GetComponent<Animator>();
        m_source = GetComponentInChildren<AudioSource>(); 

        foreach (Transform t in buttons.transform)
            m_buttons.Add(t.gameObject);
    }

    void Update()
    {
        if (Input.GetKey(bootElevatorKey) && !isBooting)
            StartCoroutine(StartElevatorScene());

        if (Input.GetKey(moveElevatorKey) && !isMoving)
            MovePlatForm(); 

        if (!isMoving && !isBeingPlayed)
            StartCoroutine(BeginElevatorSequence());
    }

    void FixedUpdate()
    {
        if (shouldMove)
            MovePlatForm();
    }


    IEnumerator StartElevatorScene()
    {
        isBooting = true;
        m_source.Play(); 
        yield return new WaitForSeconds(1.0f);

        isActive = true; isBooting = false; 
    }

   
    void StartSequence(GameObject button, PlayerCodes player)
    {
        m_activeButtons.Add(button);
        AssignCube(player, button.GetComponent<ElevatorButtonScript>());
    }

    bool IsCubesActive()
    {
        foreach(GameObject gb in m_activeButtons)
            if (gb.GetComponent<ElevatorButtonScript>().IsActive())
                return true;

        return false; 
    }

    

    void MovePlatForm()
    {
        isMoving = true;
        shouldMove = true; 
        float distance = Vector3.Distance(transform.position, nextPosition);
        if (distance > 0.2f)
        {
            Vector3 TranslateVec = Vector3.up * elevatorSpeed * Time.deltaTime;
            Debug.Log("newPosition: " + TranslateVec + ", distance: " + distance);
            transform.Translate(TranslateVec);
        }
        else
        {
            Debug.Log("distance: " + distance + " > than 0.2f");
            nextPosition = transform.position + (Vector3.up * yOffset);
            shouldMove = false;
            isMoving = false; 
        }
    }


    void AssignCube(PlayerCodes playerCode, ElevatorButtonScript button)
    {
        if (playerCode == PlayerCodes.B4)
            button.ActivateButton(playerCode.ToString(), B4Color);
        else
            button.ActivateButton(playerCode.ToString(), MiMiColor);
    }

    IEnumerator BeginElevatorSequence()
    {

        isBeingPlayed = true;

        // Sequence 1: 
        StartSequence(m_buttons[2], PlayerCodes.B4);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);


        // Sequence 2: 
        StartSequence(m_buttons[1], PlayerCodes.MiMi);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 3: 
        StartSequence(m_buttons[0], PlayerCodes.B4);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 4: 
        StartSequence(m_buttons[7], PlayerCodes.MiMi);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 5: 
        StartSequence(m_buttons[8], PlayerCodes.B4);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 6: 
        StartSequence(m_buttons[7], PlayerCodes.B4);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 7: 
        StartSequence(m_buttons[2], PlayerCodes.MiMi);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 8: 
        StartSequence(m_buttons[6], PlayerCodes.MiMi);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 9: 
        StartSequence(m_buttons[2], PlayerCodes.B4);
        StartSequence(m_buttons[8], PlayerCodes.MiMi);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 10: 
        StartSequence(m_buttons[7], PlayerCodes.B4);
        StartSequence(m_buttons[2], PlayerCodes.MiMi);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        // Sequence 11: 
        StartSequence(m_buttons[0], PlayerCodes.B4);
        StartSequence(m_buttons[6], PlayerCodes.MiMi);
        while (IsCubesActive())
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        MovePlatForm();
        while (isMoving)
            yield return new WaitForSeconds(1.0f);

        StartCoroutine(TriggerEndplatform());

        yield return null;
    }

    IEnumerator TriggerEndplatform()
    {

        // TODO: Implement..
        Debug.Log("Need to implement the ending"); 
        yield return null; 
    }
}
