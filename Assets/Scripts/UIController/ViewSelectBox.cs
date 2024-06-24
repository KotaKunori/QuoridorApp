using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunting
{
    public class ViewSelectBox : MonoBehaviour
    {
        View view;
        public Dropdown dropdown;

        private void Start()
        {
            view = GameObject.Find("View").GetComponent<View>();
        }

        public void OnValueChanged()
        {
            view.moveCamera((CameraState)dropdown.value);
        }
    }
}

