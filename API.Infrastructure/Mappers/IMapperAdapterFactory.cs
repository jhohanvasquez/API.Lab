using System;
using System.Collections.Generic;
using System.Text;

namespace API.Infrastructure.Mappers
{
    public interface IMapperAdapterFactory
    {
        IMapperAdapter Create();
    }
}
