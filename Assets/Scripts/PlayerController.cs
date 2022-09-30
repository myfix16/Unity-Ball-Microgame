using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private GameObject focalPoint;

    public GameObject powerupIndicator;

    public float speed = 5.0f;

    public bool gameOver = false;

    public int powerupTime;

    public float powerupStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        StartCoroutine(PowerupCountdownRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(forwardInput * speed * focalPoint.transform.forward);

        powerupIndicator.transform.position = transform.position + new Vector3(0, 0.06f, 0);

        // logic for game over
        if (transform.position.y < -10)
        {
            gameOver = true;
            Debug.Log("Game over!");
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerupTime += 7;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        while (true)
        {
            if (powerupTime > 0)
            {
                powerupTime--;
                if (powerupTime == 0)
                {
                    powerupIndicator.gameObject.SetActive(false);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (powerupTime > 0 && collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            Debug.Log($"Collided with {collision.gameObject.name} with remaining powerup time: {powerupTime} s.");
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
