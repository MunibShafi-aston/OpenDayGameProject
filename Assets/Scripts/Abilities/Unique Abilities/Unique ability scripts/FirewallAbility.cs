using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/Unique/Firewall")]
public class FirewallAbility : ability
{
    public GameObject fireSegmentPrefab;

    public int segmentCount = 6;
    public float segmentSpacing = 0.5f;

    public float segmentDuration = 4f;
    public float damage = 2f;
    public float tickRate = 1f;

    public float spawnDistance = 5f; // distance from player

    public override void Activate(GameObject parent)
    {
        if (fireSegmentPrefab == null)
        {
            Debug.LogError("Firewall prefab not assigned!");
            return;
        }

        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
        mouseWorld.z = 0;

        Vector3 direction = (mouseWorld - parent.transform.position).normalized;

        Vector3 wallCenter = parent.transform.position + direction * spawnDistance;

        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);

        Vector3 startPos =
            wallCenter - perpendicular * (segmentCount * segmentSpacing * 0.5f);

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 spawnPos = startPos + perpendicular * (i * segmentSpacing);

            GameObject fire = Instantiate(
                fireSegmentPrefab,
                spawnPos,
                Quaternion.identity
            );

            FirewallSegment segment = fire.GetComponent<FirewallSegment>();

            if (segment != null)
            {
                segment.Setup(segmentDuration, damage, tickRate);
            }
        }
    }
}
