using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Unique/Bow Pistol")]
public class BowPistolAbility : ability
{
    public GameObject bowPistolPrefab;

    public float radius = 2f;

    public int arrowsPerBurst = 3;
    public float arrowDelay = 0.25f;

    public float burstInterval = 3f;
    public float lifetime = 20f;

    public float baseDamage = 5f;

    public override void Activate(GameObject parent)
    {
        soundManager.Instance.PlaySFX("BowPistolSummon");
        GameObject pistol = Instantiate(
            bowPistolPrefab,
            parent.transform.position,
            Quaternion.identity
        );

        BowPistolController controller = pistol.GetComponent<BowPistolController>();

        controller.Setup(
            parent.transform,
            radius,
            arrowsPerBurst,
            arrowDelay,
            burstInterval,
            lifetime,
            baseDamage
        );
    }
}
