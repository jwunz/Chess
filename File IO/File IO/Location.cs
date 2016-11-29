namespace Chess
{
    class Location
    {
        public byte XCoordinate { get; set; }
        public byte YCoordinate { get; set; }

        private Piece piece;

        public Piece getPiece()
        {
            return this.piece;
        }

        public void setPiece(Piece pieceToSet)
        {
            this.piece = pieceToSet;

            if (pieceToSet != null)
            {
                this.piece.location = this;
            }
        }

        public Location(byte x, byte y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }
    }
}