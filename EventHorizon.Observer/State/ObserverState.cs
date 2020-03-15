namespace EventHorizon.Observer.State
{
    using EventHorizon.Observer.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ObserverState
    {
        IEnumerable<ObserverBase> List { get; }
        void Register(
            ObserverBase observer
        );
        void Remove(
            ObserverBase observer
        );
        Task Trigger<TInstance, TArgs>(
            TArgs args
        ) where TInstance : ArgumentBasedObserver<TArgs>;
    }
}
