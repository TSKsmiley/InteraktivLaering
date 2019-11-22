using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Transform tf;
    public Transform tfPlayer;
  
    public float mouseSensitity;

    // Start er kaldt inden den første frame update
    void Start()
    {
        // Gør cursoren usynlig for spilleren
        Cursor.visible = false;
        // Centrér cursoren midt i skærmen, så man starter med at have musen i midten af skærmen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update kaldes en gang per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotér transformen i forhold til museposition. Set z-aksen til 0, da vi skal ordne den fra linje 40 og til linje 45
        Vector3 rotation = new Vector3(-mouseY * mouseSensitity, mouseX * mouseSensitity, 0);
        tf.Rotate(rotation);

        // Med inspiration fra Michaels fysikspil
        float z = tf.eulerAngles.z; // Skaf vinklen z

        tf.Rotate(0, 0, -z); // Rotér z i minus
    }
}
