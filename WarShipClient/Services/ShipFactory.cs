using System;
using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class ShipFactory
    {       
        public Ship[] CreateShips(Square[] squares)
        {
            Ship[] ships = new Ship[4];

            Random random = new Random();

            for(int i = 0; i < 4;)
            {
                int number = random.Next(100);

                int[] points =
                {
                    number - 11, number - 10, number - 9,
                    number - 1, number + 1,
                    number + 9, number + 10, number + 11
                };

                if (CheckPoints(points, squares))
                {
                    Deck[] decks = new Deck[1];
                    decks[0] = new Deck(number);
                    Ship ship = new Ship("alive", decks);
                    ships[i] = ship;
                    squares[number].HasShip = true;
                    i++;
                }              
            }

            return ships;
        }
        
        private bool CheckPoints(int[] points, Square[] squares)
        {
            foreach (int point in points)
            {
                if (point >= 0 && point <= 100)
                {
                    if (squares[point].HasShip)
                    {
                        return false;
                    }
                }
            }

            return true;
        }   
    }
}