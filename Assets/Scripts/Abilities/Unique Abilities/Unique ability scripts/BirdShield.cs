using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Unique/Bird Shield")]
public class BirdShieldAbility : ability
{
    public GameObject birdPrefab;
    public int birdCount = 3;
    public float orbitRadius = 1.5f;
    public float orbitSpeed = 180f;
    public float duration = 6f;

    public override void Activate(GameObject parent)
    {
        for (int i = 0; i < birdCount; i++)
        {
            float angle = (360f / birdCount) * i;

            GameObject bird = Instantiate(birdPrefab, parent.transform.position, Quaternion.identity);

            BirdShieldOrbit orbit = bird.GetComponent<BirdShieldOrbit>();
            orbit.Setup(parent.transform, orbitRadius, orbitSpeed, angle, duration);
        }
    }
}
