using System.Collections;
using System.Collections.Generic;

namespace TreasureHunting
{
    public class Player
    {
        Players myPlayer;
        Wall_Algorithm wall_Algorithm ;
        Move_Algorithm move_Algorithm ;
        public Player(Players myPlayer, Wall_Algorithm Wall_Algorithm, Move_Algorithm  Move_Algorithm)
        {
            this.myPlayer = myPlayer;
            this.wall_Algorithm = Wall_Algorithm;
            this.move_Algorithm = Move_Algorithm; 
        }
        
        /// <summary>
        /// ???????????????????????u???????????W?????????\?b?h???????o?????\?b?h
        /// </summary>
        public Wall Call_getWallPosition(State state)
        {
            return wall_Algorithm.getWallPosition(state, myPlayer);
        }

        public void setInitialState(Position myPosition, Position treasurePosition)
        {
            move_Algorithm.setInitialState(myPosition, treasurePosition);
        }

        /// <summary>
        /// ?????????????????????s???????????\?b?h???????o?????\?b?h
        /// </summary>
        /// <param name="state">????</param>
        /// <returns>?s??</returns>
        public MoveActions Call_getAction(VisibleState state)
        {
            return move_Algorithm.getAction(state, myPlayer);
        }


    }


}