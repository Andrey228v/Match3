using System;

namespace Assets.Scripts.Game.Score
{
    public class GameProgress
    {
        public event Action OnScoreChanged;
        public event Action OnMove;

        public int Score { get; private set; }
        public int GoalScore { get; private set; }
        public int Moves { get; private set; }

        public void LoadLevelConfig(int goalScore, int moves)
        {
            GoalScore = goalScore;
            Moves = moves;
            Score = 0;
        }

        public void AddScore(int value)
        {
            if(value < 0) throw new ArgumentOutOfRangeException(nameof(value));

            Score += value;
            OnScoreChanged?.Invoke();
        }

        public bool CheckGoalScore()
        {
            return Score >= GoalScore;
        }

        public void SpendMove()
        {
            Moves--;
            OnMove?.Invoke();
        }
    }
}
