namespace EventHorizon.Blazor.Pattern.Observer.Count
{
    using System.Threading.Tasks;

    public interface CountStateActions
    {
        Task Increment();
    }
}
