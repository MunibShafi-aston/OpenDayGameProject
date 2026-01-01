using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Auto/Summon Wolf")]
public class summonWolf : ability
{
    public GameObject wolfPrefab;  
    public int summonCount = 1;
    public float followDistance = 1.5f;
    public float damage = 2f;



    public override void Tick(float deltaTime, GameObject parent)
    {
        int existingSummons = parent.GetComponentsInChildren<summonController>().Length;
        if (existingSummons > 0) return;

        if (wolfPrefab == null)
        {
            Debug.LogError("Wolf prefab not assigned!");
            return;
        }

        for (int i = 0; i < summonCount; i++)
        {
            Vector3 offset = new Vector3(i * 1.0f, 0, 0); 
            GameObject wolf = Object.Instantiate(wolfPrefab, parent.transform.position + offset, Quaternion.identity);

            summonController controller = wolf.GetComponent<summonController>();
            if (controller != null)
                controller.Setup(parent, damage, followDistance);

            wolf.transform.parent = parent.transform;

            Debug.Log("Wolf spawned: " + wolf.name);
        }
    }
}
