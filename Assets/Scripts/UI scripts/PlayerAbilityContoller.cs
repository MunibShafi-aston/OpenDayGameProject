using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerInput playerInput;

void Awake()
{
    playerInput = FindFirstObjectByType<PlayerInput>();

    if (playerInput == null)
        Debug.LogError("No PlayerInput found in scene!");

}


    void OnEnable()
    {
        if (playerInput == null) return;
        playerInput.actions["Dash"].performed += OnDash;
        playerInput.actions["Ability1"].performed += OnAbility1;
        playerInput.actions["Ability2"].performed += OnAbility2;
        playerInput.actions["Ability3"].performed += OnAbility3;
    }

    void OnDestroy()
    {
        if (playerInput == null) return;
        playerInput.actions["Dash"].performed -= OnDash;
        playerInput.actions["Ability1"].performed -= OnAbility1;
        playerInput.actions["Ability2"].performed -= OnAbility2;
        playerInput.actions["Ability3"].performed -= OnAbility3;
    }

    void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var holder = GetAbilityHolder();
        if (holder != null && holder.isActiveAndEnabled)
            holder.OnDash();
    }

    void OnAbility1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var holder = GetAbilityHolder();
        if (holder != null && holder.isActiveAndEnabled)
        holder.OnAbility1();
    }

    void OnAbility2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var holder = GetAbilityHolder();
        if (holder != null && holder.isActiveAndEnabled)
        holder.OnAbility2();
    }

    void OnAbility3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var holder = GetAbilityHolder();
        if (holder != null && holder.isActiveAndEnabled)
        holder.OnAbility3();
    }

    abilityHolder GetAbilityHolder()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player == null) return null;

    return player.GetComponent<abilityHolder>();
}
}
