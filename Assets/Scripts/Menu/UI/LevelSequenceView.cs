using Assets.Scripts.Menu.Levels;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Menu.UI
{
    public class LevelSequenceView :MonoBehaviour
    {
        [SerializeField] private List<StartLevelButton> _levelsButton = new List<StartLevelButton>();

        private SetupLevelSequence _setupLevel;

        [Inject]
        public void Constructor(SetupLevelSequence levelConfig)
        {
            _setupLevel = levelConfig;
        }

        private void OnValidate()
        {
            if (_levelsButton.Count != 5)
            {
                throw new ArgumentOutOfRangeException("5 elem only");
            }
        }

        public void SetupButtonsView(int currentLevel)
        {
            //for(int i = 0; i < _levelsButton.Count; i++)
            //{
            //    _levelsButton[i].SetNumber(_setupLevel.CurrentLevelSequence.LevelSequence[i].LevelNumber);
            //    _levelsButton[i].SetLabel();

            //    if (_levelsButton[i].Number > currentLevel) 
            //    {
            //        _levelsButton[i].SetButtonInterectable(false);
            //    }
            //}
        }
    }
}
