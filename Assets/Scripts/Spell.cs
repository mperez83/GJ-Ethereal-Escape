using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spell : MonoBehaviour
{
    public string[] potentialSpells;
    string spellName;

    public GameObject spellLetterPrefab;
    SpellLetter[] spellLetters;
    PlayerTwo playerTwo;

    int currentLetter;



    void Start()
    {
        spellName = potentialSpells[Random.Range(0, potentialSpells.Length)];
        spellLetters = new SpellLetter[spellName.Length];

        for (int i = 0; i < spellName.Length; i++)
        {
            GameObject newLetter = Instantiate(spellLetterPrefab, transform);
            newLetter.transform.localPosition = new Vector3(-((spellName.Length * 200) / 2) + (i * 200), 0, 0);

            spellLetters[i] = newLetter.GetComponent<SpellLetter>();
            spellLetters[i].SetLetter(spellName[i].ToString().ToLower());
        }
    }

    void Update()
    {
        if (currentLetter < spellName.Length && Input.GetKeyDown(spellLetters[currentLetter].GetLetter()))
        {
            spellLetters[currentLetter].ActivateLetter();
            currentLetter++;
            if (currentLetter >= spellName.Length)
            {
                playerTwo.CreateNextSpell(spellName.Length);

                foreach (SpellLetter spLet in spellLetters)
                {
                    spLet.KillLetter();
                }

                LeanTween.scale(gameObject, transform.localScale * 2, 0.3f);
                LeanTween.moveLocalY(gameObject, 150, 0.3f).setEase(LeanTweenType.easeOutExpo).setOnComplete(() =>
                {
                    Destroy(gameObject);
                });
            }
        }
    }



    public void SetPlayerTwo(PlayerTwo temp) { playerTwo = temp; }
}