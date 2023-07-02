using AppTemplate.Shared.Interfaces;
using FluentValidation;

namespace AppTemplate.Shared.AbstractClasses
{
    public abstract class AbstractService
    {
        private readonly INotifier _notifier;
        protected AbstractService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string notification)
        {
            _notifier.Notify(notification);
        }

        protected async Task<bool> IsValid<V, E>(V validator, E entity) where V : IValidator<E>
                                                                 where E : IEntity
        {
            var validation = validator.Validate(entity);
            if (validation.IsValid)
            {
                return true;
            }
            foreach (var error in validation.Errors)
            {
                Notify(error.ErrorMessage);
            }
            return false;
        }
    }
}
