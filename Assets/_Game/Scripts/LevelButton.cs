using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private LevelData leveDataID;

    public void OnEnable()
    {
        levelButton.onClick.AddListener(LevelButtonOnClick);
    }

    public void LevelButtonOnClick()
    {
        GameObject mapPrefab = Resources.Load<GameObject>("Level1");
        GameObject currentLevel = Instantiate(mapPrefab);
        Debug.Log($"Id cua level buttonclick: ");
    }
}
