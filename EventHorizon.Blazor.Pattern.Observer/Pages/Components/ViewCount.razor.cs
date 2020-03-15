namespace EventHorizon.Blazor.Pattern.Observer.Pages.Components
{
    using EventHorizon.Blazor.Pattern.Observer.Count;
    using EventHorizon.Blazor.Pattern.Observer.Count.Observers;
    using EventHorizon.Observer.Register;
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Unregister;

    public class ViewCountModel : ComponentBase, IDisposable, CountStateChangedObserver
    {
        [Inject]
        public IMediator Mediator { get; set; }
        [Inject]
        public CountState CountState { get; set; }

        protected int Count { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Count = CountState.Count;
            await Mediator.Send(
                new RegisterObserverCommand(this)
            );
        }

        public async Task Handle(CountStateChangedObserverArgs args)
        {
            Count = CountState.Count;

            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Mediator.Send(
                new UnregisterObserverCommand(this)
            ).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
