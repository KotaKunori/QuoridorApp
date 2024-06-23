using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureHunting
{
    public class W_Wall_Algorithm : Wall_Algorithm
    {
        /// <summary>
        /// 状態を受け取って設置する壁の座標を返すメソッド
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>壁の座標</returns>
        public Wall getWallPosition(State state)
        {
            //以下はデバッグ用のコード
            Wall wall;
            System.Random rand = new System.Random();
            int flag = rand.Next(2);
            int x = (rand.Next(8) + 1) * 2;
            int y = (rand.Next(8) + 1) * 2;

            if (rand.Next(2) == 0)//縦向きの壁を設置
            {
                wall = new Wall(new Position(x, y), new Position(x, y - 1), new Position(x, y + 1));

            }
            else//横向きの壁を設置
            {
                wall = new Wall(new Position(x, y), new Position(x - 1, y), new Position(x + 1, y));
            }
            return wall;
            //上の部分を消してお使いください
        }

    }
}
