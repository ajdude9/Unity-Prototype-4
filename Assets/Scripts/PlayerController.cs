using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    private float speed = 2.5f;
    private GameObject focalPoint;
    private bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        playerMovementController();
        powerupIndicator.transform.position = new UnityEngine.Vector3(playerRb.transform.position.x, playerRb.transform.position.y + 1.5f, playerRb.transform.position.z);
        //powerupIndicator.transform.rotation = playerRb.transform.rotation;
        
    }

    void playerMovementController()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        float horizontalInput = Input.GetAxis("RightLeft");
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput); 
    }

    void OnTriggerEnter(Collider other)//Upon touching a boxCollider
    {

        if(other.CompareTag("Powerup"))//If it's an object with the tag 'Powerup'
        {
            hasPowerup = true;//Set bool hasPowerup to true
            Destroy(other.gameObject);//Destroy the powerup object
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());//Run the function 'PowerupCountdownRoutine' at the same time as this one
        }//End of If statement        
    }//End of onTriggerEnter
    void OnCollisionEnter(Collision collision)//Upon colliding with something
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)//If it's an enemy and the powerup is active
        {
            Debug.Log("Collided with " + collision.gameObject.name + " with hasPowerup set to " + hasPowerup);//Log the player's collision
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            UnityEngine.Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    //!!! Important!
    IEnumerator PowerupCountdownRoutine() {//The routine for counting down how long the player will have tge powerup for.
        yield return new WaitForSeconds(7);//Wait for seven seconds when this function runs
        powerupIndicator.gameObject.SetActive(false);
        hasPowerup = false;//Set the variable 'hasPowerup' to false - thereby disabling it after 7 seconds
    }
}
