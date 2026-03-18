using UnityEngine;
using UnityEngine.InputSystem;

public class autoFire : MonoBehaviour
{
    private bool autoFireEnabled = false;

    public float fireRate = 1f;
    private float fireTimer;

    private PlayerController attackScript;

    void Awake()
    {
        attackScript = GetComponent<PlayerController>();
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
            fireTimer = fireRate;
        }
    }

    public void OnAutofire()
    {
            autoFireEnabled = !autoFireEnabled;
            Debug.Log("Auto Fire: " + (autoFireEnabled ? "ON" : "OFF"));
    }
}