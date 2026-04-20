using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class autoFire : MonoBehaviour
{
    private bool autoFireEnabled = false;

    [SerializeField] private TMP_Text autoFireText;

    private PlayerController attackScript;
    private PlayerStats playerStats;

    public bool IsAutoFireEnabled => autoFireEnabled;


    void Awake()
    {
        attackScript = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (!autoFireEnabled || attackScript == null)
            return;

        if (attackScript.fireTimer <= 0f)
        {
            attackScript.animator.SetTrigger("isAttk");
            attackScript.Shoot();

            attackScript.fireTimer =
                attackScript.fireRate / Mathf.Max(0.01f, playerStats.attackSpeed);
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