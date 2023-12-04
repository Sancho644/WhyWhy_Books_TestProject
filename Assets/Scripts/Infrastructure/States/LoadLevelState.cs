using Infrastructure.Factory;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string MainScene = "Main";
        
        private readonly SceneLoader _sceneLoader;
        
        private IGameFactory _gameFactory;

        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public LoadLevelState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(MainScene, LoadLevelResources);
        }

        public void Exit()
        {
            
        }

        private void LoadLevelResources()
        {
            
        }
    }
}