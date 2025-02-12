using SerenitySystem.services;

namespace SerenitySystem.Scenes
{


    public interface IScenesManager
    {
        void Load<T>(object[] args) where T : AbstractScene, new();
        void Unload();

    }
    public class ScenesManager : IScenesManager
    {
        private AbstractScene? _curentScene;
        public ScenesManager()
        {
            Services.AddService<IScenesManager>(this);
        }

        public void Load<T>(object[]? args) where T : AbstractScene, new()
        {
            _curentScene?.Unload();
            _curentScene = new T();
            _curentScene.Load(args);
        }
        public void Update()
        {
            if (_curentScene != null) _curentScene.Update();
        }
        public void Draw()
        {
            if (_curentScene != null) _curentScene.Draw();
        }
        public void Unload()
        {
            if (_curentScene != null) _curentScene.Unload();
        }
    }
}
