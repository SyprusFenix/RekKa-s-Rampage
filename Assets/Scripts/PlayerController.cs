using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    private float yMaxBound = 9.0f;
    private float yMinBound = 2.0f;
    private float xMaxBound = 14.0f;
    private float xMinBound = 0.0f;

    public GameObject ringOfFire;
    private GameManager gameManager;
    private AudioSource fireballAudio;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        fireballAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            PlayerMovement();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(FlameAttack());
                fireballAudio.Play();
            }
        }
    }

    void PlayerMovement()
    {
        // Checking for wasd input, that the player isn't moving, and that they aren't already at an edge
        //  Then run the Move Player coroutine
        if (Input.GetButton("Up") && !isMoving && transform.position.y < yMaxBound)
            StartCoroutine(MovePlayer(Vector3.up));

        if (Input.GetButton("Left") && !isMoving && transform.position.x > xMinBound)
            StartCoroutine(MovePlayer(Vector3.left));

        if (Input.GetButton("Down") && !isMoving && transform.position.y > yMinBound)
            StartCoroutine(MovePlayer(Vector3.down));

        if (Input.GetButton("Right") && !isMoving && transform.position.x < xMaxBound)
            StartCoroutine(MovePlayer(Vector3.right));
    }
    
    private IEnumerator MovePlayer(Vector3 direction)
    {
        // making isMoving true so that we don't get multiple inputs happening until the first is done
        isMoving = true;

        // starting our movement timer
        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

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

        // making it ok to move again
        isMoving = false;
    }

    IEnumerator FlameAttack()
    {
        ringOfFire.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        ringOfFire.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && !ringOfFire.gameObject.activeSelf)
        {
            Debug.Log("Player has lost a life.");
            gameManager.UpdateHealth(-1);
        }

        if(other.gameObject.CompareTag("Tree"))
        {
            timeToMove = 0.4f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            timeToMove = 0.2f;
        }
    }
}
