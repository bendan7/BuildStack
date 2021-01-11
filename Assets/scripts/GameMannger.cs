using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMannger : MonoBehaviour
{

    private CubesSpawner[] _spawners;
    private int _activeSpawnerIndex = 0;
    internal static event Action OnCubeSpawned = delegate { };

    private void Start()
    {
        _spawners = FindObjectsOfType<CubesSpawner>();

        SpawneCube();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            SpawneCube();
            OnCubeSpawned();

        }
    }

    private void SpawneCube()
    {
        MovingCube.CurrentCube?.Stop();



        _spawners[_activeSpawnerIndex].SpawnCube();
        _activeSpawnerIndex = (_activeSpawnerIndex + 1) % _spawners.Length;
    }

    static internal void GameEnd()
    {
        SceneManager.LoadScene(0);
    }
}
