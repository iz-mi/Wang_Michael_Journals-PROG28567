using UnityEngine;

public class missileRingPivot : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float ringLifetime = 2f;
    public float movespeed = 2f;

    public GameObject playerPrefab;
    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = ringLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the pivot point
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        //follow player prefab
        Vector3 direction = (playerPrefab.transform.position - transform.position).normalized;
        transform.position += direction * movespeed * Time.deltaTime;

        //destroy missile ring after set duration
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
