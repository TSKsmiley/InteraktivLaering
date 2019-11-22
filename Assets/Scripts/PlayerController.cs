using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform tf;

    public Transform playerCamTf;

    public Rigidbody rb;

    public float speed;

    // Grab variabler
    GameObject grabbedObject;
    float grabbedObjectSize;

    private void Update()
    {
        //Debug.Log(GetMouseHoverObject(5));

        if (Input.GetKey(KeyCode.E))
        {
            if (grabbedObject == null)
                TryGrabObject(GetMouseHoverObject(5));
            else
                DropObject();
        }

        if (grabbedObject != null)
        {
            // Nye position
            Vector3 newPos = gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize;
            grabbedObject.transform.position = newPos;
        }
    }

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

    // Kilde: https://www.youtube.com/watch?v=jOOdJZS987Y
    GameObject GetMouseHoverObject(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit raycastHit;
        Vector3 target = position + Camera.main.transform.forward * range;
        if (Physics.Linecast(position, target, out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
        return null;
    }

    void TryGrabObject(GameObject grabObject)
    {
        if (grabObject == null || !CanGrab(grabObject))
            return;

        grabbedObject = grabObject;
        grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude;
    }

    bool CanGrab(GameObject candidate)
    {
        // Det som ikke har en rigidbody kan man ikke samle op
        return candidate.GetComponent<Rigidbody>() != null;
    }

    void DropObject()
    {
        if (grabbedObject == null)
            return;

        if (grabbedObject.GetComponent<Rigidbody>() != null)
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabbedObject = null;
    }
}
