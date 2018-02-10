using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.Classes
{
    /// <summary>
    /// Static Class to Figure out the RandomNess of the Tetris Blocks
    /// </summary>
    internal static class RandomBlock
    {
        #region Variables

        private static Queue<TypeOfBlock> QueueList = new Queue<TypeOfBlock>();
        private static Random RNG = new Random();
        private static int MIN_BLOCKS_TO_REFILL_BAG = 4;

        #endregion Variables

        #region Public Methods

        /// <summary>
        /// Pulls the Next Block in the List
        /// </summary>
        /// <returns>The Next Block in the List</returns>
        public static TypeOfBlock PullABlock()
        {
            if (QueueList.Count < MIN_BLOCKS_TO_REFILL_BAG)
                AddNewPieces();
            return QueueList.Dequeue();
        }

        /// <summary>
        /// Peeks at the Next Three Pieces in the List
        /// </summary>
        /// <returns></returns>
        public static List<TypeOfBlock> LookAtTheNextThreePieces()
        {
            List<TypeOfBlock> tmp = new List<TypeOfBlock>();
            tmp.Add(QueueList.ElementAt(0));
            tmp.Add(QueueList.ElementAt(1));
            tmp.Add(QueueList.ElementAt(2));
            return tmp;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Adds All the blocks in a random order to the Queue
        /// </summary>
        private static void AddNewPieces()
        {
            //We have seven different blocks. 
            //We have to add them to the queue list randomly but only one of.  
            //starting at 0
            List<TypeOfBlock> tmp = new List<TypeOfBlock>();
            int numberOfBlocksLeft = 6;
            for (int i = numberOfBlocksLeft; i >= 0; i--)
            {
                tmp.Add((TypeOfBlock)i);
            }
            foreach (var item in tmp.Randomize())
            {
                QueueList.Enqueue(item);
            }
        }

        #endregion Private Methods

    }

    /// <summary>
    /// Static Class to Add the Randomize to All Lists
    /// </summary>
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<t> Randomize<t>(this IEnumerable<t> target)
        {
            Random r = new Random();
            return target.OrderBy(x => (r.Next()));
        }
    }
}
