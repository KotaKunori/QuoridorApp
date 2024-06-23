using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureHunting
{
    public class W_Move_Algorithm : Move_Algorithm
    {
        /// <summary>
        /// 状態を受け取って行動を返すメソッド
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>行動</returns>
        public MoveActions getAction(VisibleState state)
        {
            //以下はデバッグ用のコード()

            System.Random rand = new System.Random();
            int action = rand.Next(4);
            return (MoveActions)action;
            //この部分を消して記入してください
        }
    }
}
