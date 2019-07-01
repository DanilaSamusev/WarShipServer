using System;
using System.Collections.Generic;
using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class ShootingManager
    {
        private static int? lastComputerShotSquareId;
        private static bool DidComputerShotShip;
        
        public Square[] MakePlayerShot(Field computerField, int id)
        {
            Square computerSquare = computerField.Squares[id];

            if (computerSquare.IsClicked)
            {
                return null;
            }

            computerSquare.IsClicked = true;
            
            return new[]{computerSquare};
        }
        
        public Square[] MakeComputerShot(Field playerField)
        {
            List<Square> shotSquares = new List<Square>();

            Square shotSquare;

            do
            {
                shotSquare = MakeRandomShot(playerField);
                shotSquares.Add(shotSquare);
            } while (shotSquare.HasShip);
            
            
            return shotSquares.ToArray();
        }

        public Square MakeRandomShot(Field playerField)
        {
            Random random = new Random();
            int id;

            do
            {
                id = random.Next(100);
            } while (playerField.Squares[id].IsClicked);

            playerField.Squares[id].IsClicked = true;
            
            return playerField.Squares[id];
        }
    }
}