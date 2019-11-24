using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellLetter : MonoBehaviour
{
    string letter;

    TextMeshProUGUI letterText;

    void Start()
    {
        letterText = GetComponent<TextMeshProUGUI>();
        letterText.text = letter;

        transform.localPosition = new Vector3(transform.localPosition.x, -50, transform.localPosition.z);
        letterText.color = new Color(1, 1, 1, 0);

        LeanTween.moveLocalY(gameObject, 0, 0.3f);
        LeanTween.value(gameObject, new Color(1, 1, 1, 0), Color.white, 0.3f).setEase(LeanTweenType.easeOutCubic).setOnUpdate((Color newColor) =>
        {
            letterText.color = newColor;
        });
    }

    void Update()
    {
        
    }

    public void ActivateLetter()
    {
        //Set letter to correct position (in case it isn't)
        LeanTween.cancel(gameObject);
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);

        //Actually animate letter
        LeanTween.moveLocalY(gameObject, 125, 0.3f).setEase(LeanTweenType.easeOutExpo);
        //LeanTween.scale(gameObject, transform.localScale * 0.5f, 0.5f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.value(gameObject, Color.white, Color.green, 0.3f).setEase(LeanTweenType.easeOutExpo).setOnUpdate((Color newColor) =>
        {
            letterText.color = newColor;
        });
    }

    public void KillLetter()
    {
        //Set letter to correct position (in case it isn't)
        LeanTween.cancel(gameObject);
        //transform.localPosition = new Vector3(transform.localPosition.x, 125, transform.localPosition.z);
        letterText.color = Color.green;

        LeanTween.moveLocalY(gameObject, 250, 0.3f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.value(gameObject, Color.green, new Color(0, 1, 0, 0), 0.3f).setEase(LeanTweenType.easeOutCubic).setOnUpdate((Color newColor) =>
        {
            letterText.color = newColor;
        }).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void SetLetter(string temp) { letter = temp; }
    public string GetLetter() { return letter; }
}