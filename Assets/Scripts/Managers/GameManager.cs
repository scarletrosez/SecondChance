using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Game Progress
    public int currentDay = 0;  // 0 = Prologue, 1-4 = Days
    public bool playedCasinoMinigame = false;  // Keeps track of minigames per day
    public bool playedRepairShopMinigame = false;

    // Player Stats
    public float mentalHealth = 100f;
    public float reputation = 50f;
    public float money = 0f;

    // Variables for thresholds affecting ending
    public float mentalHealthThreshold = 50f;
    public float reputationThreshold = 60f;
    public float moneyThreshold = 1000f;

    // Method to ensure only one GameManager
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Prevent duplicates
        }
    }

    // Call this to advance to the next day
    public void AdvanceDay()
    {
        if (currentDay < 4)  // Stop at Day 4
        {
            currentDay++;
            playedCasinoMinigame = false;
            playedRepairShopMinigame = false;
            SceneManager.LoadScene("Day" + currentDay);  // Load the appropriate day scene
        }
        else
        {
            DetermineEnding();  // If it's Day 4, determine the ending
        }
    }

    // Called when the player plays the casino minigame
    public void PlayCasinoMinigame(bool win)
    {
        if (!playedCasinoMinigame && !playedRepairShopMinigame)
        {
            playedCasinoMinigame = true;
            if (win)
            {
                // Winning in the casino increases bars more
                mentalHealth += 20f;
                reputation += 15f;
                money += 500f;
            }
            else
            {
                // Losing decreases bars significantly
                mentalHealth -= 30f;
                reputation -= 10f;
                money -= 100f;
            }
            CheckBarLimits();  // Ensure the bars don’t exceed limits
        }
    }

    // Called when the player plays the phone repair shop minigame
    public void PlayRepairShopMinigame()
    {
        if (!playedCasinoMinigame && !playedRepairShopMinigame)
        {
            playedRepairShopMinigame = true;
            // Safe, small positive impact on bars
            mentalHealth += 5f;
            reputation += 10f;
            money += 200f;
            CheckBarLimits();
        }
    }

    // Save progress when Carlos sleeps
    public void SaveProgress()
    {
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetFloat("MentalHealth", mentalHealth);
        PlayerPrefs.SetFloat("Reputation", reputation);
        PlayerPrefs.SetFloat("Money", money);
        PlayerPrefs.Save();
    }

    // Load progress
    public void LoadProgress()
    {
        currentDay = PlayerPrefs.GetInt("CurrentDay", 0);
        mentalHealth = PlayerPrefs.GetFloat("MentalHealth", 100f);
        reputation = PlayerPrefs.GetFloat("Reputation", 50f);
        money = PlayerPrefs.GetFloat("Money", 0f);
    }

    // Ensure values stay within proper limits
    private void CheckBarLimits()
    {
        mentalHealth = Mathf.Clamp(mentalHealth, 0f, 100f);
        reputation = Mathf.Clamp(reputation, 0f, 100f);
        money = Mathf.Max(0f, money);  // Money can’t go below 0
    }

    // Determine the game’s ending based on the bars
    private void DetermineEnding()
    {
        if (mentalHealth >= mentalHealthThreshold && reputation >= reputationThreshold && money >= moneyThreshold)
        {
            // Good ending
            SceneManager.LoadScene("GoodEndingScene");
        }
        else
        {
            // Bad ending
            SceneManager.LoadScene("BadEndingScene");
        }
    }

    // Optionally reset progress for a new game
    public void ResetGame()
    {
        currentDay = 0;
        mentalHealth = 100f;
        reputation = 50f;
        money = 0f;
        playedCasinoMinigame = false;
        playedRepairShopMinigame = false;
        PlayerPrefs.DeleteAll();  // Clear saved progress
        SceneManager.LoadScene("Prologue");
    }
}
