using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Classes;

namespace Tetris
{
    public partial class PlayingSpace : Form
    {
        #region Variables 

        private TetrisGame _game;
        private bool _okToMove;
        private bool _gameOver;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// The constructor for the form
        /// </summary>
        public PlayingSpace()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _okToMove = false;
        }

        #endregion Constructor 

        #region Methods

        /// <summary>
        /// Finds Which Bitmap to return based on the TypeOfBlock
        /// </summary>
        /// <param name="Block"></param>
        /// <returns>A Bitmap of the Block</returns>
        private Bitmap FindWhichPictureToLoad(TypeOfBlock Block)
        {
            Bitmap tmp = null;
            switch (Block)
            {
                case TypeOfBlock.LeftL:
                    tmp = Properties.Resources.LeftL;
                    break;
                case TypeOfBlock.LeftZ:
                    tmp = Properties.Resources.LeftZ;
                    break;
                case TypeOfBlock.RightL:
                    tmp = Properties.Resources.RightL;
                    break;
                case TypeOfBlock.RightZ:
                    tmp = Properties.Resources.RightZ;
                    break;
                case TypeOfBlock.Square:
                    tmp = Properties.Resources.Square;
                    break;
                case TypeOfBlock.Straight:
                    tmp = Properties.Resources.Straight;
                    break;
                case TypeOfBlock.TBlock:
                    tmp = Properties.Resources.TBlock;
                    break;
            }
            return tmp;
        }

        #endregion Methods

        #region Events

        #region Game Events

        /// <summary>
        /// This will fire when the Game is over
        /// </summary>
        private void Game_GameOver()
        {
            _gameOver = true;
        }

        /// <summary>
        /// This will fire when the user is going to hold a piece
        /// Updates the picture box with the right image
        /// </summary>
        /// <param name="HeldBlock"></param>
        private void Game_LoadHeldPicture(TypeOfBlock HeldBlock)
        {
            Bitmap tmp = FindWhichPictureToLoad(HeldBlock);
            if (tmp != null)
                heldBlockPb.Image = tmp;
        }

        /// <summary>
        /// The Tetris class will fire this event when the piece is done moving
        /// Allowing the form to input another key press event
        /// </summary>
        private void Game_PieceOkToMove()
        {
            _okToMove = true;
        }

        /// <summary>
        /// This will find the third Preview block and put it in its rightful place
        /// </summary>
        /// <param name="PreviewBlock"></param>
        private void Game_LoadThirdPreviewPicture(TypeOfBlock PreviewBlock)
        {
            Bitmap tmp = FindWhichPictureToLoad(PreviewBlock);
            if (tmp != null)
                previewThreePb.Image = tmp;
        }

        /// <summary>
        /// This will find the Second Preview block and put it in its rightful place
        /// </summary>
        /// <param name="PreviewBlock"></param>
        private void Game_LoadSecondPreviewPicture(TypeOfBlock PreviewBlock)
        {
            Bitmap tmp = FindWhichPictureToLoad(PreviewBlock);
            if (tmp != null)
                previewTwoPb.Image = tmp;
        }

        /// <summary>
        /// This will find the First Preview block and put it in its rightful place
        /// </summary>
        /// <param name="PreviewBlock"></param>
        private void Game_LoadFirstPreviewPicture(TypeOfBlock PreviewBlock)
        {
            Bitmap tmp = FindWhichPictureToLoad(PreviewBlock);
            if (tmp != null)
                previewOnePb.Image = tmp;
        }

        #endregion Game Events

        #region Form Events

        /// <summary>
        /// Handles the Form Load Event
        /// Loads the Tetris class and hooks up the events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _game = new TetrisGame(this);
            _game.PieceOkToMove += Game_PieceOkToMove;
            _game.LoadFirstPreviewPicture += Game_LoadFirstPreviewPicture;
            _game.LoadSecondPreviewPicture += Game_LoadSecondPreviewPicture;
            _game.LoadThirdPreviewPicture += Game_LoadThirdPreviewPicture;
            _game.LoadHeldPicture += Game_LoadHeldPicture;
            _game.GameOver += Game_GameOver;
            _okToMove = true;
            _gameOver = false;
        }

        /// <summary>
        /// Handles the updating of the scores, levels and lines.  Also checks to see if the game is over
        /// I didn't want to do this but I kept getting a Cross Thread Operation when I tried other things 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScoresTimer_Tick(object sender, EventArgs e)
        {
            scoreLbl.Text = _game.TotalScore.ToString();
            levelLbl.Text = _game.CurrentLevel.ToString();
            lineLbl.Text = _game.TotalNumberOfLinesCleared.ToString();
            //If the game is over then Close down the playing space
            if (_gameOver)
            {
                _game.StopGame();
                Close();
            }
        }

        /// <summary>
        /// Handles the Key Down Event For the game.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Form1_KeyPressAsync(object sender, KeyPressEventArgs e)
        {
            if (_okToMove)
            {
                _okToMove = false;
                //Casting the 'V' into Keys makes it in F7
                if (e.KeyChar == 'v')
                    await _game.MovePieceAsync(Keys.V);
                //Casting the 'c' into Keys makes it into numpad3
                else if (e.KeyChar == 'c')
                    await _game.MovePieceAsync(Keys.C);
                else
                   await _game.MovePieceAsync((Keys)e.KeyChar);
            }
        }

        /// <summary>
        /// Handles The Arrows keys for the Key press Event.  
        /// Only the arrow keys don't work for the Key Press Event
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Down || keyData == Keys.Up)
            {
                KeyPressEventArgs e = new KeyPressEventArgs((char)keyData);
                Form1_KeyPressAsync(null, e);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Stops the Theme Song From Playing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayingSpace_FormClosing(object sender, FormClosingEventArgs e)
        {
            _game.StopGame();
            _game.Dispose();
        }

        #endregion Form Events

        #endregion Events 
    }
}