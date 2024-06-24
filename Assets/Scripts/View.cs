using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TreasureHunting
{
    public class View : MonoBehaviour
    {
        GameController gameController;
        public Camera mainCamera;
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

        GameObject floatingWall;
        bool floatingWallVertical;

        public TextMeshProUGUI turnText;

        public Text playerSelectText;
        public Button playerButton;
        public Button cpuButton;

        public GameObject wallSettingPanel;
        public GameObject searchPanel;
        public GameObject cpuPanel;
        GameObject currentPanel;

        public GameObject alertPanel;

        public TextMeshProUGUI finishText;
        public Button finishButton;

        const int scale = 4;


        // Start is called before the first frame update
        void Start()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }

        public void setPlayerSelectCanvasVisible(bool flag)
        {
            playerSelectText.gameObject.SetActive(flag);
            playerButton.gameObject.SetActive(flag);
            cpuButton.gameObject.SetActive(flag);
        }

        public void setPlayerFlag(bool flag)
        {
            gameController.setFlag(flag);
        }

        public void setPlayerText(string str)
        {
            playerSelectText.text = str;
        }

        public void wallSetting(Wall wall)
        {
            gameController.WallSetting(wall);
        }

        public void search(MoveActions action)
        {
            gameController.search(action);
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
                            if(j % 2 == 1 && i % 2 == 1)
                            {
                                planes[(j - 1) / 2, (i - 1) / 2] = Instantiate(plane);//???????????A?z?????i?[
                                planes[(j - 1) / 2, (i - 1) / 2].transform.position += new Vector3((i - 1) / 2 * scale, 0, (j - 1) / 2 * scale);//???????u???z????????????????????
                            }
                            break;
                        case StateAtrribute.wall:
                            if (j % 2 == 0 && i % 2 == 1)
                            {
                                GameObject wall = Instantiate(vWall);
                                wall.transform.position += new Vector3((i - 1) / 2 * scale, 0, j / 2 * scale);
                            }
                            else if(j % 2 == 1 && i % 2 == 0)
                            {
                                GameObject wall = Instantiate(hWall);
                                wall.transform.position += new Vector3(i / 2 * scale, 0, (j - 1) / 2 * scale);
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

        public void setNextSituation(Players player, Fases fase, bool playable)
        {
            if(currentPanel != null)
            {
                currentPanel.SetActive(false);
            }
            if(playable)
            {
                if(fase == Fases.wallSetting)
                {
                    currentPanel = wallSettingPanel;

                    floatingWallVertical = true;
                    if (player == Players.player1)
                    {
                        floatingWall = Instantiate(player1VWall);
                    }
                    else
                    {
                        floatingWall = Instantiate(player2VWall);
                    }

                    floatingWall.transform.position += new Vector3(2 / 2 * scale, 0.5f * scale, 2 / 2 * scale);
                }
                else if(fase == Fases.search )
                {
                    currentPanel = searchPanel;
                }
            }
            else
            {
                currentPanel = cpuPanel;
            }

            turnText.SetText("Player" + ((int)player + 1) + "'s Turn");
            currentPanel.SetActive(true);
        }

        public void moveFloatingWall(MoveActions action)
        {
            switch(action)
            {
                case MoveActions.up:
                    floatingWall.transform.position -= new Vector3(2 / 2 * scale, 0, 0);
                    break;
                case MoveActions.down:
                    floatingWall.transform.position += new Vector3(2 / 2 * scale, 0, 0);
                    break;
                case MoveActions.left:
                    floatingWall.transform.position -= new Vector3(0, 0, 2 / 2 * scale);
                    break;
                default:
                    floatingWall.transform.position += new Vector3(0, 0, 2 / 2 * scale);
                    break;
            }
        }

        public void rotateFloatingWall()
        {
            GameObject tmp;
            if (gameController.getCurrentPlayer() == Players.player1)
            {
                if (floatingWallVertical)
                {
                    tmp = Instantiate(player1HWall);
                    floatingWallVertical = false;
                }
                else
                {
                    tmp = Instantiate(player1VWall);
                    floatingWallVertical = true;
                }
            }
            else
            {
                if (floatingWallVertical)
                {
                    tmp = Instantiate(player2HWall);
                    floatingWallVertical = false;
                }
                else
                {
                    tmp = Instantiate(player2VWall);
                    floatingWallVertical = true;
                }
            }
            tmp.transform.position = floatingWall.transform.position;
            Destroy(floatingWall);
            floatingWall = tmp;
        }

        public void putWall()
        {
            int x = (int)((floatingWall.transform.position.z + 2) * 2 / scale);
            int y = (int)((floatingWall.transform.position.x + 2) * 2 / scale);
            Position center = new Position(x, y);
            Position top;
            Position bottom;
            if (floatingWallVertical)
            {
                top = new Position(x, y - 1);
                bottom = new Position(x, y + 1);
            }
            else
            {
                top = new Position(x - 1, y);
                bottom = new Position(x + 1, y);
            }
            Destroy(floatingWall);
            gameController.WallSetting(new Wall(center, top, bottom));
        }

        public void drawWall(Players player, Wall wall)
        {
            GameObject settingWall;

            if (wall.wallTop.x == wall.wallBottom.x)
            {
                if (player == Players.player1)
                {
                    settingWall = Instantiate(player1VWall);
                }
                else
                {
                    settingWall = Instantiate(player2VWall);
                }
            }
            else
            {
                if (player == Players.player1)
                {
                    settingWall = Instantiate(player1HWall);
                }
                else
                {
                    settingWall = Instantiate(player2HWall);
                }
            }

            settingWall.transform.position += new Vector3(wall.center.y / 2 * scale, 0, wall.center.x / 2 * scale);
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

        public void showAlert()
        {
            alertPanel.SetActive(true);
            gameController.setStandby(true);
        }

        public void hidehAlert()
        {
            alertPanel.SetActive(false);
            gameController.setStandby(false);
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
            CameraMover cameraMover = mainCamera.GetComponent<CameraMover>();
            cameraMover.MovePosition(nextPosition, nextRotation);
        }

        public void rotateCameraRight()
        {
            CameraMover cameraMover = mainCamera.GetComponent<CameraMover>();
            cameraMover.RotateRight();
        }

        public void rotateCameraLeft()
        {
            CameraMover cameraMover = mainCamera.GetComponent<CameraMover>();
            cameraMover.RotateLeft();
        }
    }

}
