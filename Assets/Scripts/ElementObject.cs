using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementObject : MonoBehaviour
{
    public Element element;

    
    public string fullName;

    public string shortName;

    public int atomNumber;

    public void Start()
    {
        element.SetFullName(fullName)
            .SetShortName(shortName)
            .SetAtomNumber(atomNumber);

        // Constructor
        //element = new Element()
        //    .SetFullName(fullName)
        //    .SetShortName(shortName)
        //    .SetAtomNumber(atomNumber);
    }
}
