using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureHunting
{
    interface Wall_Algorithm
    {
        /// <summary>
        /// 状態を受け取って設置する壁の座標を返すメソッド
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>壁の座標</returns>
        Wall getWallPosition(State state);
        
           
        
    }
}
