﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // lav en reference til transformen
    public Transform tf;

    // lav en reference til vores spiller's kamera's transform
    public Transform playerCamTf;

    public Rigidbody rb;

    public float speed;

    // FixedUpdate bruges til fysik-baserede handlinger, da man kalder denne update på en specifik frame.
    private void FixedUpdate()
    {
        // Få x og y-akserne og multiplicer disse med vores hastighedsvariabel. Set y-aksen til 0, da vi ikke skal opad
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0.0f, Input.GetAxis("Vertical") * speed);

        // Få akserne til at følge der hvor kameraet kigger hen
        movement = playerCamTf.TransformDirection(movement);

        // Sikre y-aksen er i nul, ved at omdanne tilbage til globalt akseniveau. På denne måde kan man ikke gå opad på y-aksen
        movement = new Vector3(movement.x, 0.0f, movement.z);
        
        // Få spilleren til at bevæge sig
        rb.velocity = movement;
    }

    private void Update()
    {
        // Hvis spilleren trykker på R tasten
        if (Input.GetKey(KeyCode.R))
        {
            // Genindlæs den aktive scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
