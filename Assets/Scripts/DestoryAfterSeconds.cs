using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterSeconds : MonoBehaviour
{
    public float seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("destroy");
        
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(seconds);
        GameObject.Destroy(gameObject);
    }
}
