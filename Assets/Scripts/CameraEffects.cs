using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private Camera camera;
    [SerializeField]
    private Material _bloodLossMat;
    [SerializeField]
    private float maxEffect;
    private float maxHealth;

    private float effectVal;

    private HealthManager health;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HealthManager>();
        maxHealth = health.GetMaxHealth();

        
    }
    void Update()
    {
        effectVal = (-maxEffect / maxHealth)*health.health + maxEffect;
        print(string.Format("maxH: {0} maxEff: {1} effectVal: {2}", maxHealth, maxEffect, effectVal));
        _bloodLossMat.SetFloat("_LossAmnt", effectVal);

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination){
        Graphics.Blit(source,destination,_bloodLossMat);

    }
}
