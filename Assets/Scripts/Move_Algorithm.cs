using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureHunting
{
    public abstract class Move_Algorithm
    {
        Position MyPosion = new Position();
        Position TreasurePosition = new Position();

        /// <summary>
        /// 状態を受け取って行動を返すメソッド
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>行動</returns>
        abstract public MoveActions getAction(VisibleState state, Players myPlayer);

        /// <summary>
        /// ?T???J?n?????????????W???????????W???i?[
        /// </summary>
        /// <param name="myPosition">?????????W</param>  
        /// <param name="treasurePosition">?????????W</param>
        public void setInitialState(Position myPosition, Position treasurePosition)
        {
            MyPosion = myPosition;
            TreasurePosition = treasurePosition;
        }
    }
}
