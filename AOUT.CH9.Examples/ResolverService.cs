using AOUT.CH9.Examples.Interfaces;
using StructureMap;
using StructureMap.Attributes;

namespace AOUT.CH9.Examples
{
    public class ResolverService
    {
        
        void Init()
        {
            StructureMapConfiguration
                .ForRequestedType<ILogger>()
                .TheDefaultIsConcreteType<RealLogger>();
                
        }
        public static T Resolve<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
