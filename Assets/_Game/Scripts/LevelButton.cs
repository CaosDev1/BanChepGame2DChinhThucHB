using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] public Button levelButton;
    [SerializeField] private TextMeshProUGUI levelText;

    public void OnEnable()
    {
        levelButton.onClick.AddListener(() =>
        {
            LevelButtonOnClick(2);
        });
    }
    public void LevelButtonOnClick(int id)
    {
        GameObject mapPrefab = Resources.Load<GameObject>($"Level{id}");
        GameObject currentLevel = Instantiate(mapPrefab);
        Debug.Log($"Id cua level buttonclick: ");
    }

}
