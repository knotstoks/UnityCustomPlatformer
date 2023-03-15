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
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerRef.transform.position, movementSpeed * Time.deltaTime);
    }

}
