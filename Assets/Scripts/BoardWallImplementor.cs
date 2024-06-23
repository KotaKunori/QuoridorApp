using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TreasureHunting
{
    public class BoardWallImplementor
    {
        Board board;

        /// <summary>
        /// ?????N???X???????o?????????N???X?????????????????R???X?g???N?^
        /// </summary>
        /// <param name="board">?????N???X</param>
        public BoardWallImplementor(Board board)
        {
            this.board = board;
        }

        /// <summary>
        /// ???????u?????\?????????????????\?b?h
        /// </summary>
        /// <param name="wall">???u???????????W???????????N???X</param>
        /// <returns>???????u???\????????</returns>
        public bool wallSettingEnable(Wall wall)
        {
            State stateClass = board.getState();
            int[,] state;
            state = new int[19, 19];
            state = stateClass.getState();

            Position ptop = wall.wallTop;
            Position pcenter = wall.center;
            Position pbottom = wall.wallBottom;

            if (pcenter.x == ptop.x && pcenter.x == pbottom.x) //x???W??????????(?c)
            {
                if ((pcenter.x % 2) == 0)  //x???W??????
                {
                    if ((pcenter.y % 2) != 0)  //center??y???W?????????O??false
                        return false;
                }
                else
                    return false;
            }
            else if (pcenter.y == ptop.y && pcenter.y == pbottom.y) //y???W??????????(??)
            {
                if ((pcenter.x % 2) == 0)  //y???W??????
                {
                    if (pcenter.y % 2 != 0) //center??x???W?????????O??false
                        return false;
                   
                }
                else
                    return false;
            }
            else //x??y?????????W?????????????????????_??
                return false;

            if (state[ptop.x, ptop.y] == 1 || state[ptop.x, ptop.y] == 2 || state[ptop.x, ptop.y] == 3 || state[pcenter.x, pcenter.y] == 1 || state[pcenter.x, pcenter.y] == 2 || state[pcenter.x, pcenter.y] == 3 || state[pbottom.x, pbottom.y] == 1 || state[pbottom.x, pbottom.y] == 2 || state[pbottom.x, pbottom.y] == 3) //?????????u??????????
                return false;

            //???H????

            Stack<Position> cycle = new Stack<Position>();
            Stack<Position> duplicate = new Stack<Position>();

            Position instance;

            cycle.Push(ptop); //ptop?????W??cycle?????[

            int counting = 0;

            while (cycle.Count > 0)
            {
                Position val = cycle.Pop(); //cycle?????????o??
                counting++;
                if (counting > 35)
                    break;

                duplicate.Push(val); //duplicate?????[

                if ((val.x % 2) == 0) //val??x???W????????????
                {
                    if (val.y > 1)
                    {
                        if (state[val.x, val.y - 2] == 1 || state[val.x, val.y - 2] == 2 || state[val.x, val.y - 2] == 3)
                        {
                            instance.x = val.x;
                            instance.y = val.y - 2;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }

                        }
                    }

                    if (val.y < 17)
                    {
                        if (state[val.x, val.y + 2] == 1 || state[val.x, val.y + 2] == 2 || state[val.x, val.y + 2] == 3)
                        {
                            instance.x = val.x;
                            instance.y = val.y + 2;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y < 18)
                    {
                        if (state[val.x + 1, val.y + 1] == 1 || state[val.x + 1, val.y + 1] == 2 || state[val.x + 1, val.y + 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y > 0)
                    {
                        if (state[val.x + 1, val.y - 1] == 1 || state[val.x + 1, val.y - 1] == 2 || state[val.x + 1, val.y - 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x > 0 && val.y < 18)
                    {
                        if (state[val.x - 1, val.y + 1] == 1 || state[val.x - 1, val.y + 1] == 2 || state[val.x - 1, val.y + 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x > 0 && val.y > 0)
                    {
                        if (state[val.x - 1, val.y - 1] == 1 || state[val.x - 1, val.y - 1] == 2 || state[val.x - 1, val.y - 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                }

                else if ((val.x % 2) == 1) //val??x???W????????????
                {
                    if (val.x > 1)
                    {
                        if (state[val.x - 2, val.y] == 1 || state[val.x - 2, val.y] == 2 || state[val.x - 2, val.y] == 3)
                        {
                            instance.x = val.x - 2;
                            instance.y = val.y;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }

                        }
                    }

                    if (val.x < 17)
                    {
                        if (state[val.x + 2, val.y] == 1 || state[val.x + 2, val.y] == 2 || state[val.x + 2, val.y] == 3)
                        {
                            instance.x = val.x + 2;
                            instance.y = val.y;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y < 18)
                    {
                        if (state[val.x + 1, val.y + 1] == 1 || state[val.x + 1, val.y + 1] == 2 || state[val.x + 1, val.y + 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y > 0)
                    {
                        if (state[val.x + 1, val.y - 1] == 1 || state[val.x + 1, val.y - 1] == 2 || state[val.x + 1, val.y - 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x > 0 && val.y < 18)
                    {
                        if (state[val.x - 1, val.y + 1] == 1 || state[val.x - 1, val.y + 1] == 2 || state[val.x - 1, val.y + 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x > 0 && val.y > 0)
                    {
                        if (state[val.x - 1, val.y - 1] == 1 || state[val.x - 1, val.y - 1] == 2 || state[val.x - 1, val.y - 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                }

            }

            cycle.Clear();
            duplicate.Clear();
            counting = 0;

            cycle.Push(pbottom); //pbottom?????W??cycle?????[

            while (cycle.Count > 0)
            {
                Position val = cycle.Pop(); //cycle?????????o??
                counting++;
                if (counting > 35)
                    break;

                duplicate.Push(val); //duplicate?????[

                if ((val.x % 2) == 0) //val??x???W????????????
                {
                    if (val.y > 1)
                    {
                        if (state[val.x, val.y - 2] == 1 || state[val.x, val.y - 2] == 2 || state[val.x, val.y - 2] == 3)
                        {
                            instance.x = val.x;
                            instance.y = val.y - 2;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }

                        }
                    }

                    if (val.y < 17)
                    {
                        if (state[val.x, val.y + 2] == 1 || state[val.x, val.y + 2] == 2 || state[val.x, val.y + 2] == 3)
                        {
                            instance.x = val.x;
                            instance.y = val.y + 2;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y < 18)
                    {
                        if (state[val.x + 1, val.y + 1] == 1 || state[val.x + 1, val.y + 1] == 2 || state[val.x + 1, val.y + 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y > 0)
                    {
                        if (state[val.x + 1, val.y - 1] == 1 || state[val.x + 1, val.y - 1] == 2 || state[val.x + 1, val.y - 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x > 0 && val.y < 18)
                    {
                        if (state[val.x - 1, val.y + 1] == 1 || state[val.x - 1, val.y + 1] == 2 || state[val.x - 1, val.y + 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x > 0 && val.y > 0)
                    {
                        if (state[val.x - 1, val.y - 1] == 1 || state[val.x - 1, val.y - 1] == 2 || state[val.x - 1, val.y - 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                }

                else if ((val.x % 2) == 1) //val??x???W????????????
                {
                    if (val.x > 1)
                    {
                        if (state[val.x - 2, val.y] == 1 || state[val.x - 2, val.y] == 2 || state[val.x - 2, val.y] == 3)
                        {
                            instance.x = val.x - 2;
                            instance.y = val.y;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }

                        }
                    }

                    if (val.x < 17)
                    {
                        if (state[val.x + 2, val.y] == 1 || state[val.x + 2, val.y] == 2 || state[val.x + 2, val.y] == 3)
                        {
                            instance.x = val.x + 2;
                            instance.y = val.y;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y < 18)
                    {
                        if (state[val.x + 1, val.y + 1] == 1 || state[val.x + 1, val.y + 1] == 2 || state[val.x + 1, val.y + 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x < 18 && val.y > 0)
                    {
                        if (state[val.x + 1, val.y - 1] == 1 || state[val.x + 1, val.y - 1] == 2 || state[val.x + 1, val.y - 1] == 3)
                        {
                            instance.x = val.x + 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val. x > 0 && val.y < 18)
                    {
                        if (state[val.x - 1, val.y + 1] == 1 || state[val.x - 1, val.y + 1] == 2 || state[val.x - 1, val.y + 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y + 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                    if (val.x > 0 && val.y > 0)
                    {
                        if (state[val.x - 1, val.y - 1] == 1 || state[val.x - 1, val.y - 1] == 2 || state[val.x - 1, val.y - 1] == 3)
                        {
                            instance.x = val.x - 1;
                            instance.y = val.y - 1;
                            if (cycle.Contains(instance))
                            {
                                return false;
                            }
                            if (duplicate.Contains(instance))
                            {

                            }
                            else
                            {
                                cycle.Push(instance);
                            }
                        }
                    }

                }

            }

            return true;

        }

        //int wallcount = 2;
        int wallcount = 20;

        /// <summary>
        /// ?v???[???[???w?????????W?????????u???????\?b?h
        /// </summary>
        /// <param name="player">???????u?????v???[???[</param>
        /// <param name="wall">???????W???????????N???X</param>
        public State wallSetting(Players player, Wall wall)
        {
            State stateClass = board.getState();
            int[,] state;
            state = new int[19, 19];
            state = stateClass.getState();

            Position ptop = wall.wallTop;
            Position pcenter = wall.center;
            Position pbottom = wall.wallBottom;

            if (player == 0)
            {
                state[ptop.x, ptop.y] = 2; //???u????????????statas???X?V
                state[pcenter.x, pcenter.y] = 2;
                state[pbottom.x, pbottom.y] = 2;
                stateClass.setState(state); //???????X?V
            }
            else
            {
                state[ptop.x, ptop.y] = 3; //???u????????????statas???X?V
                state[pcenter.x, pcenter.y] = 3;
                state[pbottom.x, pbottom.y] = 3;
                stateClass.setState(state); //???????X?V
            }

            wallcount--; //???????u???????????J?E???g?}?C?i?X?P

            return stateClass;
        }



        /// <summary>
        /// ?v???[???[???g?p???\???c???????????????????\?b?h
        /// </summary>
        /// <returns>??????</returns>
        public int getRestWallCount()
        {
            return wallcount;
        }
    }
    
    
}