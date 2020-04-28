using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerWasIn2 : MonoBehaviour
{
    public static bool wasHit;

    private void Start()
    {
        wasHit = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            StartCoroutine("WasHit");
        }
    }

    private IEnumerator WasHit()
    {
        wasHit = true;
        yield return new WaitForSeconds(2);
        wasHit = false;
    }
}
