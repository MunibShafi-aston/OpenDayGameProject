using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Auto/Fireball")]
public class fireball : ability
{
    public GameObject fireballPrefab;
    public float damage = 3f;
    public float interval = 1f;

    float timer;

    public override void Tick(float deltaTime, GameObject parent)
    {
        timer += deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            Activate(parent);
        }
    }

    public override void Activate(GameObject parent)
    {
        Instantiate(fireballPrefab, parent.transform.position, Quaternion.identity);
    }

}
