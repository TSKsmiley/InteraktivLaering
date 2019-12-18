using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dette script har til formål at flytte kemikalier tilbage til spilleren, hvis han kaster den udover mappet (dette forhindrer performance problemer)
public class MoveElements : MonoBehaviour
{
    // Lav en reference til et punkt over gulvet
    public Transform moveElPoint;
    private void OnTriggerEnter(Collider col)
    {
        // Flyt objektet til punktet, hvis den kolliderer med den trigger som sidder under gulvet
        col.gameObject.transform.position = moveElPoint.position;
    }
}
