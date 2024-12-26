using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Utils.Reactive.Deprecated;

namespace PizzaMaker.Code.UI.Root.ProgressBar
{
    public class LevelProgressBarsPresenter : IDisposable
    {
        private readonly List<ProgressBar> _bars;
        private readonly IReadOnlyReactiveVar<float> _progress;

        public LevelProgressBarsPresenter(
            IEnumerable<ProgressBar> bars,
            int level,
            IReadOnlyReactiveVar<float> progress)
        {
            _progress = progress;
            _bars = bars.ToList();

            SetLevel(level);
            SetProgress(progress.Value);

            _progress.Changed += OnProgressChanged;
        }

        public void Dispose() => _progress.Changed -= OnProgressChanged;

        private void OnProgressChanged(float old, float current) =>
            SetProgress(current);

        private void SetLevel(int value) =>
            _bars.ForEach(bar => bar.SetLevel(value));

        private void SetProgress(float progressValue) =>
            _bars.ForEach(bar => bar.SetProgress(progressValue));
    }
}