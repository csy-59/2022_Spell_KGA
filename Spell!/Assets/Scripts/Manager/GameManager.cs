using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;
using UnityEngine.SceneManagement;


public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private Sprite[] endingSprites;
    public bool IsGameOver { get; private set; }

    [SerializeField] private GameObject pcObjects;
    [SerializeField] private GameObject vrObjects;
    [SerializeField] private bool isNotOculus = true;
    public bool IsNotOculus { get => isNotOculus; private set { isNotOculus = value; } }

    private void Awake()
    {
        IsGameOver = false;

        //if(Application.platform == RuntimePlatform.Android)
        //{
        //    IsNotOculus = true;
        //    vrObjects.SetActive(true);
        //    pcObjects.SetActive(false);
        //}
        //else
        //{
        //    IsNotOculus = false;
        //    vrObjects.SetActive(false);
        //    pcObjects.SetActive(true);
        //}
    }

    public void SetEnding(EndingList endingType)
    {
        if(endingType == EndingList.NoEnding)
        {
            return;
        }

        IsGameOver = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        int endingNumber = (int)endingType;
        PlayerPrefsKey.SetEndingList(endingNumber);
        UIManager.Instance.SetEndingImage(endingSprites[endingNumber]);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
