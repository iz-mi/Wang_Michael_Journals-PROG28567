using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public Vector2 bombOffset;

    public float bombTrailSpacing = 5f;
    public int numberOfTrailBombs = 5;

    // Update is called once per frame
    void Update()
    {
        //check for B input, then execute SpawnBombAtOffset
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(bombOffset);
        }

        //check for T input, then execute SpawnBombTrail
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs)
        }
    }

    //task A
    public void SpawnBombAtOffset(Vector3 inOffset)
    {
        //set offset relative to player's location
        Vector3 bombOffset = transform.position + inOffset;

        //instantiate prefab
        Instantiate(bombPrefab, bombOffset, Quaternion.identity);
    }

    //task B
    public void SpawnBombTrail(float inBombSPacing, int inNumberOfBombs)
    { 
        for (int i = 1; i <= inNumberOfBombs) ;
        {
            Vector3 offset = new Vector3();
            Vector3 bombSpawnPos = transform.position + inOffset
        }
    }

}
