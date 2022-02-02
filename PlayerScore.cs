using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;
    private int combo = 0;
    public int comboMultiplier = 10;
    public float comboTime = 3f;
    private float comboTimer = 0f, timer = 0f;
    private string scoreOutput;
    public TextMeshProUGUI scoreInterface;
    public TextMeshProUGUI comboInterface;

    void Start()
    {
        comboInterface.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            comboInterface.gameObject.SetActive(true);
        }
        else if (combo!=0)
        {
            score += combo * comboMultiplier;
            combo = 0;
            comboInterface.gameObject.SetActive(false);
        }
        scoreOutput = string.Format("SCORE: {0}\nTIME: {1:f2}", score, timer);
        scoreInterface.SetText(scoreOutput);
        comboInterface.SetText("COMBO! x" + combo);
    }
    public void plusCombo(int points)
    {
        score += points;        //adding score
        ++combo;
        comboTimer = comboTime; //reseting combo timer
    }
}
