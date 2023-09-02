namespace Neonex.Common;


public class Player
{
    public int Mulligan()
    {
        return 0;
    }
    public PlayerCommand PlayCard()
    {
        return new PlayerCommand { Pass = false, PlayCardId = 0, PlayRowId = 0, PlayColumnId = 0 };
    }
}
