using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameplayManager : MonoBehaviour
{
    // UI
    public GameObject endCard;
    public TextMeshProUGUI youCaught;
   
    // Access + Declaration
    private SpawnManager spawnManager;
    private PlayerInput playerInput;
    private GameObject recentArrow;
    private GameObject currentFish;

    // fish properties
    public bool caught = false;
    public bool onHook = false;
    private float spawnRateLimit = 0.5f;
    private float spawnRateDecrementer = 0.5f;
    // Idea: Add mini-decrementer for when you get closer to limit?

    // Gameplay Tracker
    public int catchCounter = 0;
    public int sequenceLength = 1;
    public int correctInputs = 0;
    public bool gameIsOver = false;
    // Idea: Add "total correct inputs" and send messages to the player as they fish?
    // Idea: Throw random stuff on screen to distract player as game goes on?

    // Arrow Related
    private KeyCode translatedPlayerInput;
    public int currentPrompt;
    public float arrowSpawnSpeed = 3.0f;
    public bool arrowIsCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Allow restart when game is over
        if (gameIsOver && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (spawnManager.fishOnScreen)
        {
            currentFish = GameObject.FindGameObjectWithTag("Fish");
        }

        if (onHook && !gameIsOver)
        {
            if (!arrowIsCompleted && Input.anyKeyDown && DirectionTranslator(currentPrompt) == playerInput.currentInput)
            {
                SetArrowToCompleted();
                correctInputs++;
                if (correctInputs == sequenceLength)
                {
                    caught = true;
                }
            }
            else if (Input.anyKeyDown && playerInput.currentInput != DirectionTranslator(currentPrompt) && playerInput.currentInput != KeyCode.None)
            {
                GameOver();
            } 
            // Adding this code will: punish player for repeating "correct" input after initial press.
                //other idea: Add message that says "Chill, bro." Optional more intense messages if player keeps doing it.
                // Long, elaborate sequence of secret messages that are almost impossible to access?
            //else if (arrowIsCompleted && Input.anyKeyDown && playerInput.currentInput != KeyCode.None)
            //{
            //    GameOver();
            //}
            if (caught)
            {
                CatchFish();
            }
        };
    }


    // To Do: Coroutine - ArrowTimer
    // will: Switch arrows on a timer
    // will: declare player loss if timer runs out.
    public IEnumerator ArrowTimer()
    {
        for (int i = 0; sequenceLength > i; i++)
        {
            playerInput.hasPlayerReacted = false;
            recentArrow = spawnManager.SpawnArrow();

            yield return new WaitForSeconds(arrowSpawnSpeed);
            if (playerInput.hasPlayerReacted == false)
            {
                GameOver();
            }
            Destroy(recentArrow);
            arrowIsCompleted = false;

           
        }
    }

    void CatchFish()
    {
        Destroy(currentFish);
        Destroy(recentArrow);
        SetNewSpawnRate();
        spawnManager.fishOnScreen = false;
        arrowIsCompleted = false;
        catchCounter++;
        sequenceLength++;
        caught = false;
        onHook = false;
        print("You have caught " + catchCounter + " fish!");
        correctInputs = 0;
    }

    void SetNewSpawnRate()
    {
        if (arrowSpawnSpeed > spawnRateLimit)
        {
            arrowSpawnSpeed -= spawnRateDecrementer;
        }
    }

    void InputChecker()
    {
        // move function here
    }

    void SetArrowToCompleted()
    {
        arrowIsCompleted = true;
        recentArrow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.2f);
    }

    //translate arrowDirection selector to KeyCodes
    KeyCode DirectionTranslator(int arrowNumber)
    {
        if (arrowNumber == 0)
        {
            translatedPlayerInput = KeyCode.UpArrow;
        }
        else if (arrowNumber == 1)
        {
            translatedPlayerInput = KeyCode.DownArrow;
        }
        else if (arrowNumber == 2)
        {
            translatedPlayerInput = KeyCode.LeftArrow;
        }
        else if (arrowNumber == 3)
        {
            translatedPlayerInput = KeyCode.RightArrow;
        }
        else
        {
            translatedPlayerInput = KeyCode.None;
            Debug.Log("ERROR: no arrowNumber in DirectionTranslator");
        }
        return translatedPlayerInput;
    }

    void GameOver()
    {
        Destroy(recentArrow);
        Destroy(GameObject.Find("Player"));
        youCaught.text = "You Caught " + catchCounter + " Fish";
        gameIsOver = true;
        endCard.SetActive(true);
    }

    void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}