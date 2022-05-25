using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject fishPrefab;

    // arrows 0=up 1=down 2=left 3=right
    public GameObject[] arrowPrefab;
    private GameplayManager gameplayManager;

    public bool fishOnScreen = false;

    void Start()
    {
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
        SpawnFish();
    }

    void Update()
    {
        //if no fish, spawn a fish.
        if (!fishOnScreen && !gameplayManager.gameIsOver)
        {
            SpawnFish();
        }
    }
    void SpawnFish()
    {
        //spawn a fish with random X coordinates
        Instantiate(fishPrefab, generateRandomPosition(), fishPrefab.transform.rotation);
        //Variable used to prevent more than one fish on screen at a time
        fishOnScreen = true;
    }

    public GameObject SpawnArrow()
    {
        if (!gameplayManager.gameIsOver)
        {
            gameplayManager.currentPrompt = Random.Range(0, 4);
            return Instantiate(arrowPrefab[gameplayManager.currentPrompt]);
        }
        else return null;
    }

    private Vector3 generateRandomPosition()
    {
        float randomX = Random.Range(-3.0f, 11.5f);
        float offscreenY = -6.0f;
        int zPreset = -2;

        return new Vector3(randomX, offscreenY, zPreset);
    }

}
