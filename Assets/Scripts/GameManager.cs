using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    private bool isGameStarted = false;
    private PlayerMotor motor;

    // UI and the UI fields
    public Animator gameCanvas, menuAnim, diamondAnim;
    public Text scoreText, coinText, modifierText, hiscoreText;
    public float score, coinScore, modifierScore;
    private int lastScore;

    // Death menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    // Timer
    public GameObject Timer;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        scoreText.text = score.ToString("0");

        hiscoreText.text = PlayerPrefs.GetInt("Hiscore").ToString();
        coinScore = PlayerPrefs.GetFloat("coins");
        
    }

    private void Update()
    {
        Timer.SetActive(true);
        PlayerPrefs.SetFloat("coins", coinScore);
        PlayerPrefs.Save();
        coinScore = PlayerPrefs.GetFloat("coins");
        coinText.text = coinScore.ToString("0");
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<GlacierSpawner>().IsScrolling = true;
            FindObjectOfType<CamerMotor>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");
        }

        if(isGameStarted && !IsDead)
        {
            // Bump the score up
            lastScore = (int)score;
            score += (Time.deltaTime * modifierScore);
            if(lastScore != (int)score)
            {
                Debug.Log(lastScore);
                scoreText.text = score.ToString("0");
            }
            
        }
    }

    public void GetCoin()
    {
        diamondAnim.SetTrigger("Collect");
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");
    }

    public void CollectDailyReward()
    {
        coinScore += 100;
        FindObjectOfType<Timer>().collectRewardBtn.gameObject.SetActive(false);
        FindObjectOfType<Timer>().totalTime = 24 * 3600;
        FindObjectOfType<Timer>().enabled = false;
        FindObjectOfType<Timer>().enabled = true;
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        PlayerPrefs.SetFloat("carryTime", FindObjectOfType<Timer>().currentTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnDeath()
    {
        IsDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deadScoreText.text = score.ToString("0");
        deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        // Play ad
        Invoke("ShowAdClone", 2f);
        

        // Check if this is a highscore 
        if(score > PlayerPrefs.GetInt("Hiscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s += 1;
            PlayerPrefs.SetInt("Hiscore", (int)s);
        }
    }

    private void ShowAdClone()
    {
        FindObjectOfType<InterstitialAdExample>().ShowAd();
    }
}
