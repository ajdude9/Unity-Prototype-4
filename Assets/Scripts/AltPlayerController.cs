using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayerController : MonoBehaviour
{
    private float speed = 10;
    Rigidbody playerRb;
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        playerMovementHandler();
    }

    void playerMovementHandler()
    {
        if(Input.GetKey(KeyCode.W)){
            playerRb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if(Input.GetKeyUp(KeyCode.W)){
            playerRb.AddForce(Vector3.forward / 2, ForceMode.VelocityChange);
        }
        if(Input.GetKey(KeyCode.A)){
            playerRb.AddForce(Vector3.left * speed * Time.deltaTime,ForceMode.VelocityChange);
        }
        if(Input.GetKeyUp(KeyCode.A)){
            playerRb.AddForce(Vector3.left / 2, ForceMode.VelocityChange);
        }
        if(Input.GetKey(KeyCode.S)){
            playerRb.AddForce(Vector3.back * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if(Input.GetKeyUp(KeyCode.S)){
            playerRb.AddForce(Vector3.back / 2, ForceMode.VelocityChange);
        }
        if(Input.GetKey(KeyCode.D)){
            playerRb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if(Input.GetKeyUp(KeyCode.D)){
            playerRb.AddForce(Vector3.right / 2, ForceMode.VelocityChange);
        }

    }
}
