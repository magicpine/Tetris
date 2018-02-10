using System;

namespace Tetris.Classes
{
    /// <summary>
    /// Holds information about the HighScores
    /// </summary>
    [Serializable()]
    public class HighScoresClass
    {
        #region Properties

        /// <summary>
        /// Holds the Score
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Holds Which level they made
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Holds how many lines they cleared
        /// </summary>
        public int NumberOfLines { get; set; }
        /// <summary>
        /// Holds the name of the player
        /// </summary>
        public string Name { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="score"></param>
        /// <param name="level"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="name"></param>
        public HighScoresClass(int score, int level, int numberOfLines,string name)
        {
            Score = score;
            Level = level;
            NumberOfLines = numberOfLines;
            Name = name;
        }

        /// <summary>
        /// Constructor for the Serialization
        /// </summary>
        public HighScoresClass()
        {

        }

        #endregion Constructor
    }
}
