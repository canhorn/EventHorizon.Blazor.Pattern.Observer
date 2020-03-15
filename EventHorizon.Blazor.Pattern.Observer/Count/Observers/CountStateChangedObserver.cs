namespace EventHorizon.Blazor.Pattern.Observer.Count.Observers
{
    using EventHorizon.Observer.Model;

    public interface CountStateChangedObserver : ArgumentBasedObserver<CountStateChangedObserverArgs>
    {
    }
}
