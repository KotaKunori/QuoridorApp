using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunting
{
    public class View : MonoBehaviour
    {
        GameController gameController;
        public Camera mainCamera;
        CameraMover cameraMover;
        private CameraState cameraState = CameraState.overlooking;

        public GameObject player1;//?v???[???[?P???i?[????????
        public GameObject player2;//?v???[???[?Q???i?[????????
        public GameObject treasureBox;//???????i?[????????
        public GameObject lightLine;//?????p?[?e?B?N?????i?[????????

        public GameObject plane;//?????v???n?u???C???X?y?N?^?[???????u?????p??????
        public GameObject hWall;//???????????v???n?u???C???X?y?N?^?[???????u?????p??????
        public GameObject vWall;//???????????v???n?u???C???X?y?N?^?[???????u?????p??????
        public GameObject player1HWall;
        public GameObject player1VWall;
        public GameObject player2HWall;
        public GameObject player2VWall;
        GameObject[,] planes;//?C???X?^???X?????????????????z??
        GameObject[] walls;//?C???X?^???X?????????????????z??

        public Text finishText;
        public Button finishButton;

        const int scale = 4;


        // Start is called before the first frame update
        void Start()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            cameraMover = mainCamera.GetComponent<CameraMover>();
        }

        public void continueGame()
        {
            gameController.ContinueGame();
        }

        /// <summary>
        /// ???W?????????????????`?????????\?b?h
        /// </summary>
        /// <param name="state">????</param>
        public void drawInitialState(State state)
        {
            planes = new GameObject[9, 9];//?X???X?????????????P?}?X??????????????
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    switch((StateAtrribute) state.getState()[j, i]) 
                    {
                        case StateAtrribute.space:
                            if(i % 2 == 1 && j % 2 == 1)
                            {
                                planes[(j - 1) / 2, (i - 1) / 2] = Instantiate(plane);//???????????A?z?????i?[
                                planes[(j - 1) / 2, (i - 1) / 2].transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);//???????u???z????????????????????
                            }
                            break;
                        case StateAtrribute.wall:
                            if (i % 2 == 0 && j % 2 == 1)
                            {
                                GameObject wall = Instantiate(hWall);
                                wall.transform.position += new Vector3(i / 2 * scale, 0, (j - 1) / 2 * scale);
                            }
                            else if(i % 2 == 1 && j % 2 == 0)
                            {
                                GameObject wall = Instantiate(vWall);
                                wall.transform.position += new Vector3((i - 1) / 2 * scale, 0, j / 2 * scale);
                            }
                            break;
                        case StateAtrribute.player1:
                            planes[(j - 1) / 2, (i - 1) / 2] = Instantiate(plane);//???????????A?z?????i?[
                            planes[(j - 1) / 2, (i - 1) / 2].transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);//???????u???z????????????????????
                            player1.transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);//???????????N???X???????\?b?h??????????????
                            break;
                        case StateAtrribute.player2:
                            planes[(j - 1) / 2, (i - 1) / 2] = Instantiate(plane);//???????????A?z?????i?[
                            planes[(j - 1) / 2, (i - 1) / 2].transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);//???????u???z????????????????????
                            player2.transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);//???????????N???X???????\?b?h??????????????
                            break;
                        case StateAtrribute.treasure:
                            planes[(j - 1) / 2, (i - 1) / 2] = Instantiate(plane);//???????????A?z?????i?[
                            planes[(j - 1) / 2, (i - 1) / 2].transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);//???????u???z????????????????????
                            treasureBox.transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);
                            lightLine.transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);
                            break;
                    }
                }
            }
        }

        public void drawWall(Players player, Wall wall)
        {
            GameObject[] settingWall = new GameObject[2];

            if (wall.wallTop.x == wall.wallBottom.x)
            {
                if (player == Players.player1)
                {
                    settingWall[0] = Instantiate(player1VWall);
                    settingWall[0].transform.position += new Vector3((wall.wallTop.y - 1) / 2 * scale, 0, wall.wallTop.x / 2 * scale);
                    settingWall[1] = Instantiate(player1VWall);
                    settingWall[1].transform.position += new Vector3((wall.wallBottom.y - 1) / 2 * scale, 0, wall.wallBottom.x / 2 * scale);
                }
                else
                {
                    settingWall[0] = Instantiate(player2VWall);
                    settingWall[0].transform.position += new Vector3((wall.wallTop.y - 1) / 2 * scale, 0, wall.wallTop.x / 2 * scale);
                    settingWall[1] = Instantiate(player2VWall);
                    settingWall[1].transform.position += new Vector3((wall.wallBottom.y - 1) / 2 * scale, 0, wall.wallBottom.x / 2 * scale);
                }
            }
            else
            {
                if (player == Players.player1)
                {
                    settingWall[0] = Instantiate(player1HWall);
                    settingWall[0].transform.position += new Vector3(wall.wallTop.y / 2 * scale, 0, (wall.wallTop.x - 1) / 2 * scale);
                    settingWall[1] = Instantiate(player1HWall);
                    settingWall[1].transform.position += new Vector3(wall.wallBottom.y / 2 * scale, 0, (wall.wallBottom.x - 1) / 2 * scale);
                }
                else
                {
                    settingWall[0] = Instantiate(player2HWall);
                    settingWall[0].transform.position += new Vector3(wall.wallTop.y / 2 * scale, 0, (wall.wallTop.x - 1) / 2 * scale);
                    settingWall[1] = Instantiate(player2HWall);
                    settingWall[1].transform.position += new Vector3(wall.wallBottom.y / 2 * scale, 0, (wall.wallBottom.x - 1) / 2 * scale);
                }
            }
        }

        /// <summary>
        /// ?s???????????????????i???????\?b?h
        /// </summary>
        /// <param name="action">?s??</param>
        /// <param name="playerID">?v???[???[????</param>
        public void drawAction(Players player, MoveActions action)
        {
            Position initialPosition = gameController.getPlayerPosition(player);
            if (player == Players.player1)
            {
                player1.GetComponent<Player1Kojila>().SetTargetPosition(action, initialPosition);
            }
            else
            {
                player2.GetComponent<Player2Kojila>().SetTargetPosition(action, initialPosition);
            }
        }

        public void showWinner(Players player)
        {
            if(player == Players.player1)
            {
                finishText.text = "player1 Win!";
            }
            else
            {
                finishText.text = "player2 Win!";
            }
            finishText.gameObject.SetActive(true);
            finishButton.gameObject.SetActive(true);
        }

        public int getScale()
        {
            return scale;
        }


        public CameraState getCameraState()
        {
            return cameraState;
        }

        public void moveCamera(CameraState state)
        {
            cameraState = state;

            Vector3 nextPosition;
            Quaternion nextRotation;
            switch (cameraState)
            {
                case CameraState.overlooking:
                    nextPosition = new Vector3(7 * scale, 10 * scale, 4 * scale);
                    nextRotation = Quaternion.Euler(75, -90, 0);
                    break;
                case CameraState.player1View:
                    nextPosition = player1.transform.position + new Vector3(0, 2, 0);
                    nextRotation = player1.transform.rotation;
                    break;
                case CameraState.player2View:
                    nextPosition = player2.transform.position + new Vector3(0, 2, 0);
                    nextRotation = player2.transform.rotation;
                    break;
                default:
                    return;
            }
            cameraMover.MovePosition(nextPosition, nextRotation);
        }

        public void rotateCameraRight()
        {
            cameraMover.RotateRight();
        }

        public void rotateCameraLeft()
        {
            cameraMover.RotateLeft();
        }
    }

}
