using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class LevelController : MonoBehaviour
{
    public int levelUpSecPace; // how offen we increase level

    public Text level;
    public Text timer;

    int currentLevel;
    float timerSec;

    CultureInfo enUS = new CultureInfo("en-US");

    int currentLevelUpSec;

    public delegate void OnLevelUp(int level);

    public event OnLevelUp onLevelUp;

    public int CurrentLevel => currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        ValidateText();
        currentLevelUpSec = levelUpSecPace;
    }

    // Update is called once per frame
    void Update()
    {
        timerSec += Time.deltaTime;
        timer.text = string.Format(enUS, "{0:00.0}", timerSec);

        int sec = (int)timerSec;

        if(sec > 0 && sec == currentLevelUpSec)
        {
            currentLevelUpSec += levelUpSecPace;
            ++currentLevel;
            ValidateText();
            onLevelUp?.Invoke(currentLevel);
        }
    }

    void ValidateText()
    {
        level.text = string.Format(enUS, "{0}", currentLevel);
    }

}
