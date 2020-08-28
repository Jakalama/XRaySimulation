using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Transform menuTransform;
    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        Pause();

        menuTransform = this.gameObject.transform.Find("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pause();
    }

    public void StartButton()
    {
        Debug.Log("Start");
    }

    public void ExitButton()
    {
        Debug.Log("Exit");

        Application.Quit();
    }

    private void Pause()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        menuTransform.gameObject.SetActive(isPaused);
    }
}
