using UnityEngine;

public class missileRing : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject missileRingPrefab;
    public int missileCount = 5;
    public float missileRadius = 5f;
    public float missileRingSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnMissileRing();
        }
    }

    void SpawnMissileRing()
    {
        //get screen pos for ring spawn location
        float randX = Random.Range(-10f, 10f);
        float randY = Random.Range(-5f, 5f);
        Vector3 center = new Vector3(randX, randY, 0f);
        //spawn the invisible center for the missile ring pivot
        GameObject ring = Instantiate(missileRingPrefab, center, Quaternion.identity);

        //set up missile ring maths
        float missileAngleStep = 360f / missileCount;

        //spawn the ring
        for (int i = 0; i < missileCount; i++)
        {
            float angle = missileAngleStep * i * Mathf.Deg2Rad;
            Vector3 localPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * missileRadius;

            //instantiate prefabs
            GameObject missile = Instantiate(missilePrefab, ring.transform.position, Quaternion.identity);
            missile.transform.SetParent(ring.transform);
            missile.transform.localPosition = localPos;
        }
    }
}
