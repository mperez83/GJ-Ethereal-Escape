using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot : MonoBehaviour
{
    public int maxHealth;
    int health;

    Camera sceneCamera;
    public Canvas healthBarCanvas;
    public Image healthBarImage;

    public GameObject manaGooPrefab;



    void Start()
    {
        sceneCamera = Camera.main;
        health = maxHealth;
    }

    void Update()
    {
        healthBarCanvas.transform.LookAt(sceneCamera.transform.position + sceneCamera.transform.rotation * Vector3.back, sceneCamera.transform.rotation * Vector3.up);
    }



    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        healthBarImage.fillAmount = 1 - ((maxHealth - health) / (float)maxHealth);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            TakeDamage((int)Mathf.Ceil(other.relativeVelocity.magnitude));
            other.collider.GetComponent<Rigidbody>().velocity = other.relativeVelocity * -1;
        }
    }
}