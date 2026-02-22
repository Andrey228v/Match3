using Assets.Scripts.Menu.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Scripts.Menu.UI
{
    public class StartLevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lebel;
        [SerializeField] private Button _button;

        private SetupLevelSequence _setupLevel;

        public int Number { get; private set; }

        [Inject]
        public void Constructor(SetupLevelSequence setupLevel)
        {
            _setupLevel = setupLevel;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(StartButtonClick);
        }

        private void OnDisable()
        {  
            _button.onClick.RemoveListener(StartButtonClick);
        }

        public void SetNumber(int number)
        {
            Mathf.Clamp(number, 1, 10);
            Number = number;
        }

        public void SetLabel()
        {
            _lebel.text = Number.ToString();
        }

        public void SetButtonInterectable(bool value)
        {
            _button.interactable = value;
        }

        private void StartButtonClick()
        {

        }
    }
}
