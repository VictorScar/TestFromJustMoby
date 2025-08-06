using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower
{
    [SerializeField] private List<TowerCube> cubesInTower = new List<TowerCube>();
   
    private ICubeValidator[] _validators;
    public event Action<List<TowerCube>> onDataUpdated;

    public Tower(ICubeValidator[] validators)
    {
        _validators = validators;
    }

    public bool TryAddCube(CubeData cubeData, out TowerCube cube, out FailureReason failureReason)
    {
        if (ValidateAddition(cubeData, out failureReason))
        {
            cube = AddCube(cubeData.CubeType, cubeData.Position.x);
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
            Height = cubesInTower.Count
        };
        cubesInTower.Add(cube);
        onDataUpdated?.Invoke(cubesInTower);

        return cube;
    }

    public bool ValidateAddition(CubeData cubeData, out FailureReason failureReason, bool isNewCube = true)
    {
        if (cubesInTower.Count > 0)
        {
            foreach (var validator in _validators)
            {
                var previousCubeIndex = (int)cubeData.Position.y - 1;

                if (isNewCube || previousCubeIndex > cubesInTower.Count - 1)
                {
                    previousCubeIndex = cubesInTower.Count - 1;
                }

                if (!validator.Validate(cubeData, cubesInTower[previousCubeIndex].CubeData, out failureReason))
                {
                    return false;
                }
            }
        }

        failureReason = FailureReason.None;
        return true;
    }

    public void RemoveCube(TowerCube cube)
    {
        var cubeHeight = cube.Height;

        for (int i = cubeHeight; i < cubesInTower.Count; i++)
        {
            cubesInTower[i].Height--;
        }

        cubesInTower.Remove(cube);
        onDataUpdated?.Invoke(cubesInTower);
    }
}