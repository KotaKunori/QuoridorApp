using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunting
{
    public class GameController : MonoBehaviour
    {
        public View view;
        Player[] player = new Player[2];
        Board board;

        private int errorLimit = 10000;
        private Players currentPlayer = Players.player1;//???????v???[???[???????A???????????v???[???[?P
        private Players enemyPlayer = Players.player2;
        private bool standby = true;
        private Fases fase = Fases.wallSetting;

        private List<bool> playable = new List<bool>();

        // Start is called before the first frame update
        void Start()
        {
            player[0] = new Player(Players.player1, new W_Wall_Algorithm(), new DemoMoveAlgorithm());
            player[1] = new Player(Players.player2, new W_Wall_Algorithm(), new DemoMoveAlgorithm());
            board = new Board();
            view.drawInitialState(board.getState());//?????????????????????A???????????`??
            if(playable.Count < 2)
            {
                view.setPlayerSelectCanvasVisible(true);
            }
            else
            {
                fase = Fases.wallSetting;
                view.setNextSituation(currentPlayer, fase, playable[(int)currentPlayer]);
            }
        }

        public void setFlag(bool flag)
        {
            if(playable.Count == 0)
            {
                view.setPlayerText("Choose Player2");
            }
            else
            {
                view.setPlayerSelectCanvasVisible(false);
                view.setPlayerText("Choose Player1");
                standby = false;
                view.setNextSituation(currentPlayer, fase, playable[(int)currentPlayer]);
            }
            playable.Add(flag);
        }

        void changePlayer()
        {
            Players tmp = currentPlayer;
            currentPlayer = enemyPlayer;
            enemyPlayer = tmp;
        }

        public void setStandby(bool flag)
        {
            standby = flag;
        }

        public void WallSetting(Wall wall)
        {
            if (!standby)
            {
                if (board.wallSettingEnable(wall))//enableAction(action): ?s???????\?????????????????\?b?h
                {
                    board.wallSetting(currentPlayer, wall);//?v???[???[???s?????????N???X???`?B???A???????X?V
                    view.drawWall(currentPlayer, wall);//???????u???`??

                    changePlayer();

                    if (board.getRestWallCount() == 0)//?????????????O?????v???[???[?Q?????t???O??????
                    {
                        standby = true;
                        playable = new List<bool>();
                        view.setPlayerSelectCanvasVisible(true);

                        fase = Fases.search;
                        player[0].setInitialState(board.getPlayerPosition(Players.player1), board.getTreasurePosition());
                        player[1].setInitialState(board.getPlayerPosition(Players.player2), board.getTreasurePosition());
                    }
                }
                else
                {
                    view.showAlert();
                }
            }
            if(playable.Count == 2)
            {
                view.setNextSituation(currentPlayer, fase, playable[(int)currentPlayer]);
            }
        }

        public void search(MoveActions action)
        {
            if (!standby)
            {
                if (board.moveActionEnable(currentPlayer, action))//?s???????\?????????????????\?b?h
                {
                    board.moveAction(currentPlayer, action);//?v???[???[???s?????????N???X???`?B???A???????X?V
                    view.drawAction(currentPlayer, action);
                    if (board.getPlayerPosition(currentPlayer) == board.getTreasurePosition())//?????v???[???[?????????????????????I??
                    {
                        fase = Fases.end;
                        view.showWinner(currentPlayer);
                    }

                    if (board.getPlayerPosition(currentPlayer) != board.getPlayerPosition(enemyPlayer))
                    {
                        changePlayer();
                    }
                }
                else
                {
                    view.showAlert();
                }
            }

            view.setNextSituation(currentPlayer, fase, playable[(int)currentPlayer]);
        }
             
        public void ContinueGame()
        {
            if(standby)
            {
                return;
            }
            int errorCount;
            if (fase == Fases.wallSetting)//?????u?t?F?[?Y??????
            {
                State currentState = board.getState();//?????N???X??????????????????
                Wall wall = new Wall();//???????W????????????
                for (errorCount = 0; errorCount < errorLimit; errorCount++)
                {
                    wall = player[(int)currentPlayer].Call_getWallPosition(currentState);//?????????????????A?v???[???[A???????W??????????
                    if (board.wallSettingEnable(wall))//enableAction(action): ?s???????\?????????????????\?b?h
                    {
                        break;
                    }
                }
                if (errorCount == errorLimit)//?K???????????G???[???m?F???????????????s?k
                {
                    fase = Fases.end;
                    view.showWinner(enemyPlayer);
                    return;
                }
                board.wallSetting(currentPlayer, wall);//?v???[???[???s?????????N???X???`?B???A???????X?V
                view.drawWall(currentPlayer, wall);//???????u???`??
                changePlayer();

                if (board.getRestWallCount() == 0)//?????????????O?????v???[???[?Q?????t???O??????
                {
                    standby = true;
                    playable = new List<bool>();
                    view.setPlayerSelectCanvasVisible(true);

                    fase = Fases.search;
                    player[0].setInitialState(board.getPlayerPosition(Players.player1), board.getTreasurePosition());
                    player[1].setInitialState(board.getPlayerPosition(Players.player2), board.getTreasurePosition());
                }
                else
                {
                    view.setNextSituation(currentPlayer, fase, playable[(int)currentPlayer]);
                }
            }
            else if (fase == Fases.search) {
                VisibleState currentState = board.getVisibleState(currentPlayer);//?????N???X??????????????????
                MoveActions action = MoveActions.up;
                for (errorCount = 0; errorCount < errorLimit; errorCount++)
                {
                    action = player[(int)currentPlayer].Call_getAction(currentState);//?????????????????A?v???[???[A???s????????????
                    if (board.moveActionEnable(currentPlayer, action))//?s???????\?????????????????\?b?h
                    {
                        break;
                    }
                }
                if (errorCount == errorLimit)//?K???????????G???[???m?F???????????????s?k
                {
                    fase = Fases.end;
                    view.showWinner(enemyPlayer);
                }
                board.moveAction(currentPlayer, action);//?v???[???[???s?????????N???X???`?B???A???????X?V
                view.drawAction(currentPlayer, action);
                if (board.getPlayerPosition(currentPlayer) == board.getTreasurePosition())//?????v???[???[?????????????????????I??
                {
                    fase = Fases.end;
                    view.showWinner(currentPlayer);
                }
                if (board.getPlayerPosition(currentPlayer) != board.getPlayerPosition(enemyPlayer))
                {
                    changePlayer();
                }
                else
                {
                    ContinueGame();
                }

                view.setNextSituation(currentPlayer, fase, playable[(int)currentPlayer]);
            }
        }

        public Position getPlayerPosition(Players player)
        {
            return board.getPlayerPosition(player);
        }

        public Players getCurrentPlayer()
        {
            return currentPlayer;
        }
    }
}
