using Pryanik.Audio;
using Pryanik.Layout;
using Pryanik.Res;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AudioController _audioController;
    [SerializeField] private GameObject _audioPrefab;

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


        Container.BindMemoryPool<AudioPlayer,AudioPool>().WithInitialSize(2).FromComponentInNewPrefab(_audioPrefab).AsSingle().NonLazy();

        Container.Bind<IAudioController>().To<AudioController>().FromInstance(_audioController).AsCached();
        Container.Inject(_audioController);
    }
}