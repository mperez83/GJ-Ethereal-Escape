using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTwo : MonoBehaviour
{
    public PlayerOne playerOne;

    public enum Mode {Typing, Moving};
    Mode mode = Mode.Typing;

    //Typing stuff
    Camera sceneCamera;
    public Canvas spellCanvas;
    public GameObject spellPrefab;
    Spell currentSpell;
    int spellsCast;

    //Moving stuff
    Vector2 inputAxis;
    public float acceleration;

    Rigidbody rb;
    
    
    
    void Start()
    {
        sceneCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        currentSpell = Instantiate(spellPrefab, spellCanvas.transform).GetComponent<Spell>();
        currentSpell.SetPlayerTwo(this);
    }

    void Update()
    {
        //Make spells point at camera
        spellCanvas.transform.LookAt(sceneCamera.transform.position + (sceneCamera.transform.rotation * Vector3.back), sceneCamera.transform.rotation * Vector3.up);
        spellCanvas.transform.Rotate(0, 180, 0);

        switch (mode)
        {
            case Mode.Typing:
                if (Input.GetButtonDown("P2_Action"))
                {
                    mode = Mode.Moving;
                }
                break;

            case Mode.Moving:
                inputAxis = new Vector2(Input.GetAxisRaw("P2_Horizontal"), Input.GetAxisRaw("P2_Vertical"));

                if (Input.GetButtonDown("P2_Action"))
                {
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
                rb.AddForce(new Vector3(inputAxis.x, 0, inputAxis.y) * acceleration * Time.deltaTime);
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