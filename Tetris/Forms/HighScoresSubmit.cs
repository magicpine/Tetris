using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Tetris.Classes;

namespace Tetris
{
    /// <summary>
    /// Shows the user if they won or not
    /// </summary>
    public partial class HighScoresSubmit : Form
    {
        #region Variables

        private int _highScore;
        private int _level;
        private int _numberOflines;
        private string _name;
        private int _index;
        private List<HighScoresClass> _highScores;

        #endregion Variables

        #region Constructor 

        /// <summary>
        /// Constuctor that will Add 
        /// </summary>
        /// <param name="highScore"></param>
        /// <param name="level"></param>
        /// <param name="numberOflines"></param>
        public HighScoresSubmit(int highScore, int level, int numberOflines)
        {
            InitializeComponent();
            ///High Score
            _highScore = highScore;
            scoreLbl.Text = _highScore.ToString();
            ///Level
            _level = level;
            levelLbl.Text = _level.ToString();
            ///Number Of Lines
            _numberOflines = numberOflines;
            numberOfLinesLbl.Text = _numberOflines.ToString();
            ///Get The High Scores
            _highScores = new List<HighScoresClass>();
            ///IF the File Doesn't Exist then use the embedded file
            PutTheScoresInAList();
            ///Check The Scores To see if the user made it into the top ten
            if (SeeIfYourScoreIsBetter())
            {
                winningLosingLbl.Text = Properties.Settings.Default.Losing_Game_Text;
                nameLbl.Visible = true;
                nameTxt.Visible = true;
                nameBtn.Visible = true;
                highScoresBtn.Enabled = false;
            }
            else
            {
                winningLosingLbl.Text = Properties.Settings.Default.Winning_Game_Text;
            }
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Saves the Score to the File
        /// </summary>
        private void SaveTheHighScores()
        {
            ///Insert the new Score
            _highScores.Insert(_index, new HighScoresClass(_highScore, _level, _numberOflines, _name));
            ///Only saving the top ten
            _highScores.RemoveRange(10, _highScores.Count - 10);
            ///If the Path exists then save one or create it
            if (File.Exists(Properties.Settings.Default.HighScores_FilePath) == false)
            {
                var myfile = File.Create(Properties.Settings.Default.HighScores_FilePath);
                myfile.Close();
            }
            using (TextWriter tw = new StreamWriter(Properties.Settings.Default.HighScores_FilePath, true))
            {
                XmlSerializer x = new XmlSerializer(typeof(List<HighScoresClass>));
                x.Serialize(tw, _highScores);
            }
        }

        /// <summary>
        /// Checks the list to see if the user's score is saveable.  Saves the index if True
        /// </summary>
        /// <returns>True if the score is in the top ten, false if not</returns>
        private bool SeeIfYourScoreIsBetter()
        {
            int index = -1;
            foreach (var item in _highScores)
            {
                if (_highScore > item.Score)
                {
                    index = _highScores.IndexOf(item);
                    break;
                }
            }
            if (index != -1)
            {
                _index = index;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Puts the Scores into a list.  
        /// </summary>
        private void PutTheScoresInAList()
        {
            if (File.Exists(Properties.Settings.Default.HighScores_FilePath))
            {
                using (StreamReader sr = new StreamReader(Properties.Settings.Default.HighScores_FilePath))
                {
                    XmlSerializer x = new XmlSerializer(typeof(List<HighScoresClass>));
                    _highScores = (List<HighScoresClass>)x.Deserialize(sr);
                }
            }
            else
            {
                using (Stream stream = GetType().Assembly.GetManifestResourceStream("Tetris.Resources.highScores.xml"))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        XmlSerializer x = new XmlSerializer(typeof(List<HighScoresClass>));
                        _highScores = (List<HighScoresClass>)x.Deserialize(sr);
                    }
                }
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Shows the High Scores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highScoresBtn_Click(object sender, EventArgs e)
        {
            using (HighScores hs = new HighScores())
            {
                hs.ShowDialog();
            }
        }

        /// <summary>
        /// Saves the scores to the file, after a user put in a name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameBtn_Click(object sender, EventArgs e)
        {
            _name = nameTxt.Text;
            highScoresBtn.Enabled = true;
            nameBtn.Visible = false;
            nameLbl.Visible = false;
            nameTxt.Visible = false;
            SaveTheHighScores();
            Close();
        }

        #endregion Events
    }
}
