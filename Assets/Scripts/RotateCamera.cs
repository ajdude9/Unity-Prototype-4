using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    private float rotationSpeed = 50;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Turn");
        transform.Rotate(Vector3.down, horizontalInput * rotationSpeed * Time.deltaTime);

        transform.position = player.transform.position; // Move focal point with player
    }
}
