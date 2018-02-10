using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Tetris.Classes
{
    class Grid : PictureBox
    {
        #region Private Variables

        private Cells[,] _cells;
        private Form _form;
        private bool _allowBlockToBeHeld;

        #endregion Private Variables

        #region Delegates & Events

        /// <summary>
        /// Invokes When the Pieces hit over the Screen making the game to end.
        /// </summary>
        public delegate void LineOverFlowHandler();
        public event LineOverFlowHandler Overflow;
        /// <summary>
        /// Invokes when the grid is finished moving the piece.
        /// </summary>
        public delegate void PieceDoneMovingHandler();
        public event PieceDoneMovingHandler DoneMoving;
        /// <summary>
        /// Invokes when the grid starts doing its thing for pieces.
        /// </summary>
        public delegate void PieceStartedMovingHandler();
        public event PieceStartedMovingHandler StartedMoving;
        /// <summary>
        /// Invokes when the piece is Locked into place and its okay to send a new piece.
        /// </summary>
        public delegate void PieceIsLockedHandler();
        public event PieceIsLockedHandler PieceLocked;
        /// <summary>
        /// Invokes when you Hold a piece, and its okay to send a new piece.
        /// </summary>
        public delegate void PieceIsHeldHandler();
        public event PieceIsHeldHandler PieceHeld;
        /// <summary>
        /// Invokes when you clear lines in the grid.
        /// </summary>
        /// <param name="numberOfLines">The number of lines being cleared.</param>
        public delegate void LineClearedHandler(int numberOfLines);
        public event LineClearedHandler LineCleared;
        /// <summary>
        /// Invokes when you hard drop a piece.
        /// </summary>
        /// <param name="howManyLines">The number of lines that it dropped</param>
        public delegate void HardDropHandler(int howManyLines);
        public event HardDropHandler HardDrop;

        #endregion Delegates & Events

        #region Properties

        /// <summary>
        /// This is the Falling Block of the Grid
        /// </summary>
        public FallingBlock BlockThatsFalling { get; internal set; }
        /// <summary>
        /// This is the Block that is Held
        /// </summary>
        public TypeOfBlock BlockThatIsHeld { get; set; }

        #endregion Properties 

        #region Constructor

        /// <summary>
        /// The Constructor Making the Grid and the Cells that are inside of it
        /// </summary>
        /// <param name="form"></param>
        public Grid(Form form)
        {
            _form = form;
            //Set the Hold Block to NULL
            BlockThatIsHeld = TypeOfBlock.Null;
            //Allow the user to Hold a Block at the start of the game
            _allowBlockToBeHeld = true;
            //Setting up Number of Cells
            _cells = new Cells[Properties.Settings.Default.Number_Of_Columns, Properties.Settings.Default.Number_Of_Rows];
            //Setting up a grid to hold all the Cells
            Left = Properties.Settings.Default.Left_Grid_Margin;
            Top = Properties.Settings.Default.Top_Grid_Margin;
            Width = Properties.Settings.Default.Number_Of_Columns * Properties.Settings.Default.Cell_Width;
            Height = Properties.Settings.Default.Number_Of_Rows * Properties.Settings.Default.Cell_Height;
            form.Controls.Add(this);
            //Setting Up the Cells and assigning them an location
            int ColumnAdjuster = Left;
            int RowAdjuster = Top;
            for (int i = 0; i < Properties.Settings.Default.Number_Of_Columns; i++)
            {
                for (int j = 0; j < Properties.Settings.Default.Number_Of_Rows; j++)
                {
                    _cells[i, j] = new Cells(i, j)
                    {
                        Width = Properties.Settings.Default.Cell_Width,
                        Height = Properties.Settings.Default.Cell_Height,
                        Left = ColumnAdjuster + (i * Properties.Settings.Default.Cell_Width),
                        Top = RowAdjuster + (j * Properties.Settings.Default.Cell_Height)
                    };
                    form.Controls.Add(_cells[i, j]);
                }
            }
        }

        #endregion Constructor 

        #region Public Methods

        /// <summary>
        /// Creates the New Falling Block, for the Grid to Handle
        /// </summary>
        /// <param name="blockType"></param>
        public void CreateFallingBlock(TypeOfBlock blockType)
        {
            BlockThatsFalling = new FallingBlock(blockType);
            //Checks to see if the Blocks are that the top of the screen
            foreach (var item in BlockThatsFalling.BlockCells)
            {
                if (_cells[item.CellLocationX, item.CellLocationY].Filled)
                {
                    Overflow?.Invoke();
                    return;
                }
                _cells[item.CellLocationX, item.CellLocationY].BackColor = item.BackColor;
            }
        }

        /// <summary>
        /// Moves the Falling Block
        /// </summary>
        public void MoveFallingBlock()
        {
            //Checks to see if you need to lock the Piece based on the map size, or checking to see the other pieces
            foreach (var item in BlockThatsFalling.BlockCells)
            {
                if (item.CellLocationY + 1 == Properties.Settings.Default.Bottom_Of_Play_Area)
                {
                    LockCells();
                    return;
                }
                if (_cells[item.CellLocationX, item.CellLocationY + 1].Filled || item.CellLocationY == Properties.Settings.Default.Bottom_Of_Play_Area)
                {
                    LockCells();
                    return;
                }
            }
            //Draws out the Old blocks and Colours in the New Block
            List<Cells> oldCells = GetOldCellsData();
            foreach (var @new in BlockThatsFalling.BlockCells)
            {
                @new.CellLocationY++;
            }
            DrawOutTheBlocks(oldCells);
        }

        /// <summary>
        /// Moves the Falling Block based on the Key Pressed
        /// </summary>
        /// <param name="keyPressed">The Key that was pressed on the KeyBoard</param>
        public void MovePiece(Keys keyPressed)
        {
            List<Cells> oldCells = null;
            try
            {
                //Tell the game that we are starting to move the Piece and it should surpress any and all Key Events
                StartedMoving?.Invoke();
                //No need if the space bar is pressed.  
                if (keyPressed != Keys.Space)
                    oldCells = GetOldCellsData();
                switch (keyPressed)
                {
                    case Keys.Right:
                        if (CheckRightSideForBlocks())
                            BlockThatsFalling.MoveRight();
                        break;
                    case Keys.Left:
                        if (CheckLeftSideForBlocksAndWalls())
                            BlockThatsFalling.MoveLeft();
                        break;
                    case Keys.Up:
                        if (IsItOkayToMove())
                        {
                            BlockThatsFalling.RotateRight();
                            CheckRotateRight();
                        }
                        break;
                    case Keys.Down:
                        if (IsItOkayToMove())
                        {
                            BlockThatsFalling.RotateLeft();
                            CheckRotateLeft();
                        }
                        break;
                    case Keys.Space:
                        if (BlockThatsFalling.IsItFalling)
                            DropBlockToTheBottom();
                        break;
                    case Keys.V:
                        MoveFallingBlock();
                        break;
                    case Keys.C:
                        if (_allowBlockToBeHeld)
                        {
                            _allowBlockToBeHeld = false;
                            HoldFallingBlock();
                        }
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (keyPressed != Keys.Space)
                {
                    DrawOutTheBlocks(oldCells);
                }
                //Signal the game that the piece is done moving
                DoneMoving?.Invoke();
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Locks The falling Block into place
        /// </summary>
        private void LockCells()
        {
            //Sets all the blocks Filled Property to true
            foreach (var locked in BlockThatsFalling.BlockCells)
            {
                _cells[locked.CellLocationX, locked.CellLocationY].Filled = true;
            }
            //Set the Grids propertys to false and reset 
            BlockThatsFalling.IsItFalling = false;
            _allowBlockToBeHeld = true;
            //Check for Line Clear and Invoke the Piece Locked event for the Tetris Class
            CheckForLineClear();
            PieceLocked?.Invoke();
        }

        /// <summary>
        /// Takes the Current Falling Block, and stores all of the Cells in a List
        /// </summary>
        /// <returns>A list Of Cells that contian all the data from the current falling block</returns>
        private List<Cells> GetOldCellsData()
        {
            List<Cells> tmp = new List<Cells>();
            //I Had A problem with cloning the list, easiest way was to just create new blocks.
            foreach (var item in BlockThatsFalling.BlockCells)
            {
                tmp.Add(new Cells(item.CellLocationX, item.CellLocationY));
            }
            return tmp;
        }

        /// <summary>
        /// This method checks the Walls of the Grid and checks for blocks when moving the Falling Block
        /// </summary>
        /// <returns>True, if the Piece can Move.  False, if not </returns>
        private bool CheckLeftSideForBlocksAndWalls()
        {

            foreach (var item in BlockThatsFalling.BlockCells)
            {
                if (item.CellLocationX == Properties.Settings.Default.Left_Of_Play_Area || _cells[item.CellLocationX - 1, item.CellLocationY].Filled)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// This method checks the Walls of the Grid and checks for blocks when moving the Falling Block
        /// </summary>
        /// <returns>True, if the Piece can Move.  False, if not </returns>
        private bool CheckRightSideForBlocks()
        {

            foreach (var item in BlockThatsFalling.BlockCells)
            {
                if (item.CellLocationX == Properties.Settings.Default.Right_Of_Play_Area)
                {
                    return false;
                }
                if (_cells[item.CellLocationX + 1, item.CellLocationY].Filled)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Takes the Old Cells and colors them into the Grid Color, takes the current Falling Block and colors it in the Correct Colour Needed
        /// </summary>
        /// <param name="oldCells">The Old Cells from the Current Falling block before it moves</param>
        private void DrawOutTheBlocks(List<Cells> oldCells)
        {
            //Color Out the Old Block to the grid colour
            if (oldCells != null)
                foreach (var old in oldCells)
                    _cells[old.CellLocationX, old.CellLocationY].BackColor = Properties.Settings.Default.Grid_Colour;
            //Color in the new Block
            foreach (var @new in BlockThatsFalling.BlockCells)
                _cells[@new.CellLocationX, @new.CellLocationY].BackColor = @new.BackColor;
        }

        /// <summary>
        /// Takes the Old Cells and colors them into the Grid Color, takes the current Falling Block and colors it in the Correct Colour Needed
        /// </summary>
        /// <param name="oldCells">The Old Cells from the Current Falling block before it moves</param>
        /// <param name="newCells">The New Cells from the Current Falling block after it moves</param>
        private void DrawOutTheBlocks(List<Cells> oldCells, List<Cells> newCells)
        {
            //Color Out the Old Block to the grid colour
            foreach (var old in oldCells)
                _cells[old.CellLocationX, old.CellLocationY].BackColor = Properties.Settings.Default.Grid_Colour;
            //Color In the New Cells
            foreach (var @new in newCells)
            {
                _cells[@new.CellLocationX, @new.CellLocationY].BackColor = @new.BackColor;
                _cells[@new.CellLocationX, @new.CellLocationY].Filled = @new.Filled;
                _cells[@new.CellLocationX, @new.CellLocationY].CellLocationX = @new.CellLocationX;
                _cells[@new.CellLocationX, @new.CellLocationY].CellLocationY = @new.CellLocationY;
            }
        }

        /// <summary>
        /// Checks to see if the Falling Block has reached the bottom
        /// </summary>
        /// <returns>True if not, False if it hit the bottom</returns>
        private bool IsItOkayToMove()
        {
            foreach (var item in BlockThatsFalling.BlockCells)
                if (item.CellLocationY + 1 >= Properties.Settings.Default.Bottom_Of_Play_Area)
                    return false;
            return true;
        }

        /// <summary>
        /// Drops the Current Falling Block to the Bottom 
        /// </summary>
        private void DropBlockToTheBottom()
        {
            //Keeps track of the number of lines it moved, for scoring purposes
            int numberOfLinesItHadToMove = 0;
            do
            {
                MoveFallingBlock();
                numberOfLinesItHadToMove++;
            } while (BlockThatsFalling.IsItFalling);
            HardDrop?.Invoke(numberOfLinesItHadToMove);
        }

        /// <summary>
        /// Checks to see if the Current Falling Block Can Rotate the Block Left
        /// </summary>
        /// <returns>True, it did rotate.  False, if it didn't</returns>
        private bool CheckRotateLeft()
        {
            //Flag if the Block needs to be Updated after rotating back
            bool updateBlock = false;
            //If the Block is hitting blocks then rotate it back Right (CW)
            foreach (var item in BlockThatsFalling.BlockCells)
            {
                if (_cells[item.CellLocationX, item.CellLocationY].Filled == true)
                {
                    BlockThatsFalling.RotateRight();
                    updateBlock = true;
                    break;
                }
            }
            if (updateBlock)
            {
                //Updates the block CellLocation Y 
                foreach (var item in BlockThatsFalling.BlockCells)
                {
                    if (_cells[item.CellLocationX, item.CellLocationY].Filled == true)
                    {
                        foreach (var cell in BlockThatsFalling.BlockCells)
                            cell.CellLocationY++;
                        break;
                    }
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to see if the Current Falling Block Can Rotate the Block Right
        /// </summary>
        /// <returns>True, it did rotate.  False, if it didn't</returns>
        private bool CheckRotateRight()
        {
            //If the Block is hitting blocks then rotate it back Left (CCW)
            foreach (var item in BlockThatsFalling.BlockCells)
            {
                if (_cells[item.CellLocationX, item.CellLocationY].Filled == true)
                {
                    BlockThatsFalling.RotateLeft();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Takes the Current Falling Block and puts it into a Hold state
        /// </summary>
        private void HoldFallingBlock()
        {
            if (BlockThatIsHeld == TypeOfBlock.Null)
            {
                //Set the held block value
                BlockThatIsHeld = BlockThatsFalling.BlockType;
                //Set it to false so that the timer tick will fix it 
                BlockThatsFalling.IsItFalling = false;
                //Draw out the old block
                if (BlockThatsFalling.BlockCells != null)
                {
                    foreach (var old in BlockThatsFalling.BlockCells)
                        _cells[old.CellLocationX, old.CellLocationY].BackColor = Properties.Settings.Default.Grid_Colour;
                }
                //Creates a new falling Block
                CreateFallingBlock(RandomBlock.PullABlock());
            }
            else
            {
                //Get the HoldBlock 
                var oldHeldBlock = BlockThatIsHeld;
                //Set the Block that is falling to held
                BlockThatIsHeld = BlockThatsFalling.BlockType;
                //Draw out the old block
                if (BlockThatsFalling.BlockCells != null)
                {
                    foreach (var old in BlockThatsFalling.BlockCells)
                    {
                        _cells[old.CellLocationX, old.CellLocationY].BackColor = Properties.Settings.Default.Grid_Colour;
                    }
                }
                CreateFallingBlock(oldHeldBlock);
            }
            //Event for the Form to Handle, Fixes the Timer Tick
            PieceHeld?.Invoke();
        }

        /// <summary>
        /// Checks To See if you need to Clear Line(s)
        /// </summary>
        private void CheckForLineClear()
        {
            //Check to see if lines need to be cleared
            List<int> linesThatNeedToBeCleared = GetNumberOfLinesThatAreFill();
            if (linesThatNeedToBeCleared.Count > 0)
            {
                //Tell the game how many lines that are being cleared
                LineCleared?.Invoke(linesThatNeedToBeCleared.Count);
                //Draw out the falling block
                DrawOutTheBlocks(null);
                foreach (var item in linesThatNeedToBeCleared)
                    for (int x = 0; x <= Properties.Settings.Default.Right_Of_Play_Area; x++)
                        _cells[x, item].BackColor = Properties.Settings.Default.Line_Clear_Color;
                //Put the Game to Sleep to Show the User the lines being Cleared
                Thread.Sleep(250);
                //Clear out the lines
                ClearOutTheLines(linesThatNeedToBeCleared);
            }
        }

        /// <summary>
        /// Clears out all the Lines that need to be clear and moves all the blocks down to make a "Falling Effect"
        /// </summary>
        /// <param name="linesThatNeedToBeCleared"></param>
        private void ClearOutTheLines(List<int> linesThatNeedToBeCleared)
        {
            foreach (var item in linesThatNeedToBeCleared)
            {
                List<Cells> oldCells = new List<Cells>();
                List<Cells> newCells = new List<Cells>();
                for (int y = item - 1; y >= 0; y--)
                {
                    for (int x = 0; x <= Properties.Settings.Default.Right_Of_Play_Area; x++)
                    {
                        Cells tmp = new Cells(x, y)
                        {
                            BackColor = _cells[x, y].BackColor
                        };
                        oldCells.Add(tmp);
                        Cells tmpAgain = new Cells(x, y + 1)
                        {
                            BackColor = _cells[x, y].BackColor,
                            Filled = _cells[x, y].Filled
                        };
                        newCells.Add(tmpAgain);
                    }
                }
                DrawOutTheBlocks(oldCells, newCells);
            }
        }

        /// <summary>
        /// Gets the All the Lines that Are Full
        /// </summary>
        /// <returns>The Index of all the lines that are all full</returns>
        private List<int> GetNumberOfLinesThatAreFill()
        {
            List<int> linesThatNeedToBeCleared = new List<int>();
            for (int y = 0; y <= Properties.Settings.Default.Top_Of_Play_Area; y++)
            {
                bool lineFilled = true;
                for (int x = 0; x <= Properties.Settings.Default.Right_Of_Play_Area; x++)
                {
                    if (_cells[x, y].Filled == false)
                        lineFilled = false;
                }
                if (lineFilled == true)
                    linesThatNeedToBeCleared.Add(y);
            }
            return linesThatNeedToBeCleared;
        }

        #endregion Private Methods
    }
}