using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleHits))]
public class VectorEnemyMovement : MonoBehaviour, EnemyMovementInterface
{

    private CircleHits circleHits;
    [SerializeField]
    private float enemyAvoidanceWeight = 3f;
    [SerializeField]
    private float wallAvoidanceWeight = 3f;
    [SerializeField]
    private float minSpacingAvoidanceWeight = 0.1f;
    [SerializeField]
    private float fearWeight = 6f;
    [SerializeField]
    private float chaseWeight = 1f;

    [SerializeField]
    private float fearSpeed = 3f;

    [SerializeField]
    private float chaseSpeed = 2f;
    private bool amScared = false;

    private GameObject player;
    private bool stationary;

    private float scaredMult;
    private float speedMult;
    // Start is called before the first frame update
    void Start()
    {
        scaredMult = chaseWeight;
        speedMult = 1f;
        stationary = false;
        player = GameObject.FindGameObjectWithTag("Player");
        circleHits = GetComponent<CircleHits>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 getDir()
    {
        return getMoveVector(circleHits.checkSurroundings(), circleHits.getNumHits());
    }

    public float getSpeed()
    {
        if (!stationary)
        {
            return speedMult * (amScared ? fearSpeed : chaseSpeed);
        }
        return 0f;
    }
    private Vector2 getMoveVector(Collider2D[] hits, int hitNum)
    {
        Vector2 res = new Vector2();
        
        for(int i = 0; i < hitNum; i ++)
        {
            
            if (hits[i].tag == "Enemy" && !(hits[i].gameObject.name == gameObject.name) && 
                !hits[i].gameObject.GetComponent<EnemyHealth>().gettingSucked) 
            {
                res += getAvoidanceVector(hits[i].transform.position, hitNum, enemyAvoidanceWeight);
            }else if(hits[i].tag == "Wall"){
                res += getAvoidanceVector(hits[i].transform.position, hitNum, wallAvoidanceWeight);
            }
        }

        res += getPlayerVector();
        Debug.DrawLine(transform.position,
             transform.position + new Vector3(res.x, res.y, transform.position.z),
             Color.green,
             0.5f);
        return res;

    }

    public void setScared(bool scared)
    {
        amScared = scared;
        scaredMult = scared ? -fearWeight : chaseWeight;
    }
    public void setStationary(bool newStationary)
    {
        stationary = newStationary;

    }
    public void setSpeedMultiplier(float mult)
    {
        
    }
    
    private Vector2 getPlayerVector()
    {
        return scaredMult * (((Vector2) player.transform.position - (Vector2) transform.position)).normalized;
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
