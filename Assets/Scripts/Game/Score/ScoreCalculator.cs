using Assets.Scripts.Game.MatchTiles;

namespace Assets.Scripts.Game.Score
{
    public class ScoreCalculator
    {
        private GameProgress _gameProgress;

        public ScoreCalculator(GameProgress gameProgress)
        {
            _gameProgress = gameProgress;
        }

        public void CalcilateScoreToAdd(MatchDirection matchDirection)
        {
            if (matchDirection == MatchDirection.Horizontal || matchDirection == MatchDirection.Vertical)
            {
                _gameProgress.AddScore(20);
            }
            else if (matchDirection == MatchDirection.LongHorizontal || matchDirection == MatchDirection.LongVertical)
            {
                _gameProgress.AddScore(50);
            }
            else if (matchDirection == MatchDirection.Multiply) 
            {
                _gameProgress.AddScore(200);
            }
        }
    }
}
