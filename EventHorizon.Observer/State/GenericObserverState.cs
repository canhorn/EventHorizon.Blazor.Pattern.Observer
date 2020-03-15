namespace EventHorizon.Observer.State
{
    using EventHorizon.Observer.Admin.Model;
    using EventHorizon.Observer.Admin.State;
    using EventHorizon.Observer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GenericObserverState : ObserverState, AdminObserverState
    {
        public IEnumerable<ObserverBase> List { get; private set; } = new List<ObserverBase>().AsReadOnly();

        private IEnumerable<AdminObserverBase> _adminList = new List<AdminObserverBase>().AsReadOnly();

        public void Register(
            ObserverBase observer
        )
        {
            if (observer == null || List.Contains(observer))
            {
                return;
            }

            List = List.AddItem(observer);
        }

        public void Remove(
            ObserverBase observer
        )
        {
            if (observer == null || !List.Contains(observer))
            {
                return;
            }
            List = List.RemoveItem(observer);
        }

        public async Task Trigger<TInstance, TArgs>(TArgs args) where TInstance : ArgumentBasedObserver<TArgs>
        {
            var typeOf = typeof(TInstance);
            var list = List.Where(a => typeOf.IsAssignableFrom(a.GetType())).ToList();
            var shouldRunOnce = ShouldRunOnce(typeOf);
            foreach (var observer in list)
            {
                try
                {
                    await ((TInstance)observer).Handle(
                        args
                    );
                    if (shouldRunOnce)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Exception thrown while triggering observer. {0}",
                        ex.Message
                    );
                }
            }
            foreach (var observer in _adminList)
            {
                try
                {
                    await observer.HandleAdminTrigger(
                        typeOf.Name,
                        args
                    );
                    if (shouldRunOnce)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Exception thrown while triggering observer. {0}",
                        ex.Message
                    );
                }
            }
        }

        private bool ShouldRunOnce(
            Type observerType
        )
        {
            return observerType
                .GetInterfaces()
                .Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(SingleArgumentBasedObserver<>)));
        }

        public void RegisterAdminObserver(
            AdminObserverBase observer
        )
        {
            if (observer == null || _adminList.Contains(observer))
            {
                return;
            }

            _adminList = _adminList.AddItem(observer);
        }

        public void RemoveAdminObserver(
            AdminObserverBase observer
        )
        {
            if (observer == null || !_adminList.Contains(observer))
            {
                return;
            }

            _adminList = _adminList.RemoveItem(observer);
        }
    }
}
