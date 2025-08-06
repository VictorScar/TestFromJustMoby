using JustMobyTest.Data;

namespace JustMobyTest.Configs
{
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
}