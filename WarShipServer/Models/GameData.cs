using System.Collections.Generic;

namespace WarShipServer.Models
{
    public class GameData
    {
        public Board[] Boards { get; set; }
        public int PlayerBoardId { get; set; }
        public int EnemyBoardId { get; set; }
        public bool IsPlayerTurn { get; set; }
        
        public bool IsFree { get; set; }

        public GameData(int playerBoardId, int enemyBoardId, bool isPlayerTurn,
            bool isFirstPlayerReady, bool isSecondPlayerReady)
        {
            Boards = new []{new Board(0, isFirstPlayerReady), new Board(1, isSecondPlayerReady)};
            PlayerBoardId = playerBoardId;
            EnemyBoardId = enemyBoardId;
            IsPlayerTurn = isPlayerTurn;
            IsFree = true;
        }
    }
}