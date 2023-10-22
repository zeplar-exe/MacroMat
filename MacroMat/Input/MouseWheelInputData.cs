using System.Numerics;

namespace MacroMat.Input;

public class MouseWheelInputData
{
    public int DeltaVertical { get; }
    public int DeltaHorizontal { get; }
    public float DeltaMagnitude => MathF.Sqrt(MathF.Pow(DeltaHorizontal, 2) + MathF.Pow(DeltaVertical, 2));

    public MouseWheelInputData(int deltaVertical, int deltaHorizontal)
    {
        DeltaVertical = deltaVertical;
        DeltaHorizontal = deltaHorizontal;
    }
}