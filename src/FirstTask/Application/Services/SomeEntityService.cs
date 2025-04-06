using Application.Interfaces;
using Application.Models;
using Domain;

namespace Application.Services
{
    /// <summary>
    /// Класс сервиса для взаимодействия с сущностями
    /// </summary>
    internal class SomeEntityService(ISomeEntitiesRepository repository,
                                     IValidator<CodeFilter> validator) : ISomeEntityService
    {
        private readonly ISomeEntitiesRepository _repository = repository;
        private readonly IValidator<CodeFilter> _validator = validator;

        public async Task<IEnumerable<SomeEntity>> GetEntities(CodeFilter filter, CancellationToken ct)
        {
            if(_validator.Validate(filter) == false)
                return [];

            var entities = await _repository.GetEntitiesByCode(filter.From, filter.To, ct);
            return entities;
        }

        public async Task SaveEntities(IEnumerable<SomeEntity> someEntities, CancellationToken ct)
        {
            someEntities = someEntities.OrderBy(e => e.Code); //Сортирует сущности по коду
            await _repository.SaveToDataBase(someEntities, ct); //Сохранение сущностей в БД
        }
    }
}
