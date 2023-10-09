using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] protected LevelButton levelButtonPrefab; 
    [SerializeField] private Transform levelContent;

    

    private void Start()
    {
        for (int i = 0; i < levelData.itemLevel.Count; i++)
        {
            Debug.Log($"Id levelManager: {levelData.itemLevel[i].id}");
            
            Instantiate(levelButtonPrefab, levelContent);

        }


    }

    




}
