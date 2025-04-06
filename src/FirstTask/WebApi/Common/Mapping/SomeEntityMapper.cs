using Domain;
using Serilog;
using WebApi.Common.Interfaces;
using WebApi.Dtos;

namespace WebApi.Common.Mapping
{
    public class SomeEntityMapper : IMapper<SomeEntityDto, SomeEntity>
    {
        public IEnumerable<SomeEntity> MapWith(IEnumerable<SomeEntityDto> request) =>
            request.Select(x =>
            {
                if(int.TryParse(x.Code, out int code))
                {
                    return new SomeEntity()
                    {
                        Code = code,
                        Value = x.Value
                    };
                }
                else
                {
                    Log.Warning("Argument code is not valid. Return default value");
                    return new SomeEntity() 
                    {
                        Code = 0,
                        Value = x.Value
                    };
                }
            }).
            ToArray();
    }
}
