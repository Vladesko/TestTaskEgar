namespace Application.Interfaces
{
    internal interface IValidator<T>
    {
        /// <summary>
        /// Базовая валидация для различных сущностей
        /// </summary>
        /// <param name="model">Модель, коорую необходимо валидировать</param>
        /// <returns>Если фильтр невалидный, возвращает false, иначе будет true</returns>
        bool Validate(T model);
    }
}
