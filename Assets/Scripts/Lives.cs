using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Lives : MonoBehaviour
{

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public Sprite fullHeart;
    public Sprite threeQuarterHeart;
    public Sprite halfHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;

    public AudioSource audioSource;
    public AudioClip[] hurtSounds;
    public AudioClip oneUpSound;
    private int currentLives;
    //public int startingLives = 3;


    //public float transitionTime = 1f;
    //public Animator animator;
    //public GameObject image;

    //public PlayerJump playerJump;

    private Vector3 initialPosition;

    public GameObject restartButton;

    public int maxHealth;
    public int currentHealth;

    public GameObject quitButton;
    public GameObject gameOverTextObject;
    public TextMeshProUGUI livesText;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        rb.isKinematic = false;
        currentHealth = maxHealth;
        quitButton.SetActive(false);
        gameOverTextObject.SetActive(false);
        currentLives = PlayerPrefs.GetInt("lives");
        SetLivesText();
        InitHearts();
        initialPosition = transform.position;
        restartButton.SetActive(false);
    }

    void OnDeath()
    {
        currentLives--;
        //yield return new WaitForSeconds(2.0f);
        currentHealth = maxHealth;
        //StartCoroutine(Transition());
        transform.position = initialPosition;
        InitHearts();
        SetLivesText();
    }

    private void InitHearts()
    {
        heart1.GetComponent<Image>().sprite = fullHeart;
        heart2.GetComponent<Image>().sprite = fullHeart;
        heart3.GetComponent<Image>().sprite = fullHeart;
    }

    void OnHit(int damage)
    {
        currentHealth -= damage;
        audioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
        if (currentHealth < 0)
        {
            heart1.GetComponent<Image>().sprite = emptyHeart;
            heart2.GetComponent<Image>().sprite = emptyHeart;
            heart3.GetComponent<Image>().sprite = emptyHeart;
            OnDeath();
        }
        switch (currentHealth)
        {
            case 0:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = emptyHeart;
                heart3.GetComponent<Image>().sprite = emptyHeart; OnDeath(); break;
            case 1:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = emptyHeart;
                heart3.GetComponent<Image>().sprite = quarterHeart; break;
            case 2:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = emptyHeart;
                heart3.GetComponent<Image>().sprite = halfHeart; break;
            case 3:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = emptyHeart;
                heart3.GetComponent<Image>().sprite = threeQuarterHeart; break;
            case 4:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = emptyHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 5:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = quarterHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 6:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = halfHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 7:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = threeQuarterHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 8:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                heart2.GetComponent<Image>().sprite = fullHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 9:
                heart1.GetComponent<Image>().sprite = quarterHeart;
                heart2.GetComponent<Image>().sprite = fullHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 10:
                heart1.GetComponent<Image>().sprite = halfHeart;
                heart2.GetComponent<Image>().sprite = fullHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 11:
                heart1.GetComponent<Image>().sprite = threeQuarterHeart;
                heart2.GetComponent<Image>().sprite = fullHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
            case 12:
                heart1.GetComponent<Image>().sprite = fullHeart;
                heart2.GetComponent<Image>().sprite = fullHeart;
                heart3.GetComponent<Image>().sprite = fullHeart; break;
        }
    }

    private void Update()
    {
        PlayerPrefs.SetInt("lives", currentLives);
    }

    void SetLivesText()
    {
        livesText.text = "x " + currentLives.ToString();
        if (currentLives <= 0)
        {
            gameOverTextObject.SetActive(true);
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            PlayerPrefs.SetInt("lives", 3);
            currentLives = PlayerPrefs.GetInt("lives");
            rb.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            OnHit(4);
            if (currentHealth > 0 && currentHealth != maxHealth)
            {
                //transform.position = playerJump.lastGroundedPosition;
            }
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnHit(1);
        }
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OneUp"))
        {
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(oneUpSound);
            currentLives++;
            SetLivesText();
        }
    }


    //IEnumerator Transition()
    //{
    //    image.SetActive(true);
    //    animator.SetTrigger("Start");
    //    animator.ResetTrigger("Start");
    //    yield return new WaitForSeconds(transitionTime);
    //}

}
