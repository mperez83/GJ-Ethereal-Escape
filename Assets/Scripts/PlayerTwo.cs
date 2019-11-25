using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTwo : MonoBehaviour
{
    public PlayerOne playerOne;

    public enum Mode {Typing, Moving};
    Mode mode = Mode.Typing;

    public LayerMask collisionMask;

    //Typing stuff
    public Canvas spellCanvas;
    public GameObject spellPrefab;
    Spell currentSpell;
    int spellsCast;

    //Moving stuff
    public GameObject bootObject;
    Vector2 inputAxis;
    public float acceleration;
    Vector3 faceDir;
    Vector3 curVel;

    Rigidbody rb;
    Camera sceneCamera;



    void Start()
    {
        sceneCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        currentSpell = Instantiate(spellPrefab, spellCanvas.transform).GetComponent<Spell>();
        currentSpell.SetPlayerTwo(this);
    }

    void Update()
    {
        //Make the worldspace camera point at the camera
        spellCanvas.transform.LookAt(sceneCamera.transform.position + (sceneCamera.transform.rotation * Vector3.back), sceneCamera.transform.rotation * Vector3.up);
        spellCanvas.transform.Rotate(0, 180, 0);

        //Make the wizard hover some distance above the ground
        Vector3 rayPos = new Vector3(transform.position.x, 0, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(rayPos, Vector3.down, out hit, Mathf.Infinity, collisionMask))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.75f, transform.position.z);
        }

        switch (mode)
        {
            case Mode.Typing:
                if (Input.GetButtonDown("P2_Action"))
                {
                    currentSpell.gameObject.SetActive(false);
                    bootObject.SetActive(true);
                    mode = Mode.Moving;
                }
                break;

            case Mode.Moving:
                inputAxis = new Vector2(Input.GetAxisRaw("P2_Horizontal"), Input.GetAxisRaw("P2_Vertical"));

                if (Input.GetButtonDown("P2_Action"))
                {
                    currentSpell.gameObject.SetActive(true);
                    bootObject.SetActive(false);
                    mode = Mode.Typing;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (mode)
        {
            case Mode.Typing:
                //Nothing :3c
                break;

            case Mode.Moving:
                Vector3 camF = sceneCamera.transform.forward;
                Vector3 camR = sceneCamera.transform.right;
                camF.y = 0;
                camR.y = 0;
                camF = camF.normalized;
                camR = camR.normalized;

                Vector3 direction = (camF * inputAxis.y) + (camR * inputAxis.x);
                rb.AddForce(direction * acceleration * Time.deltaTime);

                if (inputAxis != Vector2.zero)
                {
                    faceDir = Vector3.SmoothDamp(faceDir, new Vector3(direction.x, 0, direction.z).normalized, ref curVel, 0.2f);
                    Quaternion rotation = Quaternion.LookRotation(faceDir, Vector3.up);
                    transform.rotation = rotation;
                }
                break;
        }
    }



    public void CreateNextSpell(int spellPower)
    {
        playerOne.AddShieldPower(spellPower);
        currentSpell = Instantiate(spellPrefab, spellCanvas.transform).GetComponent<Spell>();
        currentSpell.SetPlayerTwo(this);
        spellsCast++;
    }

    public int GetSpellsCast() { return spellsCast; }
}