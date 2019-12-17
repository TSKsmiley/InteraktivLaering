using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class AtomMachine : MonoBehaviour
{
    public Transform output;

    public TextMeshPro objectiveText;
    public int currObjective = 0;

    public TextMeshPro healthText;
    int health = 3;

    // Lav en liste fra vores reference til Objective.cs scriptet
    public List<Objective> objectives = new List<Objective>();

    // Start is called before the first frame update
    void Start()
    {
        // Tag det nuværende objective's navn (ved hjælp af indexet currentobjective)
        objectiveText.text = objectives[currObjective].chemicalName;
    }

    private void OnTriggerEnter(Collider col)
    {
        // Hvis maskinen er løbet tør for liv
        if (health <= 0)
            return;

        // Få fat i element scriptet som sidder på det objekt som spilleren få til at kollidere med triggeren
        Element colEl = col.GetComponent<Element>();

        // Hvis komponentet (scriptet Element) findes
        if (colEl != null)
        {
            // Iterer gennem alle items i vores liste
            foreach (Element item in objectives[currObjective].reqComponents)
            {
                // Hvis den item property, matcher objektets shortname (f.eks. Na) som koliderer i maskinen
                if (item.shortName == colEl.shortName)
                {
                    // Fjern denne item fra listen
                    objectives[currObjective].reqComponents.Remove(item);
                    
                    // Destruér objektet
                    Destroy(col.gameObject);

                    // 
                    if (objectives[currObjective].reqComponents.Count < 1)
                    {
                        // Hvis spilleren løber tør for objectives
                        if (currObjective == objectives.Count - 1)
                        {
                            objectiveText.text = "You Win! Press R to restart";
                        }
                        else
                        {
                            // Læg en til indexet i listen for at fortsætte til næste objective
                            currObjective++;

                            // Vis det nye objective (eller kemiske stof man skal finde) på displayet på maskinen
                            objectiveText.text = objectives[currObjective].chemicalName;
                        }
                    }

                    return;
                }
            }

            // Smid stoffet ud af maskinen, eftersom det ikke matcher de stoffer der skal bruges
            col.gameObject.transform.position = output.position;

            // Træk en fra maskinens liv
            health--;
            // Reset teksten
            healthText.text = "";

            // Iterer gennem mængden af liv maskinen har
            for (int i = 0; i < health; i++)
            {
                // Tilføj et d
                healthText.text += "d";
            }

            // Hvis maskinen dør
            if (health <= 0)
            {
                // Ændrer displayet til nedenstående på maskinen
                objectiveText.text = "You Lost! Press R to restart";
            }

            return;
        }
    }
}
