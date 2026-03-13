using UnityEngine;

public class DamagePopupSpawner : MonoBehaviour


{
    public static DamagePopupSpawner Instance;
    public GameObject damagePopupPrefab;

    public Transform canvas;

        void Awake()
        {
            Instance = this;
        }

        public void SpawnPopup(Vector3 worldPosition, int damage)
        {
            GameObject popup = Instantiate(damagePopupPrefab,canvas);

            popup.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
            DamagePopup popupScript = popup.GetComponent<DamagePopup>();
            popupScript.Setup(damage);
        }
}
