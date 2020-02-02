using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 playerPosition;
    private Vector2 offset;
    private float angle;
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10;
    public bool takingInput = true;
    private string gameState;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        gameState = gameManager.GetComponent<StateChangeManager>().GetState();
        if (takingInput && (gameState == "playing"))
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            mousePosition = Input.mousePosition;
            mousePosition.z = -10;
            playerPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePosition.x -= playerPosition.x;
            mousePosition.y -= playerPosition.y;
            angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;
            transform.position += (speed * moveDirection * Time.deltaTime);
        }
    }
}
