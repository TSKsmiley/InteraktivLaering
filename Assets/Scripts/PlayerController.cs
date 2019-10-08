using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform tf;

    public Rigidbody rb;

    public float speed;

    public float mouseSensitity;

    // Start er kaldt inden den første frame update
    void Start()
    {
        
    }

    // FixedUpdate bruges til fysik-baserede handlinger, da man kalder denne update på en specifik frame.
    private void FixedUpdate()
    {
        // Tilføj en hastighed til kameraets rigidbody. Få heraf den horisontale samt vertikale akse for at skabe WASD movement. Udelad y-aksen, da vi ikke skal opad på disse keys. Gang til sidst alt dette med vores float speed, som kan sættes i unity editoren
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0.0f, Input.GetAxis("Vertical") * speed);
    }

    // Update kaldes en gang per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //tf.Rotate(MouseX * mouseSensitity, mouseY * mouseSensitity, 0);

        // Rotér transformen i forhold til museposition. Set z-aksen til 0, da vi skal ordne den fra linje 40 og til linje 45
        tf.Rotate(new Vector3(-mouseY * mouseSensitity, MouseX * mouseSensitity, 0));

        // Med inspiration fra Michaels fysikspil ;)
        float z = tf.eulerAngles.z; // Skaf vinklen z

        tf.Rotate(0,0, -z); // Rotér z i minus

        // Flyt z-aksen tilbage til 0
        rb.AddForce(0, 100 * Time.deltaTime, 0);
    }
}
