using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreviousScoreUpade : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if(GameMannger.Score > 0)
        {
            _text.text = "Last Score: " +GameMannger.Score.ToString();
        }
    }

}
