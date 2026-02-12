using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterCardUI : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text statsText;

    private CharacterData characterData;

    public void SetData(CharacterData data)
    {
        characterData = data;
        characterImage.sprite = data.characterSprite;
        statsText.text = data.GetStatsString();
    }

public void OnSelectPressed()
{
    CharacterSelectManager.Instance.SelectCharacter(characterData);
}
}
