using Assets.Scripts.Game.Tiles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public interface IAnimation
    {
        public UniTask Reveal(GameObject target, float deley);

        public UniTask HideTile(GameObject target, float delay);

        public void DoPunchAnimate(GameObject target, Vector3 scale, float duration);

        public void MoveUI(RectTransform target, Vector3 position, float duration, Ease ease);

        public void AnimateTile(Tile tile, float value);

        public void MoveTile(Tile tile, Vector3 position, Ease ease);
    }
}
