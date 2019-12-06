using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseMovement : MonoBehaviour
{
    public Transform tf;
    public Transform tfPlayer;
  
    [Range(0.0f, 10.0f)]public float mouseSensitity;

    // Grab variabler
    GameObject grabbedObject;
    float grabbedObjectSize;
    public TextMeshProUGUI pickupText;
    [Range(0.0f, 10.0f)] public int pickupRange;
    float pickuptimer;


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
        // Find akserne nødvendige for at skabe musebevægelse og gem dem i variabler
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotér transformen i forhold til museposition. Set z-aksen til 0, da vi skal ordne den fra linje 40 og til linje 45
        Vector3 rotation = new Vector3(-mouseY * mouseSensitity, mouseX * mouseSensitity, 0);
        tf.Rotate(rotation);

        // Med inspiration fra Michaels fysikspil
        float z = tf.eulerAngles.z; // Skaf vinklen z

        tf.Rotate(0, 0, -z); // Rotér z i minus

        #region GrabObject
        //Debug.Log(GetMouseHoverObject(pickupRange));

        // Lav en timer
        pickuptimer += Time.deltaTime;
        // Hvis spilleren trykker på E har man 0.5 sekund til at slippe E ellers droppes objektet automatisk (for at undgå bugs)
        if (Input.GetKey(KeyCode.E) && pickuptimer > 0.5f)
        {
            // Reset timeren til 0 sekunder
            pickuptimer = 0.0f;

            // Hvis spilleren ikke allerede har et objekt i hånden i forvejen
            if (grabbedObject == null)
                // Forsøg da at køre metoden, som gør at spilleren har mulighed for at samle objektet op indenfor en given rækkevidde (pickupRange)
                TryGrabObject(GetMouseHoverObject(pickupRange));
            else
                // Hvis grabbedObjekt er sat til noget betyder dette at spilleren har noget i hånden. Trykker han E igen vil han droppe objektet
                DropObject();
        }

        // Hvis spilleren har et objekt i sin hånd
        if (grabbedObject != null)
        {
            // Sæt objektets position foran kameraet
            Vector3 newPos = gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize;
            grabbedObject.transform.position = newPos;
        }
        // Hvis spilleren har mulighed for at samle et objekt op
        if (GetMouseHoverObject(pickupRange) != null)
        {
            // Hvis spilleren kan tage fat i objektet
            if (CanGrab(GetMouseHoverObject(pickupRange)))
            {
                // Enable teksten som siger: "Press E to pick up"
                pickupText.enabled = true;
            }
        }
        else
        {
            // hvis spilleren er ude af range og ikke kigger på objektet vil teksten ikke vises
            pickupText.enabled = false;
        }
        #endregion GrabObject
    }

    private void FixedUpdate()
    {
        
    }

    // Kilde: https://www.youtube.com/watch?v=jOOdJZS987Y
    GameObject GetMouseHoverObject(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit raycastHit;
        Vector3 target = position + Camera.main.transform.forward * range;

        // Hvis spilleren kigger på objektet
        if (Physics.Linecast(position, target, out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
        return null;
    }

    void TryGrabObject(GameObject grabObject)
    {
        // Hvis den midlertidige variabel er ingenting samt at man ikke er i stand til at tage fat i objektet
        if (grabObject == null || !CanGrab(grabObject))
            // Fortsæt
            return;

        // Sæt vores globale grabbedObjekt til den tempvariabel kaldet grabObjekt
        grabbedObject = grabObject;

        // Slå tyngdekraften fra mens objektet er i hånden på spilleren
        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        // Sikrer at objektet ikke roterer mens man har det i hånden
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

        // Sæt objektet foran spilleren, lav også et offset fra spillerens position på 2.2f (Dette sikrer at man ikke kolliderer med playermodellen og dermed skaber bugs)
        grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude + 2.2f;
    }

    bool CanGrab(GameObject candidate)
    {
        // Det som ikke har en rigidbody kan man ikke samle op
        return candidate.GetComponent<Rigidbody>() != null;
    }

    void DropObject()
    {
        // Hvis spilleren ikke har fat i et objekt
        if (grabbedObject == null)
            // Fortsæt
            return;

        // Hvis rigidbodyen på det objekt man har fat i eksisterer
        if (grabbedObject.GetComponent<Rigidbody>() != null)
            // Lav en direkte ændring i positionen på rigidbodyen, dette gør at objektet flyver direkte hen til spilleren
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Når objektet igen er ude af hænderne på spilleren, tilføj da tyngdekraften
        grabbedObject.GetComponent<Rigidbody>().useGravity = true;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

        // Sæt variablen til ingenting, da man ikke længere har fat i et objekt
        grabbedObject = null;
    }
}
