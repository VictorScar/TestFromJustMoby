using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower
{
    [SerializeField] private List<TowerCube> cubesInTower = new List<TowerCube>();
    private Vector2 _cubeSize;

    private delegate bool ValidationDelegate(CubeData data, out string reason);

    private List<ValidationDelegate> _validators = new List<ValidationDelegate>();
    public event Action<TowerCube> onCubeAdded;
    public event Action<int> onCubeRemoved;

    public Tower(Vector2 cubeSize)
    {
        _cubeSize = cubeSize;

        _validators.Add((CubeData d, out string reason) =>
        {
            if (Mathf.Abs(cubesInTower[cubesInTower.Count - 1].XPos - d.Position.x) <= 0.5f * _cubeSize.x)
            {
                reason = "";
                return true;
            }
            else
            {
                reason = "position X is out of range";
                return false;
            }
            //return Mathf.Abs(cubesInTower[cubesInTower.Count - 1].XPos - d.Position.x) <= 0.5f * _cubeSize.x;
        });
        _validators.Add((CubeData d, out string reason) =>
        {
            if (d.Position.y >= (cubesInTower.Count) * cubeSize.y - 0.1f * cubeSize.y)
            {
                reason = "";
                return true;
            }
            else
            {
                reason = "Pos Y is too low";
                return false;
            }
            // return d.Position.y >= (cubesInTower.Count) * cubeSize.y - 0.1f * cubeSize.y;
        });
        //_validators.Add((d) => cubesInTower[cubesInTower.Count - 1].CubeType == d.CubeType);
    }

    public bool TryAddCube(CubeData cubeData, out TowerCube cube, out string failtureReason)
    {
        if (ValidateAddition(cubeData, out failtureReason))
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
        onCubeAdded?.Invoke(cube);

        return cube;
    }

    private bool ValidateAddition(CubeData cubeData, out string failtureReason)
    {
        if (cubesInTower.Count > 0)
        {
            foreach (var validator in _validators)
            {
                if (!validator(cubeData, out failtureReason))
                {
                    return false;
                }
            }
            
        }

        failtureReason = "";
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