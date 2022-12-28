namespace Ex05.GameLogic
{
    public struct BoardCell
    {
        public int RowIdx { get; set; }
        public int ColIdx { get; set; }

        public BoardCell(int i_RowIdx, int i_ColIdx)
        {
            RowIdx = i_RowIdx;
            ColIdx = i_ColIdx;
        }

        public bool Equals(BoardCell i_Cell)
        {
            return RowIdx == i_Cell.RowIdx && ColIdx == i_Cell.ColIdx;
        }
    }
}
