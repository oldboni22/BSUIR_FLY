using DG.Tweening;
using Pryanik.Res;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pryanik.Skinmenu
{
    public interface ISkinMenuController
    {
        void DisplayMenu();
    }
    public class SkinMenuController : MonoBehaviour, ISkinMenuController
    {
        
        private SkinStorage _storage;

        [Header("Components")]
        [SerializeField] private SkinButton _prefab;
        [SerializeField] private Transform _panel;
        [SerializeField] private RectTransform _menu;

        [Header("Animation")]
        [SerializeField] private float _dur;
        [SerializeField] private float _targetX;
        [SerializeField] private float _startX;
        [SerializeField] private GameObject _uiBlocker;


        private SkinButtonFactory _factory;
        #region Logic

        [Inject]
        public void Inject(SkinStorage storage)
        {
            _storage = storage;
        }

        private void Start()
        {
            Debug.Log("START");
            _factory ??= new SkinButtonFactory(_prefab,_panel);
            foreach (var skin in _storage.GetAll())
            {
                _factory.Construct().SetSkin(skin);
            }
        }
        #endregion

        #region visuals

        public void DisplayMenu()
        {
            _uiBlocker.SetActive(true);
            _menu.DOAnchorPosX(_targetX,_dur).OnComplete(() => _uiBlocker.SetActive(false));
        }
        public void CloseMenu()
        {
            _uiBlocker.SetActive(true);
            _menu.DOAnchorPosX(_startX, _dur).OnComplete(() => _uiBlocker.SetActive(false));
        }

        #endregion
    }

    public class SkinButtonFactory
    {
        public readonly SkinButton _prefab;
        public readonly Transform _transform;
        internal SkinButtonFactory(SkinButton prefab,Transform transform)
        {
            _prefab = prefab;
            _transform = transform;
        }
        public SkinButton Construct()
        {
            return GameObject.Instantiate(_prefab,_transform).GetComponent<SkinButton>();
        }
    }
}