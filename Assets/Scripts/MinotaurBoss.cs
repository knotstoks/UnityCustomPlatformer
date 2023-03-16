using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurBoss : MonoBehaviour
{
    public float movementSpeed = 2f;
    public GameObject playerRef;

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Chase if in range
        if (Vector2.Distance(gameObject.transform.position, playerRef.transform.position) <= 5f)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerRef.transform.position, movementSpeed * Time.deltaTime);
        }
        
        // else Idle
    }

}
