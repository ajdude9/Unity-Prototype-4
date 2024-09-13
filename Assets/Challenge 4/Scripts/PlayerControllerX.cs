using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 800;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    private bool boostCD = true;

    public ParticleSystem smokeParticle;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        playerMovementController();

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }

    void playerMovementController()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime);
        float horizontalInput = Input.GetAxis("RightLeft");
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput * Time.deltaTime); 
        if(Input.GetKeyDown(KeyCode.Space) && boostCD)
        {
            //playerRb.velocity = Vector3.zero;//Set the player's velocity to zero.
            //playerRb.angularVelocity = Vector3.zero;//Also stop the player from spinning
            forwardInput = Input.GetAxis("Vertical");
            playerRb.AddForce(focalPoint.transform.forward * (speed * 5) * forwardInput * Time.deltaTime, ForceMode.Impulse);
            horizontalInput = Input.GetAxis("RightLeft");
            playerRb.AddForce(focalPoint.transform.right * (speed * 5) * horizontalInput * Time.deltaTime, ForceMode.Impulse); 
            boostCD = false;
            smokeParticle.Play();
            StartCoroutine(boostCooldown());
            this.GetComponent<Renderer>().material.color = new Color (35, 0, 0);
        }
    }

    IEnumerator boostCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        boostCD = true;
        this.GetComponent<Renderer>().material.color = new Color (0, 35, 0);
        StartCoroutine(colourReset());
    }
    IEnumerator colourReset()
    {
        yield return new WaitForSeconds(0.075f);
        this.GetComponent<Renderer>().material.color = new Color (255, 255, 255);
    }
}
