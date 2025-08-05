using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPositionValidator : ICubeValidator
{
    private float _cubeWidth;
    private float _widthLimit = 0.5f;
    private FailureReason _failureReason = FailureReason.XOffsetToLarge;
    
    public XPositionValidator(float cubeWidth)
    {
        _cubeWidth = cubeWidth;
    }
    
    public bool Validate(CubeData verifiableCube, CubeData previousCube, out FailureReason failureReason)
    {
        if (Mathf.Abs(verifiableCube.Position.x - previousCube.Position.x) <= _widthLimit * _cubeWidth)
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
