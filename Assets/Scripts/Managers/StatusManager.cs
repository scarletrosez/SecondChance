using UnityEngine;
using TMPro;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance;

    [Header("Status UI Elements")]
    [SerializeField] private TextMeshProUGUI reputationStatusText;
    [SerializeField] private TextMeshProUGUI mentalHealthStatusText;
    [SerializeField] private TextMeshProUGUI financeStatusText;

    private int reputationStatus = 2; // Start at "Good"
    private int mentalHealthStatus = 2; // Start at "Good"
    private int financeStatus = 2; // Start at "Good"

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        reputationStatusText.text = "Good";
        reputationStatusText.color = Color.green;
        mentalHealthStatusText.text = "Good";
        mentalHealthStatusText.color = Color.green;
        financeStatusText.text = "Good";
        financeStatusText.color = Color.green;
    }

    public void UpdateStatus(string statusType, string change)
    {
        int changeNum = 0;
        if(change == "-1")
        {
            changeNum = -1;
        }
        else if(change == "-2")
        {
            changeNum = -2;
        }
        else if(change == "+1")
        {
            changeNum = 1;
        }
        else if(change == "+2")
        {
            changeNum = 2;
        }
        switch (statusType)
        {
            case "reputation":
                reputationStatus = Mathf.Clamp(reputationStatus + changeNum, 0, 2);
                UpdateStatusText(reputationStatusText, reputationStatus);
                break;
            case "mental":
                mentalHealthStatus = Mathf.Clamp(mentalHealthStatus + changeNum, 0, 2);
                UpdateStatusText(mentalHealthStatusText, mentalHealthStatus);
                break;
            case "finance":
                financeStatus = Mathf.Clamp(financeStatus + changeNum, 0, 2);
                UpdateStatusText(financeStatusText, financeStatus);
                break;
            default:
                Debug.LogWarning("Unknown status type: " + statusType);
                break;
        }
    }

    private void UpdateStatusText(TextMeshProUGUI statusText, int statusValue)
    {
        switch (statusValue)
        {
            case 2:
                statusText.text = "Good";
                statusText.color = Color.green;
                break;
            case 1:
                statusText.text = "Endangered";
                statusText.color = Color.yellow;
                break;
            case 0:
                statusText.text = "Bad";
                statusText.color = Color.red;
                break;
            default:
                Debug.LogWarning("Invalid status value: " + statusValue);
                break;
        }
    }
}
