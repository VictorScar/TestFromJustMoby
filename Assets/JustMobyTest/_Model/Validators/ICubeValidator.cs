namespace JustMobyTest._Model.Validators
{
    public interface ICubeValidator
    {
        bool Validate(CubeData verifiableCube, CubeData previousCube, out FailureReason failureReason);
    }
}