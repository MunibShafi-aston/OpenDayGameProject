using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerInput playerInput;
    private abilityHolder holder;

void Awake()
{
    playerInput = FindFirstObjectByType<PlayerInput>();
    holder = FindFirstObjectByType<abilityHolder>();

    if (playerInput == null)
        Debug.LogError("No PlayerInput found in scene!");

    if (holder == null)
        Debug.LogError("No abilityHolder found in scene!");
}


    void OnEnable()
    {
        playerInput.actions["Dash"].performed += OnDash;
        playerInput.actions["Ability1"].performed += OnAbility1;
        playerInput.actions["Ability2"].performed += OnAbility2;
        playerInput.actions["Ability3"].performed += OnAbility3;
    }

    void OnDisable()
    {
        playerInput.actions["Dash"].performed -= OnDash;
        playerInput.actions["Ability1"].performed -= OnAbility1;
        playerInput.actions["Ability2"].performed -= OnAbility2;
        playerInput.actions["Ability3"].performed -= OnAbility3;
    }

    void OnDash(InputAction.CallbackContext context)
    {
        holder.OnDash();
    }

    void OnAbility1(InputAction.CallbackContext context)
    {
        holder.OnAbility1();
    }

    void OnAbility2(InputAction.CallbackContext context)
    {
        holder.OnAbility2();
    }

    void OnAbility3(InputAction.CallbackContext context)
    {
        holder.OnAbility3();
    }
}
