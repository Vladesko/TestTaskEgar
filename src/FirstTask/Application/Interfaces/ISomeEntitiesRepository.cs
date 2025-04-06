using Domain;

namespace Application.Interfaces
{
    /// <summary>
    /// Интерфейс, отвечающий за соединение между внешними источниками.
    /// </summary>
    public interface ISomeEntitiesRepository
    {
        /// <summary>
        /// Сохраняет сущности в Базу данных
        /// </summary>
        /// <param name="entities">Коллекция сущностей</param>
        /// <returns></returns>
        Task SaveToDataBase(IEnumerable<SomeEntity> entities, CancellationToken ct);
        /// <summary>
        /// Возвращает сущности из базы данных по коду
        /// </summary>
        /// <param name="from">Начало фильтра</param>
        /// <param name="to">Конец фильтра</param>
        /// <param name="ct"></param>
        /// <returns>Массив сущностей</returns>
        Task<IEnumerable<SomeEntity>> GetEntitiesByCode(int from, int to, CancellationToken ct);
    }
}
