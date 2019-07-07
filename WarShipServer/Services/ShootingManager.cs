using System;
using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class ShootingManager
    {
        private readonly Game _game;
        private static int _lastShotSquareNumber = -1;
        private static Enum _shotDirection;

        public ShootingManager(Game game)
        {
            if (_game == null)
            {
                _game = game;
            }
        }
        
        public Square MakeComputerShot()
        {
            int currentShotSquareNumber;
            
            if (_lastShotSquareNumber == -1)
            {
                Random random = new Random();
                
                do
                {
                    currentShotSquareNumber = random.Next(100);
                } while (_game.PlayerField.Squares[currentShotSquareNumber].IsClicked);
                
                if (_game.PlayerField.Squares[currentShotSquareNumber].ShipNumber == -1)
                {
                    _lastShotSquareNumber = currentShotSquareNumber;
                }
                
            }
            else
            {
                currentShotSquareNumber = MakeRoundShot(_lastShotSquareNumber);
            }
            
            _game.PlayerField.Squares[currentShotSquareNumber].IsClicked = true;

            if (_game.PlayerField.Squares[currentShotSquareNumber].ShipNumber == -1)
            {
                _game.IsPlayerTurn = true;
            }

            return _game.PlayerField.Squares[currentShotSquareNumber];
        }

        public int MakeRoundShot(int currentSquareNumber)
        {
            if (!_game.PlayerField.Squares[currentSquareNumber - 1].IsClicked)
            {
                _shotDirection = Direction.Horizontally;
                return currentSquareNumber - 1;
            }
            if (!_game.PlayerField.Squares[currentSquareNumber - 10].IsClicked)
            {
                _shotDirection = Direction.Vertically;
                return currentSquareNumber - 10;
            }
            if (!_game.PlayerField.Squares[currentSquareNumber + 1].IsClicked)
            {
                _shotDirection = Direction.Horizontally;
                return currentSquareNumber + 1;
            }
           
            _shotDirection = Direction.Vertically;
            return currentSquareNumber + 10;
            
        }
        
        public int MakeDirectComputerShot()
        {
            int shotPoint = MakeLeftShot(_lastShotSquareNumber);

            if (_game.PlayerField.Squares[_lastShotSquareNumber].ShipNumber > -1)
            {
                _lastShotSquareNumber = shotPoint;
            }
            else
            {
                
            }

            return shotPoint;
        }

        public int MakeLeftShot(int currentSquareNumber)
        {
            return currentSquareNumber - 1;
        }

        private void Reset()
        {
            _lastShotSquareNumber = -1;
        }
    }
}