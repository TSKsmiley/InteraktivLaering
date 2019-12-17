using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    // Definer en offentlig tilgængelig variabel (i unity) kaldet chemicalName, som bliver brugt på displayet af maskinen
    public string chemicalName;

    // Lav en liste af vores element klasse, som definerer de nødvendige kemikalier (komponenter)
    public List<Element> reqComponents = new List<Element>();
}
