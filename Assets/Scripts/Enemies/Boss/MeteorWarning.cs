using UnityEngine;

public class MeteorWarning : MonoBehaviour
{
    public float warningDuration = 1f;

    void Start()
    {
        Destroy(gameObject, warningDuration);
    }
}
