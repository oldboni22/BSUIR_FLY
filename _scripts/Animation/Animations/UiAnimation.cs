using DG.Tweening;
using UnityEngine;

namespace Pryanik.Animations.UI
{
    public class UiAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private AnimationStep[] _stepsSet;

        private Sequence _seq;
        public Sequence Sequence => _seq;
        private void Start() => ReadSteps();

        public void PlayForward() => _seq.PlayForward();
        public void PlayBackward() => _seq.PlayBackwards();

        #region StepReader

        void ReadSteps()
        {
            _seq = DOTween.Sequence().SetAutoKill(false);
            foreach (var step in _stepsSet)
            {
                ReadStep(step);
            }
            _stepsSet = null;
        }

        void ReadStep(AnimationStep step)
        {
            switch (step._type)
            {
                case AnimationType.Move:
                    ReadMove(step);
                    break;
                case AnimationType.Scale:
                    ReadScale(step);
                    break;
            }
        }

        void ReadMove(AnimationStep step) => _seq.Append(_transform.DOAnchorPos(step._target, step._duration));
        void ReadScale(AnimationStep step) => _seq.Append(_transform.DOScale(step._target, step._duration));
        #endregion

        private void OnDestroy()
        {
            _seq.Kill();
        }
    }
}