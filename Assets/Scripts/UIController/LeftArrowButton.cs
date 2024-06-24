using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunting
{
    public class LeftArrowButton : MonoBehaviour
    {
        View view;

        private void Start()
        {
            view = GameObject.Find("View").GetComponent<View>();
        }

        public void OnClick()
        {
            if (view.getCameraState() == CameraState.player1View || view.getCameraState() == CameraState.player2View)
            {
                view.rotateCameraLeft();
            }
        }
    }
}