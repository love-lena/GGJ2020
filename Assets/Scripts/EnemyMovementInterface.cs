using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyMovementInterface
{
    Vector2 getDir();
    float getSpeed();
    void setSpeedMultiplier(float mult);
    void setScared(bool scared);
    void setStationary(bool stationary);
}
