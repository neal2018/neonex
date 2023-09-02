namespace Neonex.Common;


public class PlayerInGameInfo
{
    public Player player { get; init; }
    public CardList Deck { get; set; } = new CardList();
    public CardList Hand { get; set; } = new CardList();
    public CardList Graveyard { get; set; } = new CardList();
    public CardList Exile { get; set; } = new CardList();
    public GameBoard GameBoard = new();
    public int TotalPoints { get => GameBoard.TotalPoints; }
    public bool IsPassed { get; set; } = false;
    public PlayerInGameInfo(Player _player)
    {
        player = _player;
    }
}
