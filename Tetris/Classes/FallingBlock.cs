using System;
using System.Collections.Generic;

namespace Tetris.Classes
{
    /// <summary>
    /// This holds information about the Falling Block
    /// </summary>
    class FallingBlock
    {
        #region Properties

        /// <summary>
        /// Asks to see if it is Falling or being Held
        /// </summary>
        public bool IsItFalling { get; set; }
        /// <summary>
        /// Holds all the information about the CELLS in the Falling Block
        /// </summary>
        public List<Cells> BlockCells { get; set; }
        /// <summary>
        /// Holds the Type of Block
        /// </summary>
        public TypeOfBlock BlockType { get; internal set; }
        /// <summary>
        /// Holds the Rotation Point
        /// </summary>
        public RotationDegree RotationPoint { get; internal set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Creates a New Falling Block
        /// </summary>
        /// <param name="blockType">The Type of Block that is Created</param>
        public FallingBlock(TypeOfBlock blockType)
        {
            BlockCells = new List<Cells>();
            IsItFalling = true;
            BlockType = blockType;
            RotationPoint = RotationDegree.threeSixty;
            switch (blockType)
            {
                case TypeOfBlock.Straight:
                    BlockCells.Add(new Cells(5, 0));
                    BlockCells.Add(new Cells(5, 1));
                    BlockCells.Add(new Cells(5, 2));
                    BlockCells.Add(new Cells(5, 3));
                    foreach (var item in BlockCells)
                    {
                        item.BackColor = System.Drawing.Color.Cyan;
                        item.Filled = true;
                    }
                    break;
                case TypeOfBlock.LeftL:
                    BlockCells.Add(new Cells(5, 0));
                    BlockCells.Add(new Cells(5, 1));
                    BlockCells.Add(new Cells(5, 2));
                    BlockCells.Add(new Cells(6, 2));
                    foreach (var item in BlockCells)
                    {
                        item.BackColor = System.Drawing.Color.Orange;
                        item.Filled = true;
                    }
                    break;
                case TypeOfBlock.LeftZ:
                    BlockCells.Add(new Cells(5, 0));
                    BlockCells.Add(new Cells(5, 1));
                    BlockCells.Add(new Cells(6, 1));
                    BlockCells.Add(new Cells(6, 2));
                    foreach (var item in BlockCells)
                    {
                        item.BackColor = System.Drawing.Color.Lime;
                        item.Filled = true;
                    }
                    break;
                case TypeOfBlock.RightL:
                    BlockCells.Add(new Cells(5, 0));
                    BlockCells.Add(new Cells(5, 1));
                    BlockCells.Add(new Cells(5, 2));
                    BlockCells.Add(new Cells(4, 2));
                    foreach (var item in BlockCells)
                    {
                        item.BackColor = System.Drawing.Color.Blue;
                        item.Filled = true;
                    }
                    break;
                case TypeOfBlock.RightZ:
                    BlockCells.Add(new Cells(5, 0));
                    BlockCells.Add(new Cells(5, 1));
                    BlockCells.Add(new Cells(4, 1));
                    BlockCells.Add(new Cells(4, 2));
                    foreach (var item in BlockCells)
                    {
                        item.BackColor = System.Drawing.Color.Red;
                        item.Filled = true;
                    }
                    break;
                case TypeOfBlock.Square:
                    BlockCells.Add(new Cells(5, 0));
                    BlockCells.Add(new Cells(6, 0));
                    BlockCells.Add(new Cells(6, 1));
                    BlockCells.Add(new Cells(5, 1));
                    foreach (var item in BlockCells)
                    {
                        item.BackColor = System.Drawing.Color.Yellow;
                        item.Filled = true;
                    }
                    break;
                case TypeOfBlock.TBlock:
                    BlockCells.Add(new Cells(5, 0));
                    BlockCells.Add(new Cells(5, 1));
                    BlockCells.Add(new Cells(4, 1));
                    BlockCells.Add(new Cells(6, 1));
                    foreach (var item in BlockCells)
                    {
                        item.BackColor = System.Drawing.Color.Purple;
                        item.Filled = true;
                    }
                    break;
            }
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Rotates the Block 90 degrees to the Right
        /// </summary>
        internal void RotateRight()
        {
            switch (BlockType)
            {
                case TypeOfBlock.Straight:
                    switch (RotationPoint)
                    {
                        case RotationDegree.threeSixty:
                            RotateStraightPieceCW();
                            break;
                        case RotationDegree.ninety:
                            RotateStraightPieceCCW();
                            break;
                        default:
                            break;
                    }
                    break;
                case TypeOfBlock.LeftL:
                    RotateLeftLCW();
                    break;
                case TypeOfBlock.RightL:
                    RotateRightLCW();
                    break;
                case TypeOfBlock.Square:
                    //Why Would You rotate the Square............
                    break;
                case TypeOfBlock.RightZ:
                    RotateRightZCW();
                    break;
                case TypeOfBlock.LeftZ:
                    RotateLeftZCW();
                    break;
                case TypeOfBlock.TBlock:
                    RotateTCW();
                    break;
                default:
                    break;
            }
            CheckForWalls();
        }
        /// <summary>
        /// Rotates the Block 90 Degrees to the Left
        /// </summary>
        internal void RotateLeft()
        {
            switch (BlockType)
            {
                case TypeOfBlock.Straight:
                    switch (RotationPoint)
                    {
                        case RotationDegree.threeSixty:
                            RotateStraightPieceCW();
                            break;
                        case RotationDegree.ninety:
                            RotateStraightPieceCCW();
                            break;
                        default:
                            break;
                    }
                    break;
                case TypeOfBlock.LeftL:
                    RotateLeftLCCW();
                    break;
                case TypeOfBlock.RightL:
                    RotateRightLCCW();
                    break;
                case TypeOfBlock.Square:
                    //Pretend to Spin......
                    break;
                case TypeOfBlock.RightZ:
                    RotateRightZCCW();
                    break;
                case TypeOfBlock.LeftZ:
                    RotateLeftZCCW();
                    break;
                case TypeOfBlock.TBlock:
                    RotateTCCW();
                    break;
                default:
                    break;
            }
            CheckForWalls();
        }
        /// <summary>
        /// Moves the Blocks to the Left Block
        /// </summary>
        /// <returns>True, if It moved.  False, if it didn't</returns>
        internal bool MoveLeft()
        {
            //Find the Most Left Cell
            const int LEFT_SIDE = -1;
            const int MOVE_DISTANCE = 1;
            int mostLeft = 9;
            Cells mostLeftCell = null;

            foreach (var item in BlockCells)
            {
                if (item.CellLocationX <= mostLeft)
                {
                    mostLeft = item.CellLocationX;
                    mostLeftCell = item;
                }
            }
            //See if I can move right else do nothing
            if (mostLeftCell.CellLocationX - MOVE_DISTANCE > LEFT_SIDE)
            {
                BlockCells.ForEach(x => x.CellLocationX--);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Moves the Blocks to the Right one Block
        /// </summary>
        /// <returns>True, if It moved.  False, if it didn't</returns>
        internal bool MoveRight()
        {
            //Find the Most Right Cell
            const int RIGHT_SIDE = 10;
            const int MOVE_DISTANCE = 1;
            int mostRight = 0;
            Cells mostRightCell = null;

            foreach (var item in BlockCells)
            {
                if (item.CellLocationX >= mostRight)
                {
                    mostRight = item.CellLocationX;
                    mostRightCell = item;
                }
            }
            //See if I can move right else do nothing
            if (mostRightCell.CellLocationX + MOVE_DISTANCE < RIGHT_SIDE)
            {
                BlockCells.ForEach(x => x.CellLocationX++);
                return true;
            }
            return false;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Rotates the "T" block Clockwise 90 degrees
        /// </summary>
        private void RotateTCW()
        {
            //The Second block in the list is the pivot block
            int pivotY = BlockCells[1].CellLocationY;
            int pivotX = BlockCells[1].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 1;

                    BlockCells[2].CellLocationX = pivotX - 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 1;

                    BlockCells[2].CellLocationX = pivotX + 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the T block CounterClockwise 90 degrees
        /// </summary>
        private void RotateTCCW()
        {
            //The Second block in the list is the pivot block
            int pivotY = BlockCells[1].CellLocationY;
            int pivotX = BlockCells[1].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 1;

                    BlockCells[2].CellLocationX = pivotX - 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 1;

                    BlockCells[2].CellLocationX = pivotX + 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the right "Z" block Clockwise 90 degrees
        /// </summary>
        private void RotateRightZCW()
        {
            //The Second block in the list is the pivot block
            int pivotY = BlockCells[1].CellLocationY;
            int pivotX = BlockCells[1].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 1;

                    BlockCells[2].CellLocationX = pivotX + 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 1;

                    BlockCells[2].CellLocationX = pivotX - 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the right "Z" block CounterClockwise 90 degrees
        /// </summary>
        private void RotateRightZCCW()
        {
            //The Second block in the list is the pivot block
            int pivotY = BlockCells[1].CellLocationY;
            int pivotX = BlockCells[1].CellLocationX;
            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 1;

                    BlockCells[2].CellLocationX = pivotX + 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 1;

                    BlockCells[2].CellLocationX = pivotX - 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the left "Z" block Clockwise 90 degrees
        /// </summary>
        private void RotateLeftZCW()
        {
            //The Second block in the list is the pivot block
            int pivotY = BlockCells[1].CellLocationY;
            int pivotX = BlockCells[1].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 1;

                    BlockCells[2].CellLocationX = pivotX - 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 1;

                    BlockCells[2].CellLocationX = pivotX + 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the left "Z" block CounterClockwise 90 degrees
        /// </summary>
        private void RotateLeftZCCW()
        {
            //The Second block in the list is the pivot block
            int pivotY = BlockCells[1].CellLocationY;
            int pivotX = BlockCells[1].CellLocationX;
            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 1;

                    BlockCells[2].CellLocationX = pivotX - 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 1;

                    BlockCells[2].CellLocationX = pivotX + 1;
                    BlockCells[2].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 1;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[2].CellLocationX = pivotX;
                    BlockCells[2].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the right "L" block Clockwise 90 degrees
        /// </summary>
        private void RotateRightLCW()
        {
            //The Third block in the list is the pivot block
            int pivotY = BlockCells[2].CellLocationY;
            int pivotX = BlockCells[2].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX - 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX + 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the left "L" block Clockwise 90 degrees
        /// </summary>
        private void RotateLeftLCW()
        {
            //The Third block in the list is the pivot block
            int pivotY = BlockCells[2].CellLocationY;
            int pivotX = BlockCells[2].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX - 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX - 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX + 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX + 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Rotates the right "L" block CounterClockwise 90 degrees
        /// </summary>
        private void RotateRightLCCW()
        {
            //The Third block in the list is the pivot block
            int pivotY = BlockCells[2].CellLocationY;
            int pivotX = BlockCells[2].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX + 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX + 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX - 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX - 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the left "L" block CounterClockwise 90 degrees
        /// </summary>
        private void RotateLeftLCCW()
        {
            //The Third block in the list is the pivot block
            int pivotY = BlockCells[2].CellLocationY;
            int pivotX = BlockCells[2].CellLocationX;

            switch (RotationPoint)
            {
                case RotationDegree.ninety:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY - 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY - 1;

                    BlockCells[3].CellLocationX = pivotX + 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.oneEighty;
                    break;
                case RotationDegree.oneEighty:
                    BlockCells[0].CellLocationX = pivotX + 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX + 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY + 1;

                    RotationPoint = RotationDegree.twoSeventy;
                    break;
                case RotationDegree.twoSeventy:
                    BlockCells[0].CellLocationX = pivotX;
                    BlockCells[0].CellLocationY = pivotY + 2;

                    BlockCells[1].CellLocationX = pivotX;
                    BlockCells[1].CellLocationY = pivotY + 1;

                    BlockCells[3].CellLocationX = pivotX - 1;
                    BlockCells[3].CellLocationY = pivotY;

                    RotationPoint = RotationDegree.threeSixty;
                    break;
                case RotationDegree.threeSixty:
                    BlockCells[0].CellLocationX = pivotX - 2;
                    BlockCells[0].CellLocationY = pivotY;

                    BlockCells[1].CellLocationX = pivotX - 1;
                    BlockCells[1].CellLocationY = pivotY;

                    BlockCells[3].CellLocationX = pivotX;
                    BlockCells[3].CellLocationY = pivotY - 1;

                    RotationPoint = RotationDegree.ninety;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rotates the straight block Clockwise 90 degrees
        /// </summary>
        private void RotateStraightPieceCW()
        {
            //All the block have the same X 
            int pivotX = BlockCells[0].CellLocationX;
            //The new highest X value is +1 away from the pivot point
            pivotX += 1;
            //Find the Highest Y value
            int pivotY = 0;
            foreach (var item in BlockCells)
            {
                if (item.CellLocationY > pivotY)
                    pivotY = item.CellLocationY;
            }
            //The pivot block is -2 away from the highest block
            pivotY -= 2;
            foreach (var item in BlockCells)
            {
                item.CellLocationX = pivotX;
                pivotX--;
                item.CellLocationY = pivotY;
            }
            RotationPoint = RotationDegree.ninety;
        }
        /// <summary>
        /// Rotates the straight block CounterClockwise 90 degrees
        /// </summary>
        private void RotateStraightPieceCCW()
        {
            //All the block have the same Y 
            int pivotY = BlockCells[0].CellLocationY;
            //The new highest Y value is +2 away from the pivot point
            pivotY += 2;
            //Find the Highest X value
            int pivotX = 0;
            foreach (var item in BlockCells)
            {
                if (item.CellLocationX > pivotX)
                    pivotX = item.CellLocationX;
            }
            //The pivot block is -1 from the pivot point
            pivotX -= 1;
            foreach (var item in BlockCells)
            {
                item.CellLocationY = pivotY;
                pivotY--;
                item.CellLocationX = pivotX;
            }
            RotationPoint = RotationDegree.threeSixty;
        }
        /// <summary>
        /// Checks for the walls and moves the rotated piece if need be
        /// </summary>
        private void CheckForWalls()
        {
            int moveDistance = 0;
            foreach (var item in BlockCells)
            {
                if (item.CellLocationX > Properties.Settings.Default.Right_Of_Play_Area)
                {
                    int tmp = 9 - item.CellLocationX;
                    if (tmp < moveDistance)
                        moveDistance = tmp;
                }

                else if (item.CellLocationX < Properties.Settings.Default.Left_Of_Play_Area)
                {
                    int tmp = Math.Abs(item.CellLocationX);
                    if (tmp > moveDistance)
                        moveDistance = tmp;
                }
            }
            foreach (var item in BlockCells)
            {
                item.CellLocationX += moveDistance;
            }
        }

        #endregion Private Methods
    }
}