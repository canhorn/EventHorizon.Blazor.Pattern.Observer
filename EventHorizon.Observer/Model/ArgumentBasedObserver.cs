namespace EventHorizon.Observer.Model
{
    using System.Threading.Tasks;

    public interface ArgumentBasedObserver<TArgs> : ObserverBase
    {
        Task Handle(
            TArgs args
        );
    }
}
