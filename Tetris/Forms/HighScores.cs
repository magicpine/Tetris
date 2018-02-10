using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Tetris.Classes;

namespace Tetris
{
    /// <summary>
    /// Shows the HighScores
    /// </summary>
    public partial class HighScores : Form
    {
        #region Variables

        private List<HighScoresClass> _highScores;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Opens the Files, and Sets them in the apportiate labels
        /// </summary>
        public HighScores()
        {
            OpenFile();
            InitializeComponent();
            SetTheScores();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Stores the list in the Apportiate Labels
        /// </summary>
        private void SetTheScores()
        {
            onelbl.Text = _highScores[0].Name + " - " + _highScores[0].Score;
            twoLbl.Text = _highScores[1].Name + " - " + _highScores[1].Score;
            threeLbl.Text = _highScores[2].Name + " - " + _highScores[2].Score;
            fourLbl.Text = _highScores[3].Name + " - " + _highScores[3].Score;
            fiveLbl.Text = _highScores[4].Name + " - " + _highScores[4].Score;
            sixLbl.Text = _highScores[5].Name + " - " + _highScores[5].Score;
            sevenLbl.Text = _highScores[6].Name + " - " + _highScores[6].Score;
            eightLbl.Text = _highScores[7].Name + " - " + _highScores[7].Score;
            nineLbl.Text = _highScores[8].Name + " - " + _highScores[8].Score;
            tenLbl.Text = _highScores[9].Name + " - " + _highScores[9].Score;
        }

        /// <summary>
        /// Opens the file and puts it into the list
        /// </summary>
        private void OpenFile()
        {
            ///IF the File Doesn't Exist then use the embedded file
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
    }
}
