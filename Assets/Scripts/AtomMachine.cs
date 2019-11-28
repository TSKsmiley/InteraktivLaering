using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomMachine : MonoBehaviour
{
    public Transform output;
    public GameObject redCube;

    // Lav en liste fra vores reference til Elements.cs scriptet
    public List<Element> elements = new List<Element>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        // Få fat i element scriptet som sidder på det objekt som spilleren få til at kollidere med triggeren
        Element colEl = col.GetComponent<Element>();

        // Hvis komponentet (scriptet Element) findes
        if (colEl != null)
        {
            // Tilføj grundstof elementet "Element" til listen 
            elements.Add(colEl);

            // Destruér objektet
            Destroy(col.gameObject);
        }

        
    }
}
