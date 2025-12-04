using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/RangedSlash")]
public class rangedSlashAbility : ability
{
    public GameObject rangedSlashProjectile;
    public float speed = 10f;
    public float lifetime = 0.6f;
    public float damage = 3f;

  
    public override void Activate(GameObject parent)
    {
        PlayerController movement = parent.GetComponent<PlayerController>();
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();

        Vector2 direction = movement.movementInput.normalized;

        if (direction.sqrMagnitude < 0.01f)
        {
            direction = sr.flipX ? Vector2.left :Vector2.right;
        }

        GameObject slash = Instantiate(rangedSlashProjectile,parent.transform.position, Quaternion.identity);

        rangedSlashProjectile proj = slash.GetComponent<rangedSlashProjectile>();
        proj.Setup(direction,speed,damage,lifetime, sr.flipX);
    }
}
