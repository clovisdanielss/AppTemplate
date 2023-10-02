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

        protected virtual void Notify(string notification)
        {
            _notifier.Notify(notification);
            //adicionar chamada para serviço de logger aqui. Garantindo limpeza e responsabilidade única.
        }

        protected virtual async Task<bool> IsValid<V, E>(V validator, E entity) where V : IValidator<E>
                                                                 where E : class
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
