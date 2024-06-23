using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TreasureHunting
{
    public class BoardSearchImplementor
    {
        Board board;

        /// <summary>
        /// ?????N???X???????o?????????N???X?????????????????R???X?g???N?^
        /// </summary>
        /// <param name="board">?????N???X</param>
        public BoardSearchImplementor(Board board)
        {
            this.board = board;
        }

        /// <summary>
        /// ?w?????????v???[???[???F?m???\???????????????\?b?h
        /// </summary>
        /// <param name="player">?w?????????v???[???[</param>
        /// <returns>?F?m???\??????</returns>
        public VisibleState getVisibleState(Players player)
        {
            State stateClass = board.getState();
            int[,] state;
            state = new int[19, 19];
            state = stateClass.getState();

            VisibleState v = new VisibleState();

            int[] visible = new int[4] { 0, 0, 0, 0 };//???????E

            Position p = getPlayerPosition(player);//?v???C???[?????u??????

            if (1 <= state[p.x, p.y - 1] && state[p.x, p.y - 1] <= 3)//??????
            {
                visible[0] = (int)state[p.x, p.y - 1];//?P????
            }
            else
            {
                visible[0] = (int)state[p.x, p.y - 2];//?Q????
            }

            if (1 <= state[p.x, p.y + 1] && state[p.x, p.y + 1] <= 3)//??????
            {
                visible[1] = (int)state[p.x, p.y + 1];//?P????
            }
            else
            {
                visible[1] = (int)state[p.x, p.y + 2];//?Q????
            }

            if (1 <= state[p.x + 1, p.y] && state[p.x + 1, p.y] <= 3)//?E????
            {
                visible[2] = (int)state[p.x + 1, p.y];//?P???E
            }
            else
            {
                visible[2] = (int)state[p.x + 2, p.y];//?Q???E
            }

            if (1 <= state[p.x - 1, p.y] && state[p.x - 1, p.y] <= 3)//??????
            {
                visible[3] = (int)state[p.x - 1, p.y];//?P????
            }
            else
            {
                visible[3] = (int)state[p.x - 2, p.y];//?Q????
            }

            v.setVisibleState(visible);
            return v;
        }

        /// <summary>
        /// ?v???[???[???????????????????s???????\?????????????????\?b?h
        /// </summary>
        /// <param name="player">?s???????v???[???[</param>
        /// <param name="moveAction">?s???????e</param>
        /// <returns>?s???????\????????</returns>
        public bool moveActionEnable(Players player, MoveActions moveAction)
        {
            State stateClass = board.getState();
            int[,] state;
            state = new int[19, 19];
            state = stateClass.getState();

            Position p = getPlayerPosition(player);//?v???C???[?????u??????

            int x_plus = 0;
            int y_plus = 0;
            if ((int)moveAction == 0)//??
            {
                x_plus = 0;
                y_plus = -1;
            }
            if ((int)moveAction == 1)//??
            {
                x_plus = 0;
                y_plus = 1;
            }
            if ((int)moveAction == 2)//?E
            {
                x_plus = 1;
                y_plus = 0;
            }
            if ((int)moveAction == 3)//??
            {
                x_plus = -1;
                y_plus = 0;
            }

            if (1 <= state[p.x + x_plus, p.y + y_plus] && state[p.x + x_plus, p.y + y_plus] <= 3)//?i?????????H
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ?v???[???[???s???????f?????N???X
        /// </summary>
        /// <param name="player">?s???????v???[???[</param>
        /// <param name="moveAction">?s???????e</param>
        public State moveAction(Players player, MoveActions moveAction)
        {
            State stateClass = board.getState();
            int[,] state;
            state = new int[19, 19];
            state = stateClass.getState();

            Position p = getPlayerPosition(player);//?v???C???[?????u??????

            int enemy = 0;
            if ((int)player == 0)//?G???????H
            {
                enemy = 1;
            }
            else
            {
                enemy = 0;
            }


            int x_plus = 0;
            int y_plus = 0;
            if ((int)moveAction == 0)//??
            {
                x_plus = 0;
                y_plus = -2;
            }
            if ((int)moveAction == 1)//??
            {
                x_plus = 0;
                y_plus = 2;
            }
            if ((int)moveAction == 2)//?E
            {
                x_plus = 2;
                y_plus = 0;
            }
            if ((int)moveAction == 3)//??
            {
                x_plus = -2;
                y_plus = 0;
            }

            if (state[p.x + x_plus, p.y + y_plus] == 0)//?i????????
            {
                state[p.x, p.y] = 0;//?????????u?????????X
                state[p.x + x_plus, p.y + y_plus] = (int)player + 4;//?i?????v???C???[?????X

                stateClass.setState(state);//???????X?V
            }

            if (state[p.x + x_plus, p.y + y_plus] == 6)//?i??????????
            {
                state[p.x, p.y] = 0;//?????????u?????????X
                state[p.x + x_plus, p.y + y_plus] = (int)player + 8;//?i?????S?[???????????X?i?v???C???[?P?????W?A?v???C???[?Q?????X?j

                stateClass.setState(state);//???????X?V
            }

            if (state[p.x + x_plus, p.y + y_plus] == enemy + 4)//?i???????G
            {
                state[p.x, p.y] = 0;//?????????u?????????X
                state[p.x + x_plus, p.y + y_plus] = 7;//?i?????d???????????X

                stateClass.setState(state);//???????X?V
            }

            return stateClass;
        }
        /// <summary>
        /// ?????????u?????????\?b?h
        /// </summary>
        /// <returns>?????????u</returns>
        public Position getTreasurePosition()
        {

            State stateClass = board.getState();
            int[,] state;
            state = new int[19, 19];
            state = stateClass.getState();

            for (int i = 1; i < 19; i = i + 2)
            {
                for (int j = 1; j < 19; j = j + 2)
                {
                    if (state[i, j] == 6)//?????E???????????????I??
                    {
                        return new Position(i, j);
                    }
                }
            }
            return new Position(0, 0);//?G???[
        }

        /// <summary>
        /// ?w?????????v???[???[?????u?????????\?b?h
        /// </summary>
        /// <param name="player">?w?????????v???[???[</param>
        /// <returns>?v???[???[?????u</returns>
        public Position getPlayerPosition(Players player)
        {
            State stateClass = board.getState();
            int[,] state;
            state = new int[19, 19];
            state = stateClass.getState();

            for (int i = 1; i < 19; i = i + 2)
            {
                for (int j = 1; j < 19; j = j + 2)
                {
                    if (state[i, j] == (int)player + 4)//?v???C???[?P????4+0=4?A?v???C???[?Q????4+1=5
                    {
                        return new Position(i, j);
                    }
                }
            }
            ///?d????????????????????
            return new Position(9, 17 - (int)player * 17);//?G???[
        }
    }
}

