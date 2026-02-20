using Assets.Scripts.Game.Tiles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class AnimationManager : IAnimation, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;


        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        public void AnimateTile(Tile tile, float value)
        {
            tile.transform.DOScale(value, 0.3f).SetEase(Ease.OutCubic);
            
        }

        public void DoPunchAnimate(GameObject target, Vector3 scale, float duration)
        {
            target.transform.DOPunchScale(scale, duration, 1, 0.5f);
        }

        public async UniTask HideTile(GameObject target, float delay)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            target.transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.OutBounce);
            target.SetActive(false);
            target.transform.localScale = Vector3.one;
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f), _cancellationTokenSource.IsCancellationRequested);
            _cancellationTokenSource.Cancel();
        }

        public void MoveTile(Tile tile, Vector3 position, Ease ease)
        {
            tile.transform.DOLocalMove(position, 0.2f).SetEase(ease);
        }

        public void MoveUI(RectTransform target, Vector3 position, float duration, Ease ease)
        {
            target.DOAnchorPos(position, duration).SetEase(ease);
        }

        public async UniTask Reveal(GameObject target, float deley)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            target.transform.localScale = Vector3.one * 0.1f;
            await target.transform.DOScale(Vector3.one, deley).SetEase(Ease.OutBounce);
            _cancellationTokenSource.Cancel();
        }
    }
}
