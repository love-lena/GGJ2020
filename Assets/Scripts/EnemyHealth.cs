using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    static Dictionary<string, Vector2> lookup = new Dictionary<string, Vector2>() {
        {"default", new Vector2(2f, 6f)},
        {"slow", new Vector2(1.1f, 20f)}
    };

    public string enemyType = "default";
    public float suckRate = 0f;
    public float suckLeft = 0f;
    public bool gettingSucked = false;

    private EnemyStateController state;

    // Start is called before the first frame update
    void Start() 
    {
        gettingSucked = false;
        Vector2 suckInfo = lookup.ContainsKey(enemyType) ? lookup[enemyType] : lookup["default"];
        suckRate = suckInfo.x;
        suckLeft = suckInfo.y;
        state = gameObject.GetComponent<EnemyStateController>();
    }

    // Update is called once per frame
    void Update() { }

    public void StartSucking() {
        gettingSucked = true;
        state.SetState(EnemyStateController.EnemyState.gettingSucked);
        Debug.Log("Starting the succ");
    }

    public void StopSucking() {
        suckLeft = 0f;
        gettingSucked = false;
        state.SetState(EnemyStateController.EnemyState.dead);
    }

    public float Sucked(float delta) {
        float ammount = (suckLeft > suckRate) ? suckRate * delta : suckLeft;
        suckLeft -= ammount;
        if (suckLeft <= 0) { StopSucking(); }
        return ammount;
    }

    public bool Suckable() {
        return gettingSucked;
    }
}
