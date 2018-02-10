using System;
using System.Windows.Forms;

namespace Tetris
{
    /// <summary>
    /// Instructions for the game
    /// </summary>
    public partial class Instructions : Form
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Instructions()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Closes down the Instructions so you can play.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion Events
    }
}
