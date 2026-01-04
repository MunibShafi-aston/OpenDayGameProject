using UnityEngine;

public class enemyBurn : MonoBehaviour
{
    private Enemy enemy;

    private int stacks;
    private int maxStacks;
    private float damagePerSecond;
    private float duration;
    private float timer;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void ApplyBurn(float dps, float dur, int maxStacks)
    {
        this.maxStacks = maxStacks;
        this.damagePerSecond = dps;
        this.duration = dur;

        stacks = Mathf.Min(stacks + 1, maxStacks);
        timer = duration;
    }

    void Update()
    {
        if (stacks <= 0) return;

        timer -= Time.deltaTime;

        float damage = damagePerSecond * stacks * Time.deltaTime;
        enemy.TankDamage(damage);

        if (timer <= 0f)
        {
            stacks = 0;
            Destroy(this);
        }
    }
}
