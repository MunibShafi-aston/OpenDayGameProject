using UnityEngine;

public class HawkMarkFollow : MonoBehaviour
{
    Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = target.position;
    }
}
