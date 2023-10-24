using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public float totalTime = 24 * 3600; // 24 hours in seconds
    public float currentTime;
    private float lastUpdateTime;
    public int initialStart = 0;
    public Text countdownText;
    public GameObject collectRewardBtn;
    public bool gameStarted = false;

    private void Start()
    {
        PlayerPrefs.SetInt("initialStart", initialStart);
        if(initialStart == 0)
        {
            collectRewardBtn.SetActive(false);
            initialStart = 1;
            totalTime = 24 * 3600;
            this.GetComponent<Timer>().enabled = false;
            this.GetComponent<Timer>().enabled = true;
            PlayerPrefs.SetInt("initialStart", initialStart);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        if (currentTime <= 0.0f)
        {
            this.GetComponent<Timer>().enabled = false;
            this.GetComponent<Timer>().enabled = true;
            currentTime = PlayerPrefs.GetFloat("SavedTime", totalTime);
        }
    }

    [MenuItem("Custom/Start Timer")]
    public static void StartTimer()
    {
        // Create a GameObject with the script attached to the start timer.
        GameObject timerObject = new GameObject("Timer");
        timerObject.AddComponent<Timer>();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetFloat("carryTime", currentTime) <= 0.0f)
        {
            currentTime = PlayerPrefs.GetFloat("SavedTime", totalTime);
        }
        else if(PlayerPrefs.GetFloat("carryTime", currentTime) > 0.0f)
        {
            currentTime = PlayerPrefs.GetFloat("carryTime", currentTime);
        }
        
        EditorApplication.update += UpdateTimer;
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        EditorApplication.update -= UpdateTimer;
        PlayerPrefs.SetFloat("SavedTime", totalTime);
        PlayerPrefs.Save();
    }


    private void UpdateTimer()
    {
        if(currentTime > 0)
        {
            currentTime -= (float)EditorApplication.timeSinceStartup - lastUpdateTime;
            lastUpdateTime = (float)EditorApplication.timeSinceStartup;
            //Debug.Log("Time Remaining: " + FormatTime(currentTime));
            countdownText.text = FormatTime(currentTime).ToString();
            //Debug.Log(PlayerPrefs.GetFloat("carryTime", currentTime));
        }
        else
        {
            //Debug.Log("Time's up");
            collectRewardBtn.SetActive(true);
        }
    }

    private string FormatTime(float time)
    {
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}
