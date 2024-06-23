using System.Collections;
using System.Collections.Generic;

namespace TreasureHunting
{
    /// <summary>
    /// プレーヤーを識別する値
    /// </summary>
    public enum Players 
    {
        /// <summary>
        /// プレーヤー１の値
        /// </summary>
        player1, 
        /// <summary>
        /// プレーヤー２の値
        /// </summary>
        player2 
    }

    /// <summary>
    /// 探索時のアクションを数値表現
    /// </summary>
    public enum MoveActions
    {
        /// <summary>
        /// 上に移動
        /// </summary>
        up,
        /// <summary>
        /// 下に移動
        /// </summary>
        down,
        /// <summary>
        /// 右に移動
        /// </summary>
        right,
        /// <summary>
        /// 左に移動
        /// </summary>
        left
    }

    /// <summary>
    /// 状態の要素
    /// </summary>
    public enum StateAtrribute
    {
        /// <summary>
        /// 何もないマスもしくは境界線
        /// </summary>
        space,
        /// <summary>
        /// 外壁のある場所
        /// </summary>
        wall,
        /// <summary>
        /// /// player1の置いた壁のある場所
        /// </summary>
        wall1,
        /// <summary>
        /// /// player2の置いた壁のある場所
        /// </summary>
        wall2,
        /// <summary>
        /// プレーヤー１のいるマス
        /// </summary>
        player1,
        /// <summary>
        /// プレーヤー２のいるマス
        /// </summary>
        player2,
        /// <summary>
        /// お宝のあるマス
        /// </summary>
        treasure,
        /// <summary>
        ///プレーヤー１と２が同時に存在するマス
        /// </summary>
        player1And2,
        /// <summary>
        ///プレイヤー１のゴール判定
        ///
        /// </summary>
        goal1,
        /// <summary>
        ///プレイヤー2のゴール判定
        /// </summary>
        goal2
    }

    /// <summary>
    ///プレーヤーが視認できる状態を表す数値
    /// </summary>
    public enum VisibleStateAtrribute
    {
        /// <summary>
        /// 空いているマス
        /// </summary>
        space,
        /// <summary>
        ///味方の置いた壁
        /// </summary>
        myWall,
        /// <summary>
        /// 敵の置いた壁
        /// </summary>
        enemyWall,
        /// <summary>
        /// 敵
        /// </summary>
        enemy,
        /// <summary>
        /// お宝
        /// </summary>
        treasure
    }

    public enum CameraState
    {
        overlooking,
        player1View,
        player2View
    }

    public struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public static bool operator == (Position position1, Position position2)//比較演算子のオーバーライド
        {
            if(position1.x == position2.x && position1.y == position2.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Position position1, Position position2)
        {
            if (position1.x == position2.x && position1.y == position2.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    /// <summary>
    /// 壁を表現する構造体
    /// </summary>
    public struct Wall
    {
        public Position center;
        public Position wallTop;
        public Position wallBottom;

        public Wall(Position center, Position top, Position bottom)
        {
            this.center = center;
            this.wallTop = top;
            this.wallBottom = bottom;
        }
    }

    /// <summary>
    /// 状態を保持するクラス
    /// </summary>
    public class State
    {
        /// <summary>
        /// 状態をしまう19*19のint型配列
        /// </summary>
        private int[,] state;

        /// <summary>
        /// コンストラクタで初期状態を定義
        /// </summary>
        public State()
        {
            state = new int[19, 19];
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if(i == 0 || i == 18 || j == 0 || j == 18)
                    {
                        state[j, i] = (int)StateAtrribute.wall;//盤面の端なら値を壁にする
                    }
                    else
                    {
                        state[j, i] = (int)StateAtrribute.space;//盤面の端以外は値を空白にする
                    }
                }
            }
            state[9, 17] = (int)StateAtrribute.player1;//プレーヤー１の初期状態を設定
            state[9, 1] = (int)StateAtrribute.player2;//プレーヤー２の初期状態を設定
            state[9, 9] = (int)StateAtrribute.treasure;//お宝の初期状態を設定、ここランダムで配置できるようにしたい
        }

        public int[, ] getState()
        {
            return state;
        }

        public void setState(int[,] state)
        {
            this.state = state;
        }
    }

    /// <summary>
    /// 視認できる状態を保持するクラス
    /// </summary>
    public class VisibleState
    {
        private int[] state;

        public VisibleState()
        {
            state = new int[4];
        }

        public void setVisibleState(int[] state)
        {
            this.state = state;
        }
    }
}