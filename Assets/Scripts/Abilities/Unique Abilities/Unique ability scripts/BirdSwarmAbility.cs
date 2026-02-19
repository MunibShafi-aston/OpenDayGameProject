using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Unique/Bird Swarm")]
public class BirdSwarmAbility : ability
{
    public GameObject birdPrefab;
    public int birdCount = 3;
    public float duration = 5f;
    public float bonusDamage = 0f;

    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();

        for (int i = 0; i < birdCount; i++)
        {
            GameObject bird = Instantiate(birdPrefab, parent.transform.position, Quaternion.identity);

            BirdProjectile proj = bird.GetComponent<BirdProjectile>();
            proj.Setup(parent.transform, stats, duration, bonusDamage);
        }
    }
}
