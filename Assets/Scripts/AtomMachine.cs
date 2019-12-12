using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class AtomMachine : MonoBehaviour
{
    public Transform output;

    public TextMeshPro healthText;
    int health = 3;

    // Lav en liste fra vores reference til Objective.cs scriptet
    public List<Objective> objectives = new List<Objective>();

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
            foreach (Element item in objectives[0].reqComponents)
            {
                if (item.shortName == colEl.shortName)
                {
                    Debug.Log("true lulw");
                    objectives[0].reqComponents.Remove(item);
                    
                    // Destruér objektet
                    Destroy(col.gameObject);
                    return;
                }
            }

            // Smid stoffet ud af maskinen, eftersom det ikke matcher de stoffer der skal bruges
            col.gameObject.transform.position = output.position;

            health--;
            healthText.text = "";
            for (int i = 0; i < health; i++)
            {
                healthText.text += "d";
            }

            return;
        }
    }

    private void NewObjective(string _fullName, List<string> _objective)
    {

    }
}
