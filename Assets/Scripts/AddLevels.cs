using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddLevels : MonoBehaviour
{
    public void UpdateUnlockedLevels()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        for(int i = 0; i < 12; i++)
        {
            if(sceneName == "Level " + i)
            {
                LevelTracker.unlockedLevelMax = i;
            }
        }
    }
}
