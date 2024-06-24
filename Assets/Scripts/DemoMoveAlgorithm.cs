using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunting
{
    public class DemoMoveAlgorithm : Move_Algorithm
    {
        int preAction = (int)MoveActions.down;
        /// <summary>
        /// 状態を受け取って行動を返すメソッド
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>行動</returns>
        public override MoveActions getAction(VisibleState state, Players myPlayer)
        {
            int[] visibleState = state.getVisibleState();
            List<int> enableAction = new List<int>();
            //以下はデバッグ用のコード()
            for (int i = 0; i < visibleState.Length; i++)
            {
                if (visibleState[i] != (int)StateAtrribute.wall
                    && visibleState[i] != (int)StateAtrribute.wall1
                    && visibleState[i] != (int)StateAtrribute.wall2)
                {
                    enableAction.Add(i);
                }
            }

            int avoidAction;
            if (preAction % 2 == 0)
            {
                avoidAction = preAction + 1;
            }
            else
            {
                avoidAction = preAction - 1;
            }

            if (!(enableAction.Contains(avoidAction) && enableAction.Count == 1))
            {
                enableAction.Remove(avoidAction);
            }

            System.Random rand = new System.Random();
            int action = enableAction[rand.Next(enableAction.Count)];
            preAction = action;
            return (MoveActions)action;
            //この部分を消して記入してください
        }
    }
}