using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitSpeed = -60f;

    void Update()
    {
        enemyMovement();
    }

    public void enemyMovement()
    {
        transform.RotateAround(orbitCenter.position, Vector3.forward, orbitSpeed * Time.deltaTime);
    }
}
