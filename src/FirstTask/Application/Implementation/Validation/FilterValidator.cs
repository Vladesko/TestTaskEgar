using Application.Interfaces;
using Application.Models;
using Serilog;

namespace Application.Implementation.Validation
{
    internal class FilterValidator : IValidator<CodeFilter>
    {
        public bool Validate(CodeFilter model)
        {
            //Базовая валидация для фильтра
            if (model.From < 0)
            {
                Log.Warning("Not valid data");
                return false;
            }

            if (model.From > model.To)
            {
                Log.Warning("Not valid data");
                return false;
            }

            return true;
        }
    }
}
