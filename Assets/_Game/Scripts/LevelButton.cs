using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] public Button levelButton;
    [SerializeField] private TextMeshProUGUI levelText;

    public void SetData(int id)
    {
        levelButton.onClick.AddListener(() =>
        {
            LevelButtonOnClick(id);
            MainMenuUI.Instance.CloseLevelMenu();
        });
    }
    public void LevelButtonOnClick(int id)
    {
        GameObject mapPrefab = Resources.Load<GameObject>($"{LevelName.LEVELSTRING}{id}");
        GameObject currentLevel = Instantiate(mapPrefab);
        Player.Instance.TurnOnGravity();
    }

    public void SetButtonText(string text)
    {
        levelText.text = text;
    }

    

}


