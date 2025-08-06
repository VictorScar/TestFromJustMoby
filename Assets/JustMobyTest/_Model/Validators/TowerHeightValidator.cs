using JustMobyTest._Model;
using JustMobyTest._Model.Validators;


public class TowerHeightValidator : ICubeValidator
{
    private float _cubeHeight;
    private float _towerFieldHeight;
    private FailureReason _failureReason = FailureReason.TowerHeightToLarge;

    public TowerHeightValidator(float cubeHeight, float towerFieldHeight)
    {
        _cubeHeight = cubeHeight;
        _towerFieldHeight = towerFieldHeight;
    }

    public bool Validate(CubeData verifiableCube, CubeData previousCube, out FailureReason failureReason)
    {
        if ((previousCube.Position.y * _cubeHeight + _cubeHeight) < _towerFieldHeight)
        {
            failureReason = FailureReason.None;
            return true;
        }
        else
        {
            failureReason = _failureReason;
            return false;
        }
    }
}