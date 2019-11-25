using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerOne : MonoBehaviour
{
    public PlayerTwo playerTwo;

    Vector3 direction;
    public float acceleration;
    public float maxSpeed;

    public float dashPower;
    public float dashCooldownTimerLength;
    float dashCooldownTimer;

    public float shieldMaxPower;
    float shieldPower;
    public float shieldDegredationRate;
    public GameObject shieldObject;
    Renderer shieldRenderer;

    public Canvas shieldBarCanvas;
    public Image shieldBarImage;

    public GameObject warriorObject;

    Rigidbody rb;
    Camera sceneCamera;



    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        shieldRenderer = shieldObject.GetComponent<Renderer>();
        sceneCamera = Camera.main;
        shieldPower = shieldMaxPower;
    }

    void Update()
    {
        //Movement
        if (Input.GetMouseButton(0))
        {
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                direction = -(shieldObject.transform.position - hit.point).normalized;
            }
        }
        else
        {
            direction = Vector2.zero;
        }

        //Dash
        dashCooldownTimer -= Time.deltaTime;
        if (dashCooldownTimer < 0) dashCooldownTimer = 0;
        if (Input.GetMouseButtonDown(1) && dashCooldownTimer <= 0)
        {
            dashCooldownTimer = dashCooldownTimerLength;

            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                direction = -(shieldObject.transform.position - hit.point).normalized;
            }

            Vector3 dashForce = new Vector3(direction.x, 0, direction.z) * dashPower;

            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            
            rb.AddForce(dashForce, ForceMode.Impulse);
        }

        //Shield stuff
        shieldPower -= Time.deltaTime * shieldDegredationRate;
        shieldDegredationRate += 0.04f * Time.deltaTime;

        Vector3 shieldSize = Vector3.one * (2 * (2 - ((shieldMaxPower - shieldPower) / shieldMaxPower)));
        shieldObject.transform.localScale = shieldSize;

        Color textureColor = shieldRenderer.material.color;
        textureColor.a = 0.5f * (1 - ((shieldMaxPower - shieldPower) / shieldMaxPower));
        shieldRenderer.material.color = textureColor;

        shieldBarImage.fillAmount = 1 - ((shieldMaxPower - shieldPower) / shieldMaxPower);
        shieldBarCanvas.transform.LookAt(sceneCamera.transform.position + sceneCamera.transform.rotation * Vector3.back, sceneCamera.transform.rotation * Vector3.up);
        shieldBarCanvas.transform.Rotate(new Vector3(0, 180, 0));
        shieldBarCanvas.transform.position = new Vector3(shieldObject.transform.position.x, shieldObject.transform.position.y + 1, shieldObject.transform.position.z);

        if (shieldPower <= 0)
        {
            print("Spells cast: " + playerTwo.GetSpellsCast());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Warrior model stuff
        warriorObject.transform.localPosition = shieldObject.transform.localPosition;
        warriorObject.transform.rotation = shieldObject.transform.rotation;
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(direction.x, 0, direction.z) * acceleration * Time.deltaTime);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }



    public void AddShieldPower(float amount)
    {
        shieldPower += amount;
        if (shieldPower > shieldMaxPower) shieldPower = shieldMaxPower;
    }
}