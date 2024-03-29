using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateText : MonoBehaviour
{
    public GameObject buttonPrompt;

    public TextAsset theText;

    public int startLine;
    public int endLine;

    public string[] textLines;

    public TextBoxManager theTextBox;

    public bool requireButtonPress;
    private bool waitForPress;

    public bool destroyWhenActivated;

    // Start is called before the first frame update
    void Start()
    {
        buttonPrompt.SetActive(false);
        theTextBox = FindObjectOfType<TextBoxManager>();

        if (theText != null)
        {
            textLines = (theText.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForPress && Input.GetButtonDown("Interact"))
        {
            if (!theTextBox.isActive)
            {
                theTextBox.isActive = true;
                theTextBox.ReloadScript(theText);
                theTextBox.currentLine = startLine;
                theTextBox.endAtLine = endLine;
                theTextBox.EnableTextBox();
            }

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            buttonPrompt.SetActive(!theTextBox.isActive);
            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }
            else
            {
                theTextBox.ReloadScript(theText);
                theTextBox.currentLine = startLine;
                theTextBox.endAtLine = endLine;
                theTextBox.EnableTextBox();
            }


            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            buttonPrompt.SetActive(false);
            theTextBox.DisableTextBox();
            waitForPress = false;
        }
    }
}