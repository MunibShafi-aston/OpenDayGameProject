using UnityEngine;

public class ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    [TextArea(2,4)]
    public string description;
    public Sprite icon;

    public virtual void Activate(GameObject parent)
    {
        
    }
    public virtual void Tick(float deltaTime, GameObject parent, int stacks)
    {
        
    }
}
