using Application.Interfaces;
using Domain;
using Infastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories
{
    internal class SomeEntityRepository(SomeEntityDbContext context) : ISomeEntitiesRepository
    {
        private readonly SomeEntityDbContext _context = context;

        public async Task<IEnumerable<SomeEntity>> GetEntitiesByCode(int from, int to, CancellationToken ct)
        {
            var result = await _context.Entities.
                Where(e => e.Code >= from && e.Code <= to). //Получение сущностей по условию. Условие это диапазон, который мы задали
                ToArrayAsync(ct);

            return result;
        }

        public async Task SaveToDataBase(IEnumerable<SomeEntity> entities, CancellationToken ct)
        {
            var oldEntities = _context.Entities;
            _context.Entities.RemoveRange(oldEntities); //Удаление всех сущностей перед добавлением новых

            await _context.AddRangeAsync(entities, ct); //Добавление новых сущностей

            await _context.SaveChangesAsync(ct);
        }
    }
}
