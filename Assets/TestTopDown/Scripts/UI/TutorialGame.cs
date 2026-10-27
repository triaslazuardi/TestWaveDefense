using Nitzz.utility.Sounds;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{

    static bool shownThisSession;

    [Serializable]
    public class Page
    {
        public string title;
        [TextArea(3, 8)] public string body;
    }

    [Header("Content")]
    [SerializeField] Page[] pages;

    [Header("UI")]
    [SerializeField] GameObject parentPanel;      
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text bodyText;
    [SerializeField] TMP_Text pageText;
    [SerializeField] Button prevButton;

    [SerializeField] TMP_Text nextLabel;
 
    int index;

    void Start()
    {
        if (!shownThisSession)
        {
            shownThisSession = true;
            Open();
        }
        else
        {
            parentPanel.SetActive(false);
        }
    }

    void OnDestroy() => Time.timeScale = 1f;

    public void Open()
    {
        SoundManager.Play(Sfx.Click);
        index = 0;
        parentPanel.SetActive(true);
        Time.timeScale = 0f;    
        Show();
    }

    public void OnSkip() //skip
    {
        SoundManager.Play(Sfx.Click);
        parentPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnPrev()
    {
        if (index <= 0) return;
        SoundManager.Play(Sfx.Click);
        index--;
        Show();
    }

    public void OnNext()
    {
        if (index >= pages.Length - 1) { OnSkip(); return; }
        SoundManager.Play(Sfx.Click);
        index++;
        Show();
    }

    public void Show()
    {
        var p = pages[index];
        titleText.text = p.title;
        bodyText.text = p.body;
        pageText.text = $"{index + 1}/{pages.Length}";
        prevButton.interactable = index > 0;
        nextLabel.text = index >= pages.Length - 1 ? "Play !" : "Next";
    }
}