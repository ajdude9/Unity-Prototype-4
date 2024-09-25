using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 2.5f;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovementController();
        DestroyOOB();
    }

    void EnemyMovementController()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * (speed * 250) * Time.deltaTime);        
    }

    bool CheckOOB()
    {
        if(transform.position.y < -10)
        {
            return true;
        }        
        return false;
    }

    void DestroyOOB()
    {
        if(CheckOOB())
        {
            Debug.Log("Enemy object destroyed.");
            Destroy(gameObject);
        }
    }
}
