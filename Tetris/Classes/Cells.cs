using System.Drawing;
using System.Windows.Forms;

namespace Tetris.Classes
{
    /// <summary>
    /// This Class Holds the GRID POSTION and colour.  The Settings will be set in grid
    /// </summary>
    class Cells : PictureBox
    {
        #region Properties

        /// <summary>
        /// X location
        /// </summary>
        public int CellLocationX { get; set; }
        /// <summary>
        /// Y location
        /// </summary>
        public int CellLocationY { get; set; }
        /// <summary>
        /// Holds the Color of the Blocks
        /// </summary>
        public static Color CellBackColor { get; internal set; }
        /// <summary>
        /// Checks to see if the block is locked
        /// </summary>
        public bool Filled { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Creates a new Cell 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Cells(int x, int y)
        {
            CellLocationY = y;
            CellLocationX = x;
            BorderStyle = BorderStyle.FixedSingle;
            //TODO Set this to the GRID COLOR
            CellBackColor = Properties.Settings.Default.Grid_Colour;
            BackColor = CellBackColor;
        }

        #endregion Constructor
    }
}
