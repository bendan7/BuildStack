using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdate : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        GameMannger.OnScoreUpdate += OnScoreUpdate;
    }


    void OnScoreUpdate(int newScore)
    {
        _text.text = newScore.ToString();
    }

    private void OnDestroy()
    {
        GameMannger.OnScoreUpdate -= OnScoreUpdate;
    }
}
