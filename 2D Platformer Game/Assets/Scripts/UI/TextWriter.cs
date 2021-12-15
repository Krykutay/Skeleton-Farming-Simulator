using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    static TextWriter Instance;

    List<TextWriterSingle> _textWriterSingleList;

    void Awake()
    {
        Instance = this;
        _textWriterSingleList = new List<TextWriterSingle>();
    }

    void Update()
    {
        for (int i = 0; i < _textWriterSingleList.Count; i++)
        {
            bool destroyInstance = _textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                _textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    public static TextWriterSingle AddWriter_Static(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete)
    {
        if (removeWriterBeforeAdd)
        {
            Instance.RemoveWriter(uiText);
        }

        return Instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
    }

    TextWriterSingle AddWriter(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
        _textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(TMP_Text uiText)
    {
        Instance.RemoveWriter(uiText);
    }

    void RemoveWriter(TMP_Text uiText)
    {
        for (int i = 0; i < _textWriterSingleList.Count; i++)
        {
            if (_textWriterSingleList[i].GetUIText() == uiText)
            {
                _textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    /*
     * Represents a single TextWriter instance
     * */
    public class TextWriterSingle
    {
        Action _onComplete;

        TMP_Text _uiText;
        string _textToWrite;
        int _characterIndex;
        float _timePerCharacter;
        float _timer;
        bool _invisibleCharacters;

        public TextWriterSingle(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
        {
            _uiText = uiText;
            _textToWrite = textToWrite;
            _timePerCharacter = timePerCharacter;
            _characterIndex = 0;
            _invisibleCharacters = invisibleCharacters;
            _onComplete = onComplete;
        }

        // Returns true on complete
        public bool Update()
        {
            _timer -= Time.deltaTime;
            while (_timer <= 0f)
            {
                // Display next char
                _timer += _timePerCharacter;
                _characterIndex++;

                string text = _textToWrite.Substring(0, _characterIndex);

                if (_invisibleCharacters)
                {
                    text += "<color=#00000000>" + _textToWrite.Substring(_characterIndex) + "</color>";
                }

                _uiText.text = text;

                if (_characterIndex >= _textToWrite.Length)
                {
                    // Entire string displayed
                    _uiText = null;
                    _onComplete?.Invoke();
                    return true;
                }
            }

            return false;
        }

        public TMP_Text GetUIText()
        {
            return _uiText;
        }

        public bool IsActive()
        {
            return _characterIndex < _textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            _uiText.text = _textToWrite;
            _characterIndex = _textToWrite.Length;
            _onComplete?.Invoke();
            TextWriter.RemoveWriter_Static(_uiText);
        }

    }
}
