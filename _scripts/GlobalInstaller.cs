
using Pryanik.Audio;
using Pryanik.Layout;
using Pryanik.LevelMenu;
using Pryanik.Progress;
using Pryanik.Res;
using Pryanik.Skinmenu;
using UnityEngine;
using Zenject;

namespace Pryanik
{

    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private AudioController _audioController;
        [SerializeField] private GameObject _audioPrefab;
        [SerializeField] private SettingsController _settings;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private SkinMenuController _skinMenuController;
        [SerializeField] private LevelMenuManager _levelMenuManager;

        [Header("Storages")]
        [SerializeField] private AudioStorage _audio;
        [SerializeField] private SkinStorage _skins;
        [SerializeField] private PlayerConfigStorage _config;
        [SerializeField] private LayoutStorage _layout;

        public override void InstallBindings()
        {


            Container.Bind<LayoutStorage>().FromInstance(_layout).AsCached();
            Container.Bind<AudioStorage>().FromInstance(_audio).AsSingle();
            Container.Bind<SkinStorage>().FromInstance(_skins).AsCached();
            Container.Bind<PlayerConfigStorage>().FromInstance(_config).AsCached();


            Container.Bind<IProgressSaver>().To<ProgressSaver>().FromInstance(new ProgressSaver(_layout)).AsCached();

            Container.BindMemoryPool<AudioPlayer, AudioPool>().WithInitialSize(2).FromComponentInNewPrefab(_audioPrefab).AsCached().NonLazy();

            Container.Bind<ISceneController>().To<SceneController>().FromInstance(_sceneController).AsCached().NonLazy();

            Container.Bind<ISettingsController>().To<SettingsController>().FromInstance(_settings).AsCached();
            Container.Bind<IAudioController>().To<AudioController>().FromInstance(_audioController).AsCached();

            Container.Bind<ISkinMenuController>().To<SkinMenuController>().FromInstance(_skinMenuController).AsCached();
            Container.Bind<ILevelMenuManager>().To<LevelMenuManager>().FromInstance(_levelMenuManager).AsCached();
            
            Container.Inject(_skinMenuController);
            Container.Inject(_levelMenuManager);
            Container.Inject(_settings);
            Container.Inject(_audioController);
        }
    }
}