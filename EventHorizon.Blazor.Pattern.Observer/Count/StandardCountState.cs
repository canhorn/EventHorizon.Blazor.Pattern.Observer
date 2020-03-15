namespace EventHorizon.Blazor.Pattern.Observer.Count
{
    using EventHorizon.Blazor.Pattern.Observer.Count.Observers;
    using EventHorizon.Observer.State;
    using System.Threading.Tasks;

    public class StandardCountState : CountState, CountStateActions
    {
        private readonly ObserverState _state;

        public int Count { get; private set; } = 0;

        public StandardCountState(
            ObserverState state
        )
        {
            _state = state;
        }

        public async Task Increment()
        {
            Count++;
            await _state.Trigger<CountStateChangedObserver, CountStateChangedObserverArgs>(
                new CountStateChangedObserverArgs()
            );
        }
    }
}
