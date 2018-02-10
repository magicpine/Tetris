using System;
using System.Windows.Forms;

namespace Tetris
{
    /// <summary>
    /// Starting Screen
    /// </summary>
    public partial class Main : Form
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Starts To play the Game, by starting with the instructions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_Click(object sender, EventArgs e)
        {
            ///Instructions
            using (Instructions play = new Instructions())
            {
                play.ShowDialog();
                play.Dispose();
            }
            ///The Actual Game
            using (PlayingSpace playingSpace = new PlayingSpace())
            {
                playingSpace.ShowDialog();
                playingSpace.Dispose();
            }
        }

        /// <summary>
        /// Shows the User the HighScores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highScoresBtn_Click(object sender, EventArgs e)
        {
            ///HighScores
            using (HighScores hs = new HighScores())
            {
                hs.ShowDialog();
                hs.Dispose();
            }
        }

        #endregion Events
    }
}
