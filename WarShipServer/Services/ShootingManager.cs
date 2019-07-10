using System;
using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class ShootingManager
    {
        private readonly Game _game;
        private readonly PointsValidator _pointsValidator;
        private const int DefaultInt = -1;
        private static int _lastHitPoint = DefaultInt;
        private static int _firstHitPoint = DefaultInt;
        private static Enum _roundShotDirection;
        private static Enum _shipPosition;
        private static Enum _shotSide;

        public ShootingManager(Game game, PointsValidator pointsValidator)
        {
            if (_game == null)
            {
                _game = game;
            }

            _pointsValidator = pointsValidator;
        }

        public Square MakeComputerShot()
        {
            int currentPoint;

            if (_firstHitPoint == DefaultInt)
            {
                currentPoint = GetRandomPoint();

                MakeShot(_game.PlayerField, _game.PlayerFleet, currentPoint);

                if (_game.PlayerField.Squares[currentPoint].ShipNumber != DefaultInt)
                {
                    if (!IsShipAlive(currentPoint))
                    {
                        CleanShotMemory();
                    }
                    else
                    {
                        _firstHitPoint = currentPoint;
                    }
                }
                else
                {
                    _game.IsPlayerTurn = true;
                }
            }
            else
            {
                if (_shipPosition != null)
                {
                    currentPoint = _shipPosition.Equals(Direction.Horizontally) ? 
                        MakeHorizontallyShot() :
                        MakeVerticallyShot();
                }
                else
                {
                    currentPoint = GetRoundPoint(_firstHitPoint);

                    MakeShot(_game.PlayerField, _game.PlayerFleet, currentPoint);

                    if (_game.PlayerField.Squares[currentPoint].ShipNumber != DefaultInt)
                    {
                        if (!IsShipAlive(currentPoint))
                        {
                            CleanShotMemory();
                        }
                        else
                        {
                            _lastHitPoint = currentPoint;
                            
                            if (_roundShotDirection.Equals(Direction.Left) ||
                                _roundShotDirection.Equals(Direction.Right))
                            {
                                _shipPosition = Direction.Horizontally;
                            }
                            else
                            {
                                _shipPosition = Direction.Vertically;
                            }
                        }
                    }
                    else
                    {
                        _game.IsPlayerTurn = true;
                    }
                }
            }

            return _game.PlayerField.Squares[currentPoint];
        }

        public int GetRandomPoint()
        {
            Random random = new Random();
            int point;

            do
            {
                point = random.Next(100);
            } while (_game.PlayerField.Squares[point].IsClicked);

            return point;
        }

        public int GetRoundPoint(int middlePoint)
        {
            if (CheckPointForShot(middlePoint - 1, middlePoint / 10))
            {
                _roundShotDirection = Direction.Left;
                _shotSide = Direction.Left;
                return middlePoint - 1;
            }

            if (CheckPointForShot(middlePoint - 10, -1))
            {
                _roundShotDirection = Direction.Top;
                _shotSide = Direction.Top;
                return middlePoint - 10;
            }

            if (CheckPointForShot(middlePoint + 1, middlePoint / 10))
            {
                _roundShotDirection = Direction.Right;
                _shotSide = Direction.Right;
                return middlePoint + 1;
            }

            _roundShotDirection = Direction.Buttom;
            _shotSide = Direction.Buttom;
            return middlePoint + 10;
        }

        public int MakeHorizontallyShot()
        {
            int currentPoint = DefaultInt;

            if (_shotSide.Equals(Direction.Left))
            {
                currentPoint = _lastHitPoint - 1;
                
                if (!CheckPointForShot(currentPoint, _lastHitPoint / 10))
                {
                    _lastHitPoint = _firstHitPoint;
                    _shotSide = Direction.Right;
                }
            }

            if (_shotSide.Equals(Direction.Right))
            {
                currentPoint = _lastHitPoint + 1;
            }

            MakeShot(_game.PlayerField, _game.PlayerFleet, currentPoint);

            if (_game.PlayerField.Squares[currentPoint].ShipNumber != DefaultInt)
            {
                if (!IsShipAlive(currentPoint))
                {
                    CleanShotMemory();
                }
                else
                {
                    _lastHitPoint = currentPoint;
                }
            }
            else
            {
                _lastHitPoint = _firstHitPoint;
                
                _shotSide = _shotSide.Equals(Direction.Left) ? Direction.Right : Direction.Left;
                _game.IsPlayerTurn = true;
                
            }

            return currentPoint;
        }

        public int MakeVerticallyShot()
        {
            int currentPoint = DefaultInt;

            if (_shotSide.Equals(Direction.Top))
            {
                currentPoint = _lastHitPoint - 10;
                if (!CheckPointForShot(currentPoint, DefaultInt))
                {
                    _lastHitPoint = _firstHitPoint;
                    _shotSide = Direction.Buttom;
                }
            }

            if (_shotSide.Equals(Direction.Buttom))
            {
                currentPoint = _lastHitPoint + 10;
            }

            MakeShot(_game.PlayerField, _game.PlayerFleet, currentPoint);

            if (_game.PlayerField.Squares[currentPoint].ShipNumber != DefaultInt)
            {
                if (!IsShipAlive(currentPoint))
                {
                    CleanShotMemory();
                }
                else
                {
                    _lastHitPoint = currentPoint;
                }
            }
            else
            {
                _lastHitPoint = _firstHitPoint;

                _shotSide = _shotSide.Equals(Direction.Top) ? Direction.Buttom : Direction.Top;
                
                _game.IsPlayerTurn = true;
            }

            return currentPoint;
        }

        private void MakeShot(Field field, Fleet fleet, int point)
        {
            field.Squares[point].IsClicked = true;

            if (field.Squares[point].ShipNumber != DefaultInt)
            {
                fleet.Ships[field.Squares[point].ShipNumber].HitsNumber++;
            }
        }


        public bool CheckPointForShot(int squareNumber, int rowNumber)
        {
            if (_pointsValidator.ValidatePointForBounds(squareNumber) &&
                !_game.PlayerField.Squares[squareNumber].IsClicked)
            {
                if (rowNumber != DefaultInt)
                {
                    return squareNumber / 10 == rowNumber;
                }

                return true;
            }

            return false;
        }

        private bool IsShipAlive(int currentPoint)
        {
            var shipId = _game.PlayerField.Squares[currentPoint].ShipNumber;
            return _game.PlayerFleet.Ships[shipId].IsAlive;
        }

        private void CleanShotMemory()
        {
            _shipPosition = null;
            _shotSide = null;
            _roundShotDirection = null;
            _firstHitPoint = DefaultInt;
            _lastHitPoint = DefaultInt;
        }
    }
}