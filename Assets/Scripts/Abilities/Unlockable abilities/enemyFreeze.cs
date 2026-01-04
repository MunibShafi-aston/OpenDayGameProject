using UnityEngine;
using System.Collections;

public class enemyFreeze : MonoBehaviour
{
    private enemyChase chase;

    void Awake()
    {
        chase = GetComponent<enemyChase>();
        if (chase == null)
            Debug.LogWarning("enemyFreeze: enemyChase component not found on " + gameObject.name);
    }


    public void ApplyFreeze(float duration)
    {
        if (chase == null) return;

        chase.Freeze(duration);

        Debug.Log($"Enemy frozen for {duration} seconds: {gameObject.name}");
    }
}
