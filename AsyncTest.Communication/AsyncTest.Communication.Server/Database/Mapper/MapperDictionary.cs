using System;
using System.Collections.Generic;
using System.Linq;

namespace AsyncTest.Communication.Server.Database.Mapper
{
    public class MapperDictionary : IMapperDictionary
    {
        private readonly IDictionary<Type, IMapper> _mapperDictionary = new Dictionary<Type, IMapper>();

        public MapperDictionary(IEnumerable<IMapper> mappers)
        {
            foreach (IMapper mapper in mappers)
            {
                Type mapperType = mapper.GetType();
                Type[] interfaces = mapperType.GetInterfaces();
                Type mapperInterface = interfaces.Last(x => x.IsGenericType);
                foreach (Type type in mapperInterface.GetGenericArguments())
                {
                    _mapperDictionary.Add(type, mapper);
                }
            }
        }

        public IMapper GetMapperForType(Type type)
        {
            return _mapperDictionary[type];
        }
    }
}