using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CubesConfigData
{
    public CubeConfig[] CubeConfigs;

    public bool TryGetData(TowerCubeType cubeType, out CubeConfig cubeConfig)
    {
        if (CubeConfigs != null)
        {
            foreach (var config in CubeConfigs)
            {
                if (config.CubeType == cubeType)
                {
                    cubeConfig = config;
                    return true;
                }
            }
        }

        cubeConfig = new CubeConfig();
        return false;
    }
}