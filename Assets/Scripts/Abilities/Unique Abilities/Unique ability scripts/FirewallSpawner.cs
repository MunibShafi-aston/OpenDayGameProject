using UnityEngine;

public class FirewallSpawner : MonoBehaviour
{
    public GameObject fireSegmentPrefab;

    public void Setup(
        Vector3 direction,
        int segments,
        float spacing,
        float duration,
        float burnDamage,
        float tickRate)
    {
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);

        int half = segments / 2;

        for (int i = -half; i <= half; i++)
        {
            Vector3 pos = transform.position + perpendicular * i * spacing;

            GameObject seg = Instantiate(
                fireSegmentPrefab,
                pos,
                Quaternion.identity
            );

            FirewallSegment fs = seg.GetComponent<FirewallSegment>();

            fs.Setup(duration, burnDamage, tickRate);
        }

        Destroy(gameObject);
    }
}
