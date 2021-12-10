using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isBallLaunched;
    public Transform launchPoint;
    public float speed;
    public Transform explosion;
    public GameManager gameManager;
    public Transform[] powerUps;
    private AudioSource brickDestructionSound;
    private AudioSource brickHitSound;
    private AudioSource wallHitSound;
    private AudioSource lifeLoss;
    public GameObject paddle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var audioSources = GetComponents<AudioSource>();
        brickDestructionSound = audioSources[0];
        brickHitSound = audioSources[1];
        wallHitSound = audioSources[2];
        lifeLoss = audioSources[3];
    }


    void Update()
    {
        if (gameManager.gameOver)
        {
            return;
        }

        if (!isBallLaunched)
        {
            transform.position = launchPoint.position;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isBallLaunched)
        {
            isBallLaunched = true;
            rb.AddForce(new Vector2(0, speed));
        }
        if (isBallLaunched)
        {
            transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bottom"))
        {
            lifeLoss.Play();
            rb.velocity = Vector2.zero;
            isBallLaunched = false;
            paddle.transform.localScale = new Vector3(0.6f, 0.6f, 0);
            gameManager.UpdateLives(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Edge"))
        {
            wallHitSound.Play();
        }
        if(col.transform.CompareTag("Brick"))
        {
            Brick brick = col.gameObject.GetComponent<Brick>();
            if (brick.healthPoints > 1)
            {
                brickHitSound.Play();
                brick.BreakBrick();
            }
            else
            {
                Transform newExplosion = Instantiate(explosion, col.transform.position, col.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);
                int powerUpSpawnChance = Random.Range(1, 101);

                if (powerUpSpawnChance < 20)
                {
                    Transform randomPowerUp;
                    int powerUpIndex;

                    powerUpIndex = Random.Range(0, powerUps.Length);
                    randomPowerUp = powerUps[powerUpIndex];
                    Instantiate(randomPowerUp, col.transform.position, col.transform.rotation);
                }

                brickDestructionSound.Play();
                gameManager.UpdateScore(brick.pointValue);
                Destroy(col.gameObject);
            }
        }
    }
}
