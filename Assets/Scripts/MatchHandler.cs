using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchHandler : MonoBehaviour
{
    int manaGooCollected;

    public static MatchHandler instance;



    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }



    public void CollectManaGoo(int amount)
    {
        manaGooCollected += amount;
        if (manaGooCollected >= 3)
        {
            SceneManager.LoadScene("Win");
        }
    }
}