namespace TicTacToeLogic
{
    public struct Player
    {
        private readonly char r_Symbol;
        private readonly int r_PlayerId;
        private int m_Score;

        public Player(int i_PlayerId, char i_Symbol)
        {
            r_PlayerId = i_PlayerId;
            r_Symbol = i_Symbol;
            m_Score = 0;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public int PlayerId
        {
            get
            {
                return r_PlayerId;
            }
        }

        public char Symbol
        {
            get
            {
                return r_Symbol;
            }
        }
    }
}