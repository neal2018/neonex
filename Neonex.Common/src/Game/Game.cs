namespace Neonex.Common;
using System.Linq;

public class Game
{
    public const int PlayerCount = 2;
    public const int RoundCount = 3;
    public static Random rng = new();
    public readonly List<RoundStartInfo> RoundStartInfos = new(){
                new RoundStartInfo { DrawCount = 10, MulliganCount = 3 },
                new RoundStartInfo { DrawCount = 2, MulliganCount = 2 },
                new RoundStartInfo { DrawCount = 1, MulliganCount = 1 }
            };
    List<PlayerInGameInfo> PlayerInGameInfos { get; set; }
    public Game(Player player1, Player player2)
    {
        PlayerInGameInfos = new() {
            new PlayerInGameInfo(player1),
            new PlayerInGameInfo(player2),
        };
    }
    public void Start()
    {
        int player1WinCount = 0;
        int player2WinCount = 0;
        foreach (var playerInfo in PlayerInGameInfos)
        {
            foreach (var cardPoint in Enumerable.Range(1, 10))
            {
                foreach (var _ in Enumerable.Range(0, 4))
                {
                    playerInfo.Deck.Cards.Add(new Card { Points = cardPoint });
                }
            }
            playerInfo.Deck.Cards = playerInfo.Deck.Cards.OrderBy(a => rng.Next()).ToList();
        }
        var roundStartInfos = new List<RoundStartInfo> {
            new RoundStartInfo { DrawCount = 10, MulliganCount = 3 },
            new RoundStartInfo { DrawCount = 2, MulliganCount = 2 },
            new RoundStartInfo { DrawCount = 1, MulliganCount = 1 }
        };

        for (int curRound = 0; curRound < RoundCount; curRound++)
        {
            var roundStartInfo = roundStartInfos[curRound];
            foreach (var playerInfo in PlayerInGameInfos)
            {
                foreach (var _ in Enumerable.Range(0, roundStartInfo.DrawCount))
                {
                    playerInfo.Hand.Cards.Add(playerInfo.Deck.Cards[0]);
                    playerInfo.Deck.Cards.RemoveAt(0);
                }
                foreach (var _ in Enumerable.Range(0, roundStartInfo.MulliganCount))
                {
                    var mulliganCardIndex = playerInfo.player.Mulligan();
                    var mulliganCard = playerInfo.Hand.Cards[mulliganCardIndex];
                    playerInfo.Deck.Cards.Add(mulliganCard);

                    playerInfo.Hand.Cards[mulliganCardIndex] = playerInfo.Deck.Cards[0];
                    playerInfo.Deck.Cards.RemoveAt(0);
                }
            }

            int curPlayerId = 0;
            while (!IsRoundEnd())
            {
                RunPlayerTurn(curPlayerId);
                curPlayerId = 1 - curPlayerId;
            }

            if (PlayerInGameInfos[0].TotalPoints > PlayerInGameInfos[0].TotalPoints)
            {
                player1WinCount++;
            }
            else if (PlayerInGameInfos[0].TotalPoints > PlayerInGameInfos[0].TotalPoints)
            {
                player2WinCount++;
            }
            else
            {
                player1WinCount++;
                player2WinCount++;
            }
        }
        Console.WriteLine($"{player1WinCount}, {player2WinCount}");
    }
    public void RunPlayerTurn(int curPlayerId)
    {
        var curPlayerInfo = PlayerInGameInfos[curPlayerId];
        if (curPlayerInfo.IsPassed) { return; }
        if (curPlayerInfo.Hand.Cards.Count == 0)
        {
            curPlayerInfo.IsPassed = true;
            return;
        }
        var playerRespond = curPlayerInfo.player.PlayCard();
        if (playerRespond.Pass)
        {
            curPlayerInfo.IsPassed = true;
            return;
        }
        var cardToPlay = curPlayerInfo.Hand.Cards[playerRespond.PlayCardId];
        curPlayerInfo.Hand.Cards.RemoveAt(playerRespond.PlayCardId);
        Console.WriteLine(playerRespond.PlayRowId);
        Console.WriteLine(curPlayerInfo.GameBoard.Board.Count());
        curPlayerInfo.GameBoard.Board[playerRespond.PlayRowId].Cards.Insert(playerRespond.PlayColumnId, cardToPlay);
    }
    public bool IsRoundEnd()
    {
        return PlayerInGameInfos.All(x => x.IsPassed);
    }
}
