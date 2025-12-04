using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/AOEAttack")]
public class AOEability : ability
{
    public GameObject aoePrefab;
    public float radius = 1.5f;
    public float damage = 4f;
    public float duration = 0.3f;

    public override void Activate(GameObject parent)
    {
        SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();

        Vector3 spawnPos = parent.transform.position + (sr.flipX ? Vector3.left : Vector3.right) * 0.5f;

        GameObject aoe = Instantiate(aoePrefab, spawnPos, Quaternion.identity);

        AOEAttack aoeAttack = aoe.GetComponent<AOEAttack>();
        aoeAttack.Setup(radius,damage,duration);
    }


}
