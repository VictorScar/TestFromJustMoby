using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightCubeValidator : ICubeValidator
{
  private float _cubeHeight;
  private FailureReason _failureReason = FailureReason.PosYLow;

  public HeightCubeValidator(float cubeHeight)
  {
    _cubeHeight = cubeHeight;
  }
  
  public bool Validate(CubeData verifiableCube, CubeData previousCube, out FailureReason failureReason)
  {
    if (Mathf.Abs(verifiableCube.Position.y - previousCube.Position.y) >= _cubeHeight)
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
