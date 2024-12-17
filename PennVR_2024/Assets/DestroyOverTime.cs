using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public int howLongToLive = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSomeTime());
    }

    IEnumerator WaitForSomeTime()
    {
        yield return new WaitForSeconds(howLongToLive);
        DestroyImmediate(this.gameObject);
    }
}
