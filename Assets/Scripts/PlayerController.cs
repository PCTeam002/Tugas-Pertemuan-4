using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    float xVal, vVal, zVal, jump, yBound, zBound;
    public float speed;
    public float jumpForce;
    
    public TextMeshProUGUI gameOverText;
    Rigidbody rb;
    void Start()
    {
        yBound = 5;
        zBound = 4.5f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        zVal = Input.GetAxis("Vertical");
        xVal = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");

        //transform.position += new Vector3(zVal, jump, -xVal) * Time.deltaTime * 10;
        rb.AddForce(new Vector3(zVal, 0, -xVal) * speed);
        if (Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        LimitBounds();
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
        }
    }
}
