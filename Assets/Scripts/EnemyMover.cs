using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    private Vector3 normalizeDirection;
    private float speed = 5f;

    void Update()
    {
        move();
    }
    private void move(){
        //simple movement interpolation for now
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    public void SetMovement(Vector2 newNormalizeDirection, float newSpeed){
        normalizeDirection = new Vector3(newNormalizeDirection.x, newNormalizeDirection.y, 0);
        speed = newSpeed;
    }
}
