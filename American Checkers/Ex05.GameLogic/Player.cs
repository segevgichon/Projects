using System;
using System.Collections.Generic;

namespace Ex05.GameLogic
{
    public class Player
    {
        public string PlayerName { get; set; }
        public eBoardOption PlayerCoin { get; set; }
        public eBoardOption PlayerKing { get; set; }

        public Player(string i_PlayerName, eBoardOption i_PlayerCoin, eBoardOption i_PlayerKing)
        {
            PlayerName = i_PlayerName;
            PlayerCoin = i_PlayerCoin;
            PlayerKing = i_PlayerKing;
        }

        public List<BoardCell[]> GetAllValidMoves(Board i_Board)
        {
            return i_Board.GetAllPlayerValidMoves(PlayerCoin, PlayerKing);
        }

        public List<BoardCell[]> GetCellJumpMoves(BoardCell i_Cell, Board i_Board)
        {
            return i_Board.GetCellJumpMoves(i_Cell, PlayerCoin, PlayerKing);
        }

        public BoardCell[] GetCPUMove(List<BoardCell[]> i_LegalMoves)
        {
            int randomIdx = new Random().Next(i_LegalMoves.Count);
            return i_LegalMoves[randomIdx];
        }
    }
}
