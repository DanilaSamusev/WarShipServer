using System;
using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class Game
    {
        public Field PlayerField { get; set; }
        public Field ComputerField { get; set; }
        public bool IsPlayerTurn { get; set; }
        

        public Game(ShipsAligner shipsAligner)
        {
            PlayerField = new Field();
            ComputerField = new Field();
            shipsAligner.PlantShipsRandom(ComputerField);
            IsPlayerTurn = true;
        }

        public Square[][] MakeShooting(int id)
        {
            Square[] computerSquares = MakePlayerShot(id);
            Square[] playerSquares = null;

            if (!computerSquares[0].HasShip)
            {
                playerSquares = MakeComputerShot();
            }
            
            return new[] {computerSquares, playerSquares};
        }
        
        public Square[] MakePlayerShot(int id)
        {
            Square computerSquare = ComputerField.Squares[id];

            if (computerSquare.IsClicked)
            {
                return null;
            }

            computerSquare.IsClicked = true;
            
            return new[]{computerSquare};
        }
        
        public Square[] MakeComputerShot()
        {
            Random random = new Random();
            int id;

            do
            {
                id = random.Next(100);
            } while (PlayerField.Squares[id].IsClicked);

            PlayerField.Squares[id].IsClicked = true;
            IsPlayerTurn = true;
            return new[] {PlayerField.Squares[id]};
        }
    }
}