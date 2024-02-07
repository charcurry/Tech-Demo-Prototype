using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupController : MonoBehaviour
{
    public int numberOfPickups;

    public AudioSource audioSource;
    public AudioClip clip;

    public string levelName;

    public int optionalPickups;

    public string levelNameCompletedKey;

    public bool winOnTalk;

    public GameObject continueButton;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>();
        PlayerPrefs.SetInt(levelName, 0);
        numberOfPickups = CountPickups();
        Time.timeScale = 1f;
        SetCountText();
        count = 0;
        winTextObject.SetActive(false);
        continueButton.SetActive(false);
    }

    // Update is called once per frame

    void SetCountText()
    {
        int pickupsLeft = numberOfPickups - count;
        countText.text = "x " + count.ToString() + "/" + numberOfPickups;
        if (PlayerPrefs.GetInt(levelName) >= numberOfPickups)
        {
            if (winOnTalk)
            {
                return;
            }
            else
            {
                winTextObject.SetActive(true);
                continueButton.SetActive(true);
                Time.timeScale = 0f;
                SetBool(levelNameCompletedKey, true);
                PlayerPrefs.SetString("lastLevelCompleted", levelName);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            audioSource.PlayOneShot(clip);
            other.gameObject.SetActive(false);
            count = count + 1;
            PlayerPrefs.SetInt(levelName, PlayerPrefs.GetInt(levelName) + 1);
            SetCountText();
        }

    }

    public void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public int CountPickups()
    {
        return GameObject.FindGameObjectsWithTag("PickUp").Count() - optionalPickups;
    }
}
