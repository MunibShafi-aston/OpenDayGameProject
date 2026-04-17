using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/Unique/RangedSlash")]
public class rangedSlashAbility : ability
{
    public GameObject rangedSlashProjectile;
    public float speed = 10f;
    public float lifetime = 0.6f;
    public float damage = 3f;

    soundManager SoundManager;
    public override void Activate(GameObject parent)
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
        mouseWorld.z = 0;

        Vector3 direction = (mouseWorld - parent.transform.position).normalized;

        GameObject slash = Instantiate(
            rangedSlashProjectile,
            parent.transform.position,
            Quaternion.identity
        );

        rangedSlashProjectile proj = slash.GetComponent<rangedSlashProjectile>();

        proj.Setup(direction, speed, damage, lifetime);
        soundManager.Instance.PlaySFX("WolfAbil1");

    }
}
