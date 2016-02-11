using System;

namespace AsyncTest.Communication.Server.Database.Mapper
{
    public interface IMapperDictionary
    {
        IMapper GetMapperForType(Type type);
    }
}