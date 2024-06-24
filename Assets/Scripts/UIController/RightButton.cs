using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunting
{
    public class RightButton : MonoBehaviour
    {
        View view;

        private void Start()
        {
            view = GameObject.Find("View").GetComponent<View>();
        }

        public void OnClick()
        {
            view.moveFloatingWall(MoveActions.right);
        }
    }
}