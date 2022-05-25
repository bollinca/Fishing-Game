using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // accessing public
    public GameObject hook;
    private GameplayManager gameplayManager;

    // Start is called before the first frame update
    void Start()
    {
        hook = GameObject.Find("Hook");
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
    }

    // Update is called once per frame
    void Update()
    { if (!gameplayManager.gameIsOver)
        {
            transform.Translate((hook.transform.position - transform.position) * Time.deltaTime);
        }
    }

        //fish is considered "on hook" when touching bobber (on trigger)
        private void OnTriggerEnter2D(Collider2D collision)
    {
        //if fish touches hook, fish disappears.
        if (collision.gameObject.CompareTag("Hook"))
        {
            gameplayManager.onHook = true;
            StartCoroutine(gameplayManager.ArrowTimer());
            //will: if fish escaped, fish swims away.
        }
    }
}
