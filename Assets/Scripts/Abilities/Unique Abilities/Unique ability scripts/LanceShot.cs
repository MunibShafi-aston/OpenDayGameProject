using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/Unique/Lance Shot")]
public class LanceShot : ability
{
    public GameObject lancePrefab;
    public float speed = 10f;
    public float abilDamage = 0f;
    public float duration = 3f;
    public float tickRate = 0.5f;
    public override void Activate(GameObject parent)
    {
        PlayerStats stats = parent.GetComponent<PlayerStats>();

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorld.z = 0;

        Vector3 direction = (mouseWorld - parent.transform.position).normalized;

        GameObject lance = Instantiate(
            lancePrefab,
            parent.transform.position + direction * 0.6f,
            Quaternion.identity
        );

        LanceProjectile proj = lance.GetComponent<LanceProjectile>();
        proj.Setup(direction, speed, abilDamage, duration, tickRate);
        
        soundManager.Instance.PlaySFX("LanceAbility");

    }
}
