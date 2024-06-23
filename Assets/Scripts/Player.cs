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
        /// �T���J�n���Ɏ����̍��W�Ƃ���̍��W���i�[
        /// </summary>
        /// <param name="myPosition">�����̍��W</param>  
        /// <param name="treasurePosition">����̍��W</param>
        public void setInitialState(Position myPosition, Position treasurePosition)
        {
            MyPosion = myPosition;
            TreasurePosition = treasurePosition;
        }

        
        /// <summary>
        /// �Ⴊ��Ԃ��󂯎���Đݒu����ǂ̍��W��Ԃ����\�b�h���Ăяo�����\�b�h
        /// </summary>
        public Wall Call_getWallPosition(State state)
        {
            return wall_Algorithm.getWallPosition(state);
        }
        
       
        /// <summary>
        /// �Ⴊ��Ԃ��󂯎���čs����Ԃ����\�b�h���Ăяo�����\�b�h
        /// </summary>
        /// <param name="state">���</param>
        /// <returns>�s��</returns>
        public MoveActions Call_getAction(VisibleState state)
        {
            return move_Algorithm.getAction(state);
        }


    }


}