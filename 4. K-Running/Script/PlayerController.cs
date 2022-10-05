using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public Joystick joy;

    public AudioClip deathClip;
    public AudioClip runClip;
    public AudioClip jump1Clip;
    public AudioClip jump2Clip;
    public AudioClip jump1VoiceClip;
    public AudioClip jump2VoiceClip;

    public float jumpForce = 300f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    private AudioSource voiceAudio;

    private AudioSource coinAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        voiceAudio = transform.GetChild(0).GetComponent<AudioSource>();
        coinAudio = transform.GetChild(1).GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isDead || Pause.isPause)
        {
            return;
        }
        
        float xInput = joy.Horizontal;

        if(xInput != 0)
        {
            transform.Translate(xInput*0.3f, 0, 0);
        }

        if (Input.GetMouseButtonDown(0) && jumpCount < 2 && Pause.isPauseBtn == false)
        {
            playerAudio.clip = jump1Clip;
            voiceAudio.clip = jump1VoiceClip;

            if (jumpCount == 1)
            {
                animator.SetBool("Jumped", true);
                playerAudio.clip = jump2Clip;
                voiceAudio.clip = jump2VoiceClip;
            }

            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            voiceAudio.Play();
            playerAudio.Play();
        }

        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);

    }

    private void Die()
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.volume = 0.7f;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dead" && !isDead)
        {
            Die();
        }

        if(other.tag == "Item" && !isDead)
        {
            other.gameObject.SetActive(false);
            GameManager.instance.AddScore(1);
            coinAudio.Play();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("Jumped", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
