namespace EventHorizon.Observer.Admin.State
{
    using EventHorizon.Observer.Admin.Model;

    public interface AdminObserverState
    {
        void RegisterAdminObserver(
            AdminObserverBase observer
        );
        void RemoveAdminObserver(
            AdminObserverBase observer
        );
    }
}
