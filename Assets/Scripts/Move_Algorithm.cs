using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureHunting
{
    interface Move_Algorithm
    {
        /// <summary>
        /// 状態を受け取って行動を返すメソッド
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>行動</returns>
        MoveActions getAction(VisibleState state);

    }
}
