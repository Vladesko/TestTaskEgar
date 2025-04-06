using Application.Models;
using Domain;

namespace Application.Interfaces
{
    /// <summary>
    /// Интерфейс для взаимодействия уровня Application и другими уровнями. Взаимодействие с сущностями.
    /// </summary>
    public interface ISomeEntityService
    {
        /// <summary>
        /// Сохранение сущностей
        /// </summary>
        /// <param name="someEntities">Коллекция сущностей.</param>
        /// <returns></returns>
        Task SaveEntities(IEnumerable<SomeEntity> someEntities, CancellationToken ct);
        /// <summary>
        /// Возвращает коллекцию сущностей по фильтру
        /// </summary>
        /// <param name="from">С какого кода начать получение сущностей</param>
        /// <param name="to">До какого когда отбирать сущности</param>
        /// <param name="ct"></param>
        /// <returns>Список сущностей в виде коллекции</returns>
        Task<IEnumerable<SomeEntity>> GetEntities(CodeFilter filter, CancellationToken ct);
    }
}
