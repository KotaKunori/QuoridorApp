using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunting
{
    public class CpuButton : MonoBehaviour
    {
        View view;

        private void Start()
        {
            view = GameObject.Find("View").GetComponent<View>();
        }

        public void OnClick()
        {
            view.setPlayerFlag(false);
        }
    }
}