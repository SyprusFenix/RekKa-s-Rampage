using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private bool isMoving;
    public float moveSpeed = 1f;
    public float moveDelay = 1f;
    public float timer = 0;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    private float yDestroyBounds = 1f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            EnemyMovement();

            if (transform.position.y < yDestroyBounds)
            {
                Destroy(gameObject);
                gameManager.UpdateHealth(-1);
            }
        }
    }

    void EnemyMovement()
    {
        timer += Time.deltaTime;
        
        // Moving the enemy once the moveDelay timer is up
        if (timer >= moveDelay && !isMoving)
            StartCoroutine(MoveEnemyDown(Vector3.down));
    }

    private IEnumerator MoveEnemyDown(Vector3 direction)
    {
        isMoving = true;

        // starting our movement timer
        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + (moveSpeed * direction);

        // moving with a Lerp from origPos to targetPos at a speed based on our timeToMove
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // this is kind of a clean up step to make the character go exactly where they're supposed to go, 
        // because it can get just a little off over a long time
        transform.position = targetPos;
        timer = 0;
        isMoving = false;
    }
}
