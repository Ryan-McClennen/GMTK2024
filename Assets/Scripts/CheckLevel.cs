using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevel : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    TextMeshProUGUI text;

    void Start()
    {
        int levelNum =  int.Parse(text.text);
        if (levelNum > PlayerPrefs.GetInt("MaxLevel"))
            button.enabled = false;
    }
}
