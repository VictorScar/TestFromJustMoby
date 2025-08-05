using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICubeValidator
{
    bool Validate(CubeData verifiableCube, CubeData previousCube, out FailureReason failureReason);
}