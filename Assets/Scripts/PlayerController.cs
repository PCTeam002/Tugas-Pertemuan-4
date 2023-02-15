using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    float xVal, vVal, zVal, jump, yBound, zBound;
    private int maxJumps = 2;
    private int jumpCount = 0;
    private int score;
    private int timer;

    public float speed;
    public float jumpForce;
    
    private bool isGameOver;
    private bool isGrounded;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI TimerText;
    Rigidbody rb;
    void Start()
    {
        yBound = 5;
        zBound = 4.5f;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("UpdateScore", 1f, 0.5f);
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        zVal = Input.GetAxis("Vertical");
        xVal = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");

        if (isGrounded)
        {
            jumpCount = 0;
        }

        //transform.position += new Vector3(zVal, jump, -xVal) * Time.deltaTime * 10;
        rb.AddForce(new Vector3(zVal, 0, -xVal) * speed);
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCount++;
            Debug.Log("isground = " + isGrounded);
        }
        LimitBounds();
        GameOver();
    }
    void LimitBounds()
    {
        if (transform.position.x > 55)
        {
            transform.position = new Vector3(45, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -5)
        {
            transform.position = new Vector3(-5, transform.position.y, transform.position.z);
        }
        if (transform.position.y > (yBound))
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
        else if (transform.position.y < (-yBound))
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
        if (transform.position.z > (zBound))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z < (-zBound))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {   
            gameOverText.gameObject.SetActive(true);
            Debug.Log("GAMEOVER!");
            isGameOver = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("isground = " + isGrounded);
        }
    }
    void GameOver()
    {
        if (isGameOver)
        {
            Time.timeScale = 0;
        }
    }
    void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score : " + score;
    }
    void UpdateTimer()
    {
        timer += 1;
        TimerText.text = "Timer : " + timer;
    }
}
