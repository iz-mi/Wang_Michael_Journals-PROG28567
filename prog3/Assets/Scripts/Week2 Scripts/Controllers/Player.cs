using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    //set initial player movespeed
    //public float moveSpeed = 10f;

    //set max player movespeed
    public float maxSpeed = 5f;
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;

    float acceleration;
    float deceleration;

    public float verticalMovement = 0f;
    public float horizontalMovement = 0f;

    void Update()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        PlayerMovement();
        //bombRing();
    }

    public void PlayerMovement()
    {
        //vertical movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalMovement += acceleration * Time.deltaTime;
            if (Mathf.Abs(verticalMovement) >= maxSpeed)
            {
                verticalMovement = maxSpeed;
            }
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement -= acceleration * Time.deltaTime;
            if (Mathf.Abs(verticalMovement) >= maxSpeed)
            {
                verticalMovement = -maxSpeed;
            }
        }

        //vertical decel
        else
        {
            if (verticalMovement > 0)
            {
                verticalMovement -= deceleration * Time.deltaTime;
                if (verticalMovement < 0)
                {
                    verticalMovement = 0;
                }
            }

            if (verticalMovement < 0)
            {
                verticalMovement += deceleration * Time.deltaTime;
                if (verticalMovement > 0)
                {
                    verticalMovement = 0;
                }
            }
        }

        //horizontal movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMovement -= acceleration * Time.deltaTime;
            if (Mathf.Abs(horizontalMovement) >= maxSpeed)
            {
                horizontalMovement = -maxSpeed;
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMovement += acceleration * Time.deltaTime;
            if (Mathf.Abs(horizontalMovement) >= maxSpeed)
            {
                horizontalMovement = maxSpeed;
            }
        }

        //horizontal decel
        else
        {
            if (horizontalMovement > 0)
            {
                horizontalMovement -= deceleration * Time.deltaTime;
                if (horizontalMovement < 0)
                {
                    horizontalMovement = 0;
                }
            }

            if (horizontalMovement < 0)
            {
                horizontalMovement += deceleration * Time.deltaTime;
                if (horizontalMovement > 0)
                {
                    horizontalMovement = 0;
                }
            }
        }

        Vector3 movementDirection = new Vector3(horizontalMovement, verticalMovement, 0f) * Time.deltaTime;
        transform.position += movementDirection;
    }

    /*
    public void bombRing()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float ringRadius = 3f;
            float ringDuration = 5f;

            Vector2 playerCenter = transform.position;

            Vector2 bombPos1 = playerCenter + new Vector2(0f, ringRadius);
            Vector2 bombPos2 = playerCenter + new Vector2(ringRadius, 0f);
            Vector2 bombPos3 = playerCenter + new Vector2(0f, -ringRadius);
            Vector2 bombPos4 = playerCenter + new Vector2(-ringRadius, 0f);

            GameObject bomb1 = Instantiate(bombPrefab, (Vector3)bombPos1, Quaternion.identity);
            GameObject bomb2 = Instantiate(bombPrefab, (Vector3)bombPos2, Quaternion.identity);
            GameObject bomb3 = Instantiate(bombPrefab, (Vector3)bombPos3, Quaternion.identity);
            GameObject bomb4 = Instantiate(bombPrefab, (Vector3)bombPos4, Quaternion.identity);
        
            Destroy(bomb1, ringDuration);
            Destroy(bomb2, ringDuration);
            Destroy(bomb3, ringDuration);
            Destroy(bomb4, ringDuration);
        }
    }
    */
}


