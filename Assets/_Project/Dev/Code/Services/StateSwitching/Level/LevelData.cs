using PizzaMaker.Code.Utils.Reactive.Deprecated;

namespace PizzaMaker.Code.Services.StateSwitching.Level
{
    public struct LevelData
    {
        public IReadOnlyReactiveVar<float> Progress;
        public int Level;

        public LevelData(IReadOnlyReactiveVar<float> progress, int level)
        {
            Progress = progress;
            Level = level;
        }
    }
}