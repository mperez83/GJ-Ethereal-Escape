using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler instance;

    public int amountOfCrystalsToSpawn;
    public int winAmount;

    int manaGooCollected;

    public Image manaImage;

    public GameObject crystalClusterPrefab;
    public Collider spawnArea;
    public LayerMask collisionMask;



    void Start()
    {
        instance = this;

        manaImage.fillAmount = 1 - ((winAmount - manaGooCollected) / (float)winAmount);

        for (int i = 0; i < amountOfCrystalsToSpawn; i++)
        {
            Vector3 rayPos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), 0, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            RaycastHit hit;
            if (Physics.Raycast(rayPos, Vector3.down, out hit, Mathf.Infinity, collisionMask))
            {
                Vector3 spawnPos = new Vector3(hit.point.x, hit.point.y + 0.4f, hit.point.z);
                Instantiate(crystalClusterPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            GameManager.instance.ChangeScene("MainMenu");
        }
    }



    public void CollectManaGoo(int amount)
    {
        manaGooCollected += amount;
        manaImage.fillAmount = 1 - ((winAmount - manaGooCollected) / (float)winAmount);

        if (manaGooCollected >= winAmount)
        {
            GameManager.instance.ChangeScene("Credits");
        }
    }
}