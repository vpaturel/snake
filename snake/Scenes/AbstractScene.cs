using SerenitySystem.Helpers;
using Raylib_cs;    
namespace SerenitySystem.Scenes
{
    public abstract class AbstractScene
    {
        private List<Helpers.Timer> timers = new List<Helpers.Timer>();

        public Helpers.Timer AddTimer(Action? callback, float duration, bool isLooping = true)
        {
            var timer = new Helpers.Timer(callback, duration, isLooping);
            timers.Add(timer);
            return timer;


        }

        public void RemoveTimer(Helpers.Timer timer)
        {
            timers.Remove(timer);
        }
        public void UpdateTimers()
        {
            foreach (var timer in timers)
            {
                timer.Update(Raylib.GetFrameTime());
            }

        }

        public virtual void Update()
        {
            UpdateTimers();
        }
        public abstract void Draw();
        public abstract void Load(object[]? args);
        public virtual void Unload()
        {
            timers = new List<Helpers.Timer>();

        }

    }
}
