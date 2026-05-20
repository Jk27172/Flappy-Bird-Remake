using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flyPower;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public AudioSource gameOverSound;
    public MusicScript musicScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (birdIsAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                myRigidbody.linearVelocity = Vector2.up * flyPower;
            }

            Vector2 screenPos = Camera.main.WorldToViewportPoint(transform.position);

            if (screenPos.y > 1 || screenPos.y < 0)
            {
                TriggerGameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        if (!birdIsAlive) return;

        birdIsAlive = false;
        logic.gameOver();
        gameOverSound.Play();
        musicScript.StopMusic();
        Time.timeScale = 0;
    }
}
