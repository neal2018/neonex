// See https://aka.ms/new-console-template for more information
using Neonex.Common;

var player1 = new Player();
var player2 = new Player();
var game = new Game(player1, player2);

game.Start();
