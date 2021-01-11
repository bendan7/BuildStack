using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMannger : MonoBehaviour
{
    public GameObject NewGameCanvas;
    public GameObject ScoreCanvas;
    public static int Score = 0;
    private bool isGameRun = false;

    private CubesSpawner[] _spawners;
    private int _activeSpawnerIndex = 0;

    
    private int cubesCounter = 0;
    internal static event Action<int> OnScoreUpdate = delegate { };

    private void Start()
    {
        _spawners = FindObjectsOfType<CubesSpawner>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (!isGameRun)
            {
                ScoreCanvas.SetActive(true);
                NewGameCanvas.SetActive(false);
                isGameRun = true;
                SpawneCube();
                return;
            }


            bool? isNewCubeSpawn = SpawneCube();
            if (isNewCubeSpawn == true)
            {
                cubesCounter += 1;
                Score += cubesCounter;
                OnScoreUpdate(Score);

            }


        }
    }

    private bool? SpawneCube()
    {
        var isCubeStopInValidPositon =  MovingCube.CurrentCube?.Stop();

        _spawners[_activeSpawnerIndex].SpawnCube();
        _activeSpawnerIndex = (_activeSpawnerIndex + 1) % _spawners.Length;

        return isCubeStopInValidPositon;
    }

    static internal void GameEnd()
    {
        SceneManager.LoadScene(0);
    }
}
