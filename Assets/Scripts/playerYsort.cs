using UnityEngine;

[ExecuteAlways]
public class YSort : MonoBehaviour
{
    [Tooltip("Higher = finer ordering. Typical = 100")]
    public int multiplier = 100;

    public string forceSortingLayer = ""; 
    SpriteRenderer[] srs;

    void Awake()
    {
        srs = GetComponentsInChildren<SpriteRenderer>();
        if (srs == null || srs.Length == 0)
            Debug.LogWarning($"YSort on '{name}' found no SpriteRenderer.");
    }

    void LateUpdate()
    {
        if (srs == null || srs.Length == 0) return;

        int order = Mathf.RoundToInt(-transform.position.y * multiplier);

        foreach (var sr in srs)
        {
            if (!string.IsNullOrEmpty(forceSortingLayer))
                sr.sortingLayerName = forceSortingLayer;

            sr.sortingOrder = order;
        }

#if UNITY_EDITOR
        Debug.Log($"YSort '{name}': y={transform.position.y:F3}, order={order}, layer='{srs[0].sortingLayerName}'");
#endif
    }
}
