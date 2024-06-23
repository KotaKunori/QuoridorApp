using System.Collections;
using System.Collections.Generic;

namespace TreasureHunting
{
    public class Player
    {
        Position MyPosion = new Position();
        Position TreasurePosition = new Position();
        Wall_Algorithm wall_Algorithm ;
        Move_Algorithm move_Algorithm ;
        public Player(W_Wall_Algorithm Wall_Algorithm, W_Move_Algorithm  Move_Algorithm)
        {
            this.wall_Algorithm = Wall_Algorithm;
            this.move_Algorithm = Move_Algorithm; 
        }

        /// <summary>
        /// 探索開始時に自分の座標とお宝の座標を格納
        /// </summary>
        /// <param name="myPosition">自分の座標</param>  
        /// <param name="treasurePosition">お宝の座標</param>
        public void setInitialState(Position myPosition, Position treasurePosition)
        {
            MyPosion = myPosition;
            TreasurePosition = treasurePosition;
        }

        
        /// <summary>
        /// 例が状態を受け取って設置する壁の座標を返すメソッドを呼び出すメソッド
        /// </summary>
        public Wall Call_getWallPosition(State state)
        {
            return wall_Algorithm.getWallPosition(state);
        }
        
       
        /// <summary>
        /// 例が状態を受け取って行動を返すメソッドを呼び出すメソッド
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>行動</returns>
        public MoveActions Call_getAction(VisibleState state)
        {
            return move_Algorithm.getAction(state);
        }


    }


}