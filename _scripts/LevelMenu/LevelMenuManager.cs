using Pryanik.Layout;
using Pryanik.Progress;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Pryanik.LevelMenu
{
    public interface ILevelMenuManager
    {
        void Open();
    }
    public class LevelMenuManager : MonoBehaviour, ILevelMenuManager
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private LevelPanel _prefab;
        [SerializeField] private Transform _grid;

        private LevelPanelFactory _factory;
        private readonly List<LevelPanel> _levelPanels = new List<LevelPanel>();

        [Inject] private ISceneController _sceneController;
        [Inject] private IProgressSaver _saver;
        [Inject] private LayoutStorage _storage;

        private void Start()
        {
            _sceneController.OnSceneChanged += Close;
            _sceneController.OnMenuOpened += () => UpdateButton(PlayerPrefsManager.LevelId);
            _factory = new LevelPanelFactory(_prefab,_sceneController,_grid);
            ConstructButtons();
            UpdateAll();
        }

        void UpdateButton(string id)
        {
            _levelPanels.Single(p => p._lvlId == id).SetParams(_saver.GetProgress(id));
        }
        void UpdateAll()
        {
            int i = 0;
            foreach (var id in _storage.GetAll().Select(l => l.Id))
            {
                _levelPanels[i].SetParams(_saver.GetProgress(id));
                _levelPanels[i]._lvlId = id;
            }
        }
        void ConstructButtons()
        {
            foreach(var lvl in _storage.GetAll())
            {
               _levelPanels.Add(_factory.Construct());
            }
        }

        public void Open()
        {
            _panel.SetActive(true);
        }

        public void Close()
        {
            _panel.SetActive(false);
        }
    }




    public class LevelPanelFactory
    {
        private readonly LevelPanel _panel;
        private readonly ISceneController _sceneController;
        private readonly Transform _grid;

        public LevelPanelFactory(LevelPanel panel, ISceneController sceneController, Transform grid)
        {
            _sceneController = sceneController;
            _panel = panel;
            _grid = grid;
        }

        internal LevelPanel Construct()
        {
            LevelPanel panel = GameObject.Instantiate(_panel,_grid).GetComponent<LevelPanel>();
            panel.SetSceneController(_sceneController);
            return panel;
        }
    }
}