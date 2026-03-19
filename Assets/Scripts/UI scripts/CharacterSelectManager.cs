using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager Instance;

    [Header("UI")]
    [SerializeField] private Transform cardParent;
    [SerializeField] private CharacterCardUI cardPrefab;
    [SerializeField] private CharacterDetailsUI detailsUI;
    [SerializeField] private CharacterSelect characterSelectUI;

    [Header("Character Data")]
    [SerializeField] private List<CharacterData> characters;

    void Awake()
    {
    Instance = this;
    }

    private void Start()
    {
        GenerateCards();
    }

    private void GenerateCards()
    {
        foreach (Transform child in cardParent)
            Destroy(child.gameObject);

            foreach (CharacterData character in characters)
        {
            CharacterCardUI card = Instantiate(cardPrefab, cardParent);
            card.SetData(character);
        }
        
    }

    public void SelectCharacter(CharacterData data)
    {
        detailsUI.UpdateDetails(data);
        CharacterSelection.Instance.SelectCharacter(data);
        characterSelectUI.EnablePlayButton();
    }

    public void PlayGame()
    {
        if (!CharacterSelection.Instance.HasSelectedCharacter())
        {
            Debug.LogWarning("No character selected!");
            return;
        }
        SceneManager.LoadScene("GameScene");
    }
}