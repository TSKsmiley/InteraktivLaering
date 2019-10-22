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
        // Gør cursoren usynlig for spilleren
        Cursor.visible = false;
        // Centrér cursoren midt i skærmen, så man starter med at have musen i midten af skærmen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // FixedUpdate bruges til fysik-baserede handlinger, da man kalder denne update på en specifik frame.
    private void FixedUpdate()
    {
        // Få x og y-akserne og multiplicer disse med vores hastighedsvariabel. Set y-aksen til 0, da vi ikke skal opad
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0.0f, Input.GetAxis("Vertical") * speed);

        //Få akserne til at følge der hvor kameraet kigger hen
        movement = tf.TransformDirection(movement);

        // Sikre y-aksen er i nul, ved at omdanne tilbage til globalt akseniveau. På denne måde kan man ikke gå opad på y-aksen
        movement = new Vector3(movement.x, 0.0f, movement.z);
        
        // Få spilleren til at bevæge sig
        rb.velocity = movement;
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
