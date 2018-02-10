using System;
using System.Collections.Generic;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Classes;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tetris
{
    public class TetrisGame : IDisposable
    {
        #region Variables

        private Grid _grid;
        private System.Timers.Timer _timeClock;
        private bool OkToMove;
        private int _totalScore;
        private int _totalLevel;
        private int _numberOfLinesCleared;
        private Form _form;
        private SoundPlayer _themeSong;
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private bool _formAlreadyUp;

        #endregion Variables

        #region Public Properties

        /// <summary>
        /// The Total Score of the Current Game
        /// </summary>
        public int TotalScore
        {
            get { return _totalScore; }
        }
        /// <summary>
        /// The Current Level of the Game
        /// </summary>
        public int CurrentLevel
        {
            get { return _totalLevel; }
        }
        /// <summary>
        /// The Total Number of Lines Cleared in the Current Game
        /// </summary>
        public int TotalNumberOfLinesCleared
        {
            get { return _numberOfLinesCleared; }
        }

        #endregion Public Properties

        #region Delegates & Events

        /// <summary>
        /// Tells the Form that its okay to send another Key Press 
        /// </summary>
        public delegate void PieceOkToMoveHandler();
        public event PieceOkToMoveHandler PieceOkToMove;
        /// <summary>
        /// Tells the Form the Next Block to Fall
        /// </summary>
        /// <param name="PreviewBlocks">The Block that is falling Next</param>
        public delegate void LoadFirstPreviewPictureHandler(TypeOfBlock PreviewBlocks);
        public event LoadFirstPreviewPictureHandler LoadFirstPreviewPicture;
        /// <summary>
        /// Tells the Form the Second Block that will Fall Next
        /// </summary>
        /// <param name="PreviewBlocks">The Second Block that will Fall</param>
        public delegate void LoadSecondPreviewPictureHandler(TypeOfBlock PreviewBlocks);
        public event LoadSecondPreviewPictureHandler LoadSecondPreviewPicture;
        /// <summary>
        /// Tells the Form the Third Block that will Fall Next
        /// </summary>
        /// <param name="PreviewBlocks">The Third Block that will Fall</param>
        public delegate void LoadThirdPreviewPictureHandler(TypeOfBlock PreviewBlocks);
        public event LoadThirdPreviewPictureHandler LoadThirdPreviewPicture;
        /// <summary>
        /// Tells the Form which Block that will be Held
        /// </summary>
        /// <param name="HeldBlock">The Type of Block that is being held</param>
        public delegate void LoadHeldPictureHandler(TypeOfBlock HeldBlock);
        public event LoadHeldPictureHandler LoadHeldPicture;
        /// <summary>
        /// Tells the Form that the game is over
        /// </summary>
        public delegate void GameOverHandler();
        public event GameOverHandler GameOver;

        #endregion Delegates & Events

        #region Constructor

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="form">The Form that will be using this</param>
        public TetrisGame(Form form)
        {
            //Setting up the Grid
            _form = form;
            _grid = new Grid(form);
            _grid.Overflow += Grid_Overflow;
            _grid.DoneMoving += Grid_DoneMoving;
            _grid.StartedMoving += Grid_StartedMoving;
            _grid.PieceLocked += Grid_PieceLocked;
            _grid.PieceHeld += Grid_PieceHeld;
            _grid.LineCleared += Grid_LineCleared;
            _grid.HardDrop += Grid_HardDrop;
            form.Controls.Add(_grid);
            //TimeClock
            _timeClock = new System.Timers.Timer()
            {
                Enabled = true,
                Interval = Properties.Settings.Default.Game_Starting_Timer_Interval
            };
            _timeClock.Elapsed += TimeClock_Tick;
            //Reset the Game Score and Level
            _totalLevel = 0;
            _totalScore = 0;
            _numberOfLinesCleared = 0;
            //Get the Theme song playing :)
            _themeSong = new SoundPlayer();
            _themeSong.Stream = Properties.Resources.theme;
            _themeSong.PlayLooping();
            //Async is messing up the highscores form
            _formAlreadyUp = false;
        }

        #endregion Constructor

        #region Grid Events

        /// <summary>
        /// Calculates the Score after a Hard Drop
        /// </summary>
        /// <param name="howManyLines">How Many lines the hard Drop did.</param>
        private void Grid_HardDrop(int howManyLines)
        {
            _totalScore += howManyLines * 2;
        }

        /// <summary>
        /// Calucates the Scores After clearing line(s)
        /// </summary>
        /// <param name="numberOfLines"></param>
        private void Grid_LineCleared(int numberOfLines)
        {
            if (numberOfLines > 0)
            {
                //Adding the score depending on the level and score.
                int tmp;
                switch (numberOfLines)
                {
                    case 1:
                        tmp = 40;
                        break;
                    case 2:
                        tmp = 100;
                        break;
                    case 3:
                        tmp = 300;
                        break;
                    case 4:
                        tmp = 1200;
                        break;
                    default:
                        tmp = -1;
                        break;
                }
                if (tmp != -1)
                    _totalScore += (_totalLevel + 1) * tmp;
                //Add it too the number of lines
                _numberOfLinesCleared += numberOfLines;
                //See if you leveled up
                int lines = _numberOfLinesCleared;
                int level = lines / Properties.Settings.Default.Number_Of_Lines_Needed_To_Level_Up;
                if (level != _totalLevel)
                {
                    _totalLevel = level;
                    //You win the Game If you get at the winning level
                    if (_totalLevel >= Properties.Settings.Default.Winning_Level)
                        SetHighScores();
                    else
                        _timeClock.Interval -= Properties.Settings.Default.Level_Up_Timer_Decrease;
                }
            }
        }

        /// <summary>
        /// Sets the flag to false, Surpressing Key Press Events
        /// </summary>
        private void Grid_StartedMoving()
        {
            OkToMove = false;
        }

        /// <summary>
        /// Starts the Timer Tick if it was set to False from the Grid
        /// </summary>
        private void Grid_PieceLocked()
        {
            if (_timeClock.Enabled == false)
                _timeClock.Start();
        }

        /// <summary>
        /// Sets the flag to true, allowing key press events
        /// </summary>
        private void Grid_DoneMoving()
        {
            OkToMove = true;
        }

        /// <summary>
        /// The Game Has been over because lines are above the top of the gridS
        /// </summary>
        private void Grid_Overflow()
        {
            _timeClock.Stop();
            SetHighScores();
        }

        /// <summary>
        /// This tells the form that which block to show on the form for the held block
        /// </summary>
        private void Grid_PieceHeld()
        {
            LoadHeldPicture?.Invoke(_grid.BlockThatIsHeld);
            LoadPreviewPictures();
        }

        #endregion Grid Events

        #region Private Methods

        /// <summary>
        /// Shows the Form that the Game is Over and Opens up the High Score Submit Form
        /// </summary>
        private void SetHighScores()
        {
            if (_formAlreadyUp == false)
            {
                _formAlreadyUp = true;
                using (HighScoresSubmit hss = new HighScoresSubmit(_totalScore, _totalLevel, _numberOfLinesCleared))
                    hss.ShowDialog();
                GameOver?.Invoke();
                _themeSong.Stop();
            }
        }

        /// <summary>
        /// Tells the Form which picture to use for the Preview Pictures
        /// </summary>
        private void LoadPreviewPictures()
        {
            List<TypeOfBlock> tmp = RandomBlock.LookAtTheNextThreePieces();
            LoadFirstPreviewPicture?.Invoke(tmp[0]);
            LoadSecondPreviewPicture?.Invoke(tmp[1]);
            LoadThirdPreviewPicture?.Invoke(tmp[2]);
        }

        /// <summary>
        /// This is the Games Timer Tick 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeClock_Tick(object sender, EventArgs e)
        {
            //Start of Game
            if (_grid.BlockThatsFalling == null)
            {
                _grid.CreateFallingBlock(RandomBlock.PullABlock());
                LoadPreviewPictures();
            }
            else
            {
                if (_grid.BlockThatsFalling.IsItFalling)
                    _grid.MoveFallingBlock();
                else
                {
                    _grid.CreateFallingBlock(RandomBlock.PullABlock());
                    LoadPreviewPictures();
                }
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// This Asnyc Method Allows the Grid to do its thing while doing a Timer Tick
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public async Task MovePieceAsync(Keys keyCode)
        {
            if (_grid.BlockThatsFalling != null)
            {
                if (_grid.BlockThatsFalling.IsItFalling)
                {
                    if (keyCode == Keys.Space)
                        _timeClock.Stop();
                    //V is soft drop, every time you press it it adds one to the score while its falling
                    if (keyCode == Keys.V)
                        _totalScore += 1;
                    await Task.Run(() => _grid.MovePiece(keyCode));
                    do
                    {
                        //This loop allows the program to finish what it needs to do before it moves again
                    } while (OkToMove == false);
                }
                PieceOkToMove?.Invoke();
            }
        }

        /// <summary>
        /// This will be called from the form to Stop the game from contuining
        /// </summary>
        public void StopGame()
        {
            _themeSong.Stop();
            _timeClock.Stop();
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }
        #endregion Public Methods
    }
}
