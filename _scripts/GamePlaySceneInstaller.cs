using Pryanik.Checkpoint;
using UnityEngine;
using Zenject;


namespace Pryanik
{
    public class GamePlaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerCamera _camera;
        [SerializeField] private GamePlaySceneController _controller;
        [SerializeField] private CheckPointManager _checkpoint;

        [Header("Ui")]
        [SerializeField] private PlayerGravityUi _gravityUi;
        public override void InstallBindings()
        {

            Container.Bind<ICheckPointManager>().To<CheckPointManager>().FromInstance(_checkpoint);
            Container.Bind<IGamePlaySceneController>().To<GamePlaySceneController>().FromInstance(_controller);


            Container.Bind<IPlayer>().To<Player>().FromInstance(_player);
            Container.Bind<IPlayerCamera>().To<PlayerCamera>().FromInstance(_camera);

            Container.Bind<IPlayerGravityUi>().To<PlayerGravityUi>().FromInstance(_gravityUi);
        }
    }
}