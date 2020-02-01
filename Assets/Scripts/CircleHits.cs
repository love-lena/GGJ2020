using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleHits : MonoBehaviour
{
    private Collider2D coll;
    [SerializeField]
    public float checkRadius;

    public Collider2D[] circleHits = new Collider2D[20];
    public int numHits;
    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        
    }

    public Collider2D[] checkSurroundings()
    {
        coll.enabled = false;
        numHits = Physics2D.OverlapCircleNonAlloc(transform.position, checkRadius, circleHits);
        coll.enabled = true;
        return circleHits;
    }
    public int getNumHits()
    {
        return numHits;
    }

    //utility for other scripts to get the tags that are in the
    //hits array they have access to
    public List<String> getObjectTypes(Collider2D[] hits, int hitNum)
    {
        List<String> res = new List<string>();

        for(int i = 0; i < hitNum; i++)
        {
            if (!res.Contains(hits[i].tag))
            {
                res.Add(hits[i].tag);
            }
        }
        return res;
    }
}
