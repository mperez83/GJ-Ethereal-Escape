using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float xForce = Random.Range(-4f, 4f);
        float yForce = Random.Range(5f, 10f);
        float zForce = Random.Range(-4f, 4f);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        rb.AddForce(force, ForceMode.Impulse);

        rb.AddTorque(force, ForceMode.Impulse);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MatchHandler.instance.CollectManaGoo(1);
            Destroy(gameObject);
        }
    }
}