using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    Player plScr;
    Text levelText;
    void Start()
    {
        plScr = StageManager.Ins.PlScr;
        levelText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "LV" + plScr.Level.ToString();
    }
}
