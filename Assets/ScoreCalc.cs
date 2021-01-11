using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalc : MonoBehaviour
{
    private int _score = 0;
    private TextMeshProUGUI _text;
    private int counter = 1;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        GameMannger.OnCubeSpawned += OnCubeSpawned;
    }


    void OnCubeSpawned()
    {
        _score += counter;
        counter++;
        _text.text = _score.ToString();
    }

    private void OnDestroy()
    {
        GameMannger.OnCubeSpawned -= OnCubeSpawned;
    }
}
