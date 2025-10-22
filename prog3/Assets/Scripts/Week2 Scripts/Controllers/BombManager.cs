using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject bombPrefab;
    float ringRadius = 3f;
    float ringDuration = 5f;

    public int numberOfBombs = 5;


    public Transform bombRingCenter;
    public float ringRotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            spawnBombRing();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            despawnBomb();
        }

        //rotate the bomb ring
        if (bombRingCenter.childCount > 0)
        {
            bombRingCenter.Rotate(0f, 0f, ringRotationSpeed * Time.deltaTime);
        }
    }

    void despawnBomb()
    {
        if (bombRingCenter.childCount > 0)
        {
            Destroy(bombRingCenter.GetChild(0).gameObject);
        }
    }

    void spawnBombRing()
    {
        for (int i=0; i < numberOfBombs; i++)
        {
            //autofill bomb placements around the ring
            float bombAngle = (360f / numberOfBombs) * i;
            float xPos = transform.position.x + Mathf.Cos(bombAngle * Mathf.Deg2Rad) * ringRadius;
            float yPos = transform.position.y + Mathf.Sin(bombAngle * Mathf.Deg2Rad) * ringRadius;
            
            Vector2 spawnPos = new Vector2(xPos, yPos);
            GameObject bomb = Instantiate(bombPrefab, spawnPos, Quaternion.identity);
            
            //SET THE BOMB AS A CHILD OR NOTHING WORKS
            bomb.transform.SetParent(bombRingCenter);
            
            Destroy(bomb, ringDuration);
        }
    }
}
