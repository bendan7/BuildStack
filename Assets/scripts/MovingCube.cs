using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{

    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; internal set; }

    [SerializeField]
    private float _moveSpeed = 1f;

    private Color _cubeColor;



    private void OnEnable()
    {
        if(LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
            return;
        }

        CurrentCube = this;
        _cubeColor = GetRandomColor();
        GetComponent<Renderer>().material.color = _cubeColor;

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        _moveSpeed = 0;
        float hangOver = GetHangOver();

        if (IsGameEnd(hangOver))
        {
            return;
        }

        float direction = hangOver >= 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.Z)
        {
            SplitCubeOnZ(hangOver, direction);

        }
        else if (MoveDirection == MoveDirection.X)
        {
            SplitCubeOnX(hangOver, direction);
        }
        LastCube = this;

    }

    private bool IsGameEnd(float hangOver)
    {
        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;
        if (Math.Abs(hangOver) > max)
        {
            CurrentCube = null;
            LastCube = null;
            GameMannger.GameEnd();
            return true;
        }

        return false;
    }

    private float GetHangOver()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else if (MoveDirection == MoveDirection.X)
        {
            return transform.position.x - LastCube.transform.position.x;
        }

        Debug.LogError("GetHangOver errer");
        return 0;
            
    }

    private void SplitCubeOnZ(float hangOver, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Math.Abs(hangOver);
        float fallingBlockSize = transform.localScale.z - newZSize;
        float newZPosition = LastCube.transform.position.z + (hangOver / 2);

        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, newZSize);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2 * direction);

       

        float fallingBlackPosition = fallingBlockSize/2 * direction + cubeEdge;

        SpawnDropCube(fallingBlackPosition, fallingBlockSize);
    }

    private void SplitCubeOnX(float hangOver, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Math.Abs(hangOver);
        float fallingBlockSize = transform.localScale.x - newXSize;
        float newXPosition = LastCube.transform.position.x + (hangOver / 2);

        this.transform.localScale = new Vector3(newXSize, this.transform.localScale.y, this.transform.localScale.z);
        this.transform.position = new Vector3(newXPosition, this.transform.position.y, this.transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2 * direction);

        float fallingBlackPosition = fallingBlockSize / 2 * direction + cubeEdge;

        SpawnDropCube(fallingBlackPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if(MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
        }
        else if (MoveDirection == MoveDirection.X)
        {
            cube.transform.localScale = new Vector3(fallingBlockSize,transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z );
        }

        cube.GetComponent<Renderer>().material.color = _cubeColor;
        cube.AddComponent<Rigidbody>();
        Destroy(cube, 1f);

    }

    private void FixedUpdate()
    {
        if(MoveDirection == MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * _moveSpeed;
        }
        else if(MoveDirection == MoveDirection.X)
        {
            transform.position += transform.right * Time.deltaTime * _moveSpeed;
        }
    }


}
