using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    [SerializeField] private List<TowerCube> cubesInTower = new List<TowerCube>();
    private float _xPos;

    public event Action<TowerCubeType, int> onCubeAdded;
    public event Action<int> onCubeRemoved;
    
    public void AddCube(TowerCubeType cubeType, Vector3 pos)
    {
        if (cubesInTower.Count < 1)
        {
            _xPos = pos.x;
        }
        else
        {
            ValidateAdditional(pos);
        }
        var cube = new TowerCube(cubeType);
        cubesInTower.Add(cube);
        onCubeAdded?.Invoke(cube.CubeType, cubesInTower.Count-1);
    }

    private bool ValidateAdditional(Vector3 pos)
    {
        return true;
    }

    public void RemoveCube(TowerCube cube)
    {
        cubesInTower.Remove(cube);
        onCubeRemoved?.Invoke(0);
    }
}