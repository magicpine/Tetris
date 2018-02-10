namespace Tetris.Classes
{
    /// <summary>
    /// Holds information About The Block 
    /// </summary>
    public enum TypeOfBlock
    {
        Straight,
        LeftL,
        RightL,
        Square,
        RightZ,
        LeftZ,
        TBlock,
        Null
    }

    /// <summary>
    /// Holds information about which point in the rotation the piece is in
    /// </summary>
    public enum RotationDegree
    {
        ninety,
        oneEighty,
        twoSeventy,
        threeSixty
    }
}
