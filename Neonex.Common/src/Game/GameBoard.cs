namespace Neonex.Common;
using System.Linq;

public class GameBoard
{
    public const int RowCount = 3;
    public List<CardList> Board = new(new CardList[RowCount]);
    public int TotalPoints
    {
        get
        {
            return Board.Aggregate(0, (acc, x) => acc + x.Cards.Aggregate(0, (acc, x) => acc + x.Points));
        }
    }
}
