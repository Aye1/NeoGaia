using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    public TextMeshPro textMesh;
    public Image characterImage;
    
    private string _currentText;
    private string _remainingText;
    private string[] _allTexts;
    private string _fullText;
    private int _textId;

    public bool displayTextFinished;
    public bool allTextsDisplayed;

    public event EventHandler dialogClosing;

	// Use this for initialization
	void Start () {
        // Set the render camera to avoid messing with Prefab camera
        GetComponent<Canvas>().worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        ManageInput();
	}

    public void Init(string[] text)
    {
        textMesh.text = "";
        _allTexts = text;
        allTextsDisplayed = false;
        StartDisplayingText();
    }

    private void StartDisplayingText()
    {
        _textId = 0;
        PrepareVariablesForNewText();
        StartCoroutine(DisplaySingleTextLetterByLetter());
    }

    private void DisplayNextText()
    {
        _textId++;
        PrepareVariablesForNewText();
        StartCoroutine(DisplaySingleTextLetterByLetter());
    }

    private void PrepareVariablesForNewText()
    {
        _currentText = "";
        _remainingText = _allTexts[_textId];
        _fullText = _allTexts[_textId];
        displayTextFinished = false;
    }

    private void DisplayNextTextOrClose()
    {
        if (_textId == _allTexts.Length - 1)
        {
            // All texts displayed, close DialogBox
            allTextsDisplayed = true;
            CloseDialog();
        }
        else
        {
            // There are other texts remaining
            DisplayNextText();
        }
    }

    private IEnumerator DisplaySingleTextLetterByLetter()
    {
        float timeBetweenLetters = 0.05f;
        Debug.Log("Coroutine");
        while (!displayTextFinished)
        {
            string firstLetter = _remainingText.Substring(0, 1);
            _currentText = _currentText + firstLetter;
            if (_currentText == _fullText)
            {
                displayTextFinished = true;
            }
            else
            {
                _remainingText = _remainingText.Substring(1);
            }
            textMesh.text = _currentText;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }

    private void FastEndCurrentText()
    {
        _currentText = _fullText;
        _remainingText = "";
        textMesh.text = _currentText;
        displayTextFinished = true;
    }


    private void ManageInput()
    {
        if(Input.anyKeyDown)
        {
            if (displayTextFinished)
            {
                DisplayNextTextOrClose();
            } else
            {
                FastEndCurrentText();
            }
        }
    }

    private void CloseDialog()
    {
        EventHandler handler = dialogClosing;
        if (handler != null)
        {
            handler(this, EventArgs.Empty);
        }
        Destroy(gameObject);
    }
}
