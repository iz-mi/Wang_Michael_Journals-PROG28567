using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    private Vector3 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        pickNewLocation();
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void pickNewLocation()
    {
        float randX = Random.Range(-5f, 5f);
        float randY = Random.Range(-5f, 5f);
        targetLocation = new Vector3(randX, randY, 0f);
    }

    public void AsteroidMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, moveSpeed * Time.deltaTime);
    }
}
