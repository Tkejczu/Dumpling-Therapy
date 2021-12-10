using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{

    public float speed;
    public float rightScreenBorder;
    public float leftScreenBorder;
    public GameManager gameManager;
    private Animator anim;
    private AudioSource hitSound;
    private AudioSource pickUpSoundPositive;
    private AudioSource pickUpSoundNegative;

    private void Start()
    {
        anim = GetComponent<Animator>();
        var audioSources = GetComponents<AudioSource>();
        hitSound = audioSources[0];
        pickUpSoundPositive = audioSources[1];
        pickUpSoundNegative = audioSources[2];
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        SetPaddleBoundary();
        PaddleMovement(horizontal);
    }
    void PaddleMovement(float horizontal)
    {
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        if (transform.position.x < leftScreenBorder)
        {
            transform.position = new Vector2(leftScreenBorder, transform.position.y);
        }
        if (transform.position.x > rightScreenBorder)
        {
            transform.position = new Vector2(rightScreenBorder, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ExtraLife"))
        {
            pickUpSoundPositive.Play();

            gameManager.UpdateLives(1);
            Destroy(col.gameObject);
        }
        if(col.CompareTag("Enlarge"))
        {
            pickUpSoundPositive.Play();

            if (gameObject.transform.localScale.x < 0.9f)
            {
                gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0);
            }
            Destroy(col.gameObject);
        }
        if(col.CompareTag("Decrease"))
        {
            pickUpSoundNegative.Play();

            if (gameObject.transform.localScale.x >= 0.6f)
            {
                gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0);
            }
            Destroy(col.gameObject);
        }
        if(col.CompareTag("PlusPoints"))
        {
            pickUpSoundPositive.Play();

            gameManager.UpdateScore(500);
            Destroy(col.gameObject);
        }
        if (col.CompareTag("MinusPoints"))
        {
            pickUpSoundNegative.Play();

            gameManager.UpdateScore(-500);
            Destroy(col.gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball") && col.gameObject.GetComponent<Ball>().isBallLaunched == true)
        {
            Rigidbody2D ballRb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 bouncePoint = col.contacts[0].point;
            Vector3 paddleCenter = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
            

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - bouncePoint.x;

            if (bouncePoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), col.gameObject.GetComponent<Ball>().speed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), col.gameObject.GetComponent<Ball>().speed));
            }
            hitSound.Play();
            anim.SetTrigger("PaddleHit");
        }
    }

    void SetPaddleBoundary()
    {
        if(gameObject.transform.localScale.x == 0.6f)
        {
            rightScreenBorder = 4.47f;
            leftScreenBorder = -4.47f;
}
        if (gameObject.transform.localScale.x > 0.6f)
        {
            rightScreenBorder = 4.25f;
            leftScreenBorder = -4.25f;
        }
        if (gameObject.transform.localScale.x < 0.6f)
        {
            rightScreenBorder = 4.75f;
            leftScreenBorder = -4.75f;
        }
    }
}
