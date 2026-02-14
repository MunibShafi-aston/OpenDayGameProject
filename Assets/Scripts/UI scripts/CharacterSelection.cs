using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection Instance;
    private CharacterData selectedCharacter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SelectCharacter(CharacterData character)
    {
        selectedCharacter = character;
        Debug.Log("Selected character: " + character.characterName);
    }

    public CharacterData GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public bool HasSelectedCharacter()
    {
        return selectedCharacter != null;
    }

    public void OnSelectPressed()
    {
        CharacterSelection.Instance.SelectCharacter(selectedCharacter);
    }
    
}