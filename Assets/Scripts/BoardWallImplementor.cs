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

        private bool listContains(Position targetPosition, List<Position> targetList)
        {
            foreach (Position position in targetList)
            {
                if (position == targetPosition)
                {

                    return true;
                }
            }
            return false;
        }

        private bool existRoot(int[, ] state, Position startPosition, Position endPosition)
        {
            List<Position> exploreList = new List<Position>();
            List<Position> exploredList = new List<Position>();

            exploreList.Add(startPosition);
            while(exploreList.Count != 0)
            {
                Position currentPosition = exploreList[0];
                exploreList.Remove(currentPosition);

                if(currentPosition == endPosition)
                {
                    return true;
                }

                exploredList.Add(currentPosition);

                if(currentPosition.x != 1 && state[currentPosition.x - 1, currentPosition.y] == 0)
                {
                    Position targetPosition = new Position(currentPosition.x - 2, currentPosition.y);
                    if (!listContains(targetPosition, exploreList) && !listContains(targetPosition, exploredList))
                    {
                        exploreList.Add(targetPosition);
                    }
                }
                if (currentPosition.x != 17 && state[currentPosition.x + 1, currentPosition.y] == 0)
                {
                    Position targetPosition = new Position(currentPosition.x + 2, currentPosition.y);
                    if (!listContains(targetPosition, exploreList) && !listContains(targetPosition, exploredList))
                    {
                        exploreList.Add(targetPosition);
                    }
                }
                if (currentPosition.y != 1 && state[currentPosition.x, currentPosition.y - 1] == 0)
                {
                    Position targetPosition = new Position(currentPosition.x, currentPosition.y - 2);
                    if (!listContains(targetPosition, exploreList) && !listContains(targetPosition, exploredList))
                    {
                        exploreList.Add(targetPosition);
                    }
                }
                if (currentPosition.y != 17 && state[currentPosition.x, currentPosition.y + 1] == 0)
                {
                    Position targetPosition = new Position(currentPosition.x, currentPosition.y + 2);
                    if (!listContains(targetPosition, exploreList) && !listContains(targetPosition, exploredList))
                    {
                        exploreList.Add(targetPosition);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ???????u?????\?????????????????\?b?h
        /// </summary>
        /// <param name="wall">???u???????????W???????????N???X</param>
        /// <returns>???????u???\????????</returns>
        public bool wallSettingEnable(Wall wall)
        {
            State stateHolder = board.getState();
            int[,] state = stateHolder.getState();

            Position ptop = wall.wallTop;
            Position pcenter = wall.center;
            Position pbottom = wall.wallBottom;

            if(!((ptop.x == pcenter.x - 1 && pbottom.x == pcenter.x + 1 && ptop.y == pcenter.y && pbottom.y == pcenter.y)
                || (ptop.y == pcenter.y - 1 && pbottom.y == pcenter.y + 1 && ptop.x == pcenter.x && pbottom.x == pcenter.x)))
            {
                return false;
            }
            if(pcenter.x % 2 != 0 || pcenter.y % 2 != 0)
            {
                return false;
            }

            if(pcenter.x < 2 || pcenter.y < 2 || pcenter.x > 16 || pcenter.y > 16)
            {
                return false;
            }

            if(state[pcenter.x, pcenter.y] != (int) StateAtrribute.space || state[ptop.x, ptop.y] != (int)StateAtrribute.space
                || state[pbottom.x, pbottom.y] != (int)StateAtrribute.space)
            {
                return false;
            }

            state[ptop.x, ptop.y] = (int)StateAtrribute.wall;
            state[pcenter.x, pcenter.y] = (int)StateAtrribute.wall;
            state[pbottom.x, pbottom.y] = (int)StateAtrribute.wall;

            Position player1Position = board.getPlayerPosition(Players.player1);
            Position player2Position = board.getPlayerPosition(Players.player2);
            Position treasurePosition = board.getTreasurePosition();

            if(!existRoot(state, player1Position, treasurePosition))
            {
                return false;
            }
            if(!existRoot(state, player2Position, treasurePosition))
            {
                return false;
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