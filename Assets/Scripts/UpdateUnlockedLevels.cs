using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateUnlockedLevels : MonoBehaviour
{
    public int maxLevelUnlocked;
    [SerializeField]
    public Button[] levels;


    private void Start()
    {
        foreach (var button in levels)
        {
            button.enabled = false;
        }

        for (int i = 0; i < LevelTracker.unlockedLevelMax; i++)
        {
            levels[i].enabled = true;
        }
    }
}
