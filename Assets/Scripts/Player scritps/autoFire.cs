using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class autoFire : MonoBehaviour
{
    private bool autoFireEnabled = false;

    private float fireTimer;

    [SerializeField] private TMP_Text autoFireText;

    private PlayerController attackScript;
    private PlayerStats playerStats;

    void Awake()
    {
        attackScript = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (!autoFireEnabled)
            return;

        if (attackScript == null)
            return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            attackScript.Shoot();
            fireTimer = playerStats.attackSpeed;
        }
    }

    public void OnAutofire()
    {
            autoFireEnabled = !autoFireEnabled;
            Debug.Log("Auto Fire: " + (autoFireEnabled ? "ON" : "OFF"));
            UpdateUI();
    }

    public void UpdateUI()
    {
        if(autoFireText != null)
        {
            autoFireText.text = "Auto fire:" + (autoFireEnabled ? "ON":"OFF");
        }
    }
}