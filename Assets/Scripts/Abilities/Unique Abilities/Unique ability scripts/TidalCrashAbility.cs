using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/Unique/Water Wave")]
public class TidalCrashAbility : ability
{
    public GameObject wavePrefab;

    public float speed = 6f;
    public float lifetime = 2f;
    public float damage = 3f;
    public float pushForce = 4f;

    public override void Activate(GameObject parent)
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
        mouseWorld.z = 0;

        Vector3 direction = (mouseWorld - parent.transform.position).normalized;

        GameObject wave = Instantiate(
            wavePrefab,
            parent.transform.position + direction * 0.5f,
            Quaternion.identity
        );

        TidalCrash waveScript = wave.GetComponent<TidalCrash>();
        waveScript.Setup(direction, speed, damage, pushForce, lifetime);
    }
}
