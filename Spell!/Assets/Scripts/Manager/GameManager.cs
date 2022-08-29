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

    private void Awake()
    {
        IsGameOver = false;
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
