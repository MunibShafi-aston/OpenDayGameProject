using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpAmount = 3;

    [Header("Magnet settings")]
    public float magnetRadius = 3f;
    public float magnetSpeed = 5f;
    public float absorbDistance = 0.2f;

    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        Vector2 direction = (playerTransform.position - transform.position);
        float distance = direction.magnitude;

        if (distance <= magnetRadius)
        {
            transform.position += (Vector3)(direction.normalized * magnetSpeed * Time.deltaTime);

            if (distance <= absorbDistance)
            {
                PlayerStats stats = playerTransform.GetComponent<PlayerStats>();
                if (stats != null)
                    stats.addXP(xpAmount);

                Destroy(gameObject);
            }
        }
    }
}
