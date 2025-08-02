using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    [SerializeField] private List<TowerCube> cubesInTower = new List<TowerCube>();
    private float _xPos;
 

    public event Action<TowerCubeType, Vector2> onCubeAdded;
    public event Action<int> onCubeRemoved;

    public TowerData Data
    {
        set
        {
            var cubesData = value.CubesData;

            if (cubesData != null)
            {
                foreach (var data in cubesData)
                {
                    AddCube(data);
                }
            }
        }
    }
    
    public bool TryAddCube(TowerCubeType cubeType, Vector3 pos)
    {
        if (cubesInTower.Count < 1)
        {
            var finalCubePos = new Vector2(pos.x, 0);
            
            AddCube(new TowerCube
            {
                CubeType = cubeType,
                Position = finalCubePos
            });
            return true;
        }
        else if( ValidateAdditional(pos))
        {
            var finalCubePos = new Vector2(pos.x, cubesInTower.Count);
            AddCube(new TowerCube
            {
                CubeType = cubeType,
                Position = finalCubePos
            });
            return true;
        }

        return false;

    }

    private void AddCube(TowerCube cubeData)
    {
        cubesInTower.Add(cubeData);
        onCubeAdded?.Invoke(cubeData.CubeType, cubeData.Position);
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