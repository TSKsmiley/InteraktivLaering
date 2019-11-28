using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    // lav en string som kan indeholde grundstoffets fulde navn
    public string fullName;

    // lav en string som kan indeholde grundstoffets forkortelse
    public string shortName; 

    // lav en string som kan indeholde grundstoffets atomnummer
    public int atomNumber;

    // Alt nedenstående er for at gøre det nemt når man skal ændre værdierne, for at få koden til at se renere ud og mere overskuelig
    public Element SetFullName(string _name)
    {
        fullName = _name;
        return this;
    }

    public Element SetShortName(string _sName)
    {
        shortName = _sName;
        return this;
    }

    public Element SetAtomNumber(int _number)
    {
        atomNumber = _number;
        return this;
    }
}
