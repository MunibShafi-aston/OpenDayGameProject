using UnityEngine;

public class ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent)
    {
        
    }
    public virtual void Tick(float deltaTime, GameObject parent)
    {
        
    }
}
