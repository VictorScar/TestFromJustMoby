using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower
{
    [SerializeField] private List<TowerCube> cubesInTower = new List<TowerCube>();
    private float _xPos;


    public event Action<TowerCube> onCubeAdded;
    public event Action<int> onCubeRemoved;

    /*public TowerData Data
    {
        set
        {
            var cubesData = value.CubesData;

            if (cubesData != null)
            {
                foreach (var data in cubesData)
                {
                    AddCube(data.CubeType, data.XPos);
                }
            }
        }
    }*/

    public bool TryAddCube(TowerCubeType cubeType, Vector3 pos, out TowerCube cube)
    {
        if (ValidateAdditional(cubeType, pos))
        {
            cube = AddCube(cubeType, pos.x);
            return true;
        }

        cube = null;
        return false;
    }

    private TowerCube AddCube(TowerCubeType cubeType, float xPos)
    {
        var cube = new TowerCube
        {
            CubeType = cubeType,
            XPos = xPos,
            Height =  cubesInTower.Count
        };
        cubesInTower.Add(cube);
        onCubeAdded?.Invoke(cube);

        return cube;
    }

    private bool ValidateAdditional(TowerCubeType cubeType, Vector2 pos)
    {
        if (cubesInTower.Count > 0)
        {
            
        }
        return true;
    }

    public void RemoveCube(TowerCube cube)
    {
        var cubeHeight = cube.Height;

        for (int i = cubeHeight; i < cubesInTower.Count; i++)
        {
            cubesInTower[i].Height -= 1;
        }
        cubesInTower.Remove(cube);
        
        onCubeRemoved?.Invoke(cubeHeight);
    }
}