using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleHits))]
public class VectorEnemyMovement : MonoBehaviour, EnemyMovementInterface
{

    private CircleHits circleHits;
    [SerializeField]
    private float enemyAvoidanceWeight;
    [SerializeField]
    private float wallAvoidanceWeight;
    [SerializeField]
    private float minSpacingAvoidanceWeight;
    // Start is called before the first frame update
    void Start()
    {
        circleHits = GetComponent<CircleHits>();
    }

    // Update is called once per frame
    void Update()
    {
        getMoveVector(circleHits.checkSurroundings(), circleHits.getNumHits());
        
    }

    public Vector2 getDir()
    {
        return Vector2.zero;
    }

    public float getSpeed()
    {
        return 0;
    }
    private Vector2 getMoveVector(Collider2D[] hits, int hitNum)
    {
        Vector2 res = new Vector2();
        
        //bool trailFound = false;
        for(int i = 0; i < hitNum; i ++)
        {
            if (hits[i].tag == "Enemy") 
            {
                res += getAvoidanceVector(hits[i].transform.position, hitNum, enemyAvoidanceWeight);
            }else if(hits[i].tag == "Wall"){

                res += getAvoidanceVector(hits[i].transform.position, hitNum, wallAvoidanceWeight);
            }
        }

        Debug.DrawLine(transform.position,
             transform.position + new Vector3(res.x, res.y, transform.position.z),
             Color.green,
             2);
        //res += getPlayerVector();
        return res;

    }

    public void setScared(bool scared)
    {

    }

    private Vector2 getAvoidanceVector(Vector2 avoidPos, int numAvoiding, float avoidanceWeight)
    {
        float distance = Vector2.Distance(avoidPos, transform.position);
        //dist ratio gets closer to 1 if closer to flit obj
        float distRatio = 1 - (distance / circleHits.checkRadius); 
        //make sure the ration isn't so small the flit gets into deadlock
        distRatio = distRatio < minSpacingAvoidanceWeight? minSpacingAvoidanceWeight : distRatio;
        // float distWeight = distRatio - (distRatio * (1 - pSpaceDistWeight));
        Vector2 hitDif = (avoidPos - (Vector2)transform.position).normalized;
        Vector2 addVec = (new Vector2(-hitDif.x, -hitDif.y));
        
        Vector2 res = (((addVec * avoidanceWeight) / numAvoiding) * distRatio);
         
        //print(String.Format("name: {0} distance: {1} distRatio: {2} hitDif: {3} addVec: {4} \nnumAvoid: {5} weight: {6} res: {7} ",
         //   gameObject.name, distance, distRatio, hitDif, addVec,numAvoiding, avoidanceWeight, res));
        return res; 
    }
}
