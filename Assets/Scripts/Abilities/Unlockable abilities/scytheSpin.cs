using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Auto/Scythe Spin")]
public class scytheSpin : ability
{
    public GameObject scythePrefab; 
    public float radius = 1.5f;    
    public float rotationSpeed = 180f; 
    public float damage = 2f;       

    public override void Tick(float deltaTime, GameObject parent, int stacks)
    {
        bool alreadyExists = false;
        foreach (Transform child in parent.transform)
        {
            if (child.GetComponent<scytheSpinProjectile>() != null)
            {
                alreadyExists = true;
                break;
            }
        }

        if (alreadyExists) return;

        if (scythePrefab == null)
        {
            Debug.LogError("Scythe prefab not assigned!");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            float angle = i * 180f; 
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;

            GameObject scythe = Object.Instantiate(scythePrefab, parent.transform.position + offset, Quaternion.identity, parent.transform);
            scythe.transform.localScale = Vector3.one;

            scytheSpinProjectile proj = scythe.GetComponent<scytheSpinProjectile>();
            if (proj != null)
                proj.Setup(parent, radius, rotationSpeed, damage, angle);

            Debug.Log("Spawned scythe: " + scythe.name);
        }
    }
}
