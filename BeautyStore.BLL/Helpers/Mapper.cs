using BeautyStore.Interfaces.Helpers;
using System;
using System.Linq;

namespace BeautyStore.BLL.Helpers
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            var destinationProps = typeof(TDestination).GetProperties();
            var result = Activator.CreateInstance<TDestination>();
            foreach (var prop in typeof(TSource).GetProperties())
            {
                var targetProp = destinationProps.FirstOrDefault(x => x.Name.IsEqual(prop.Name));
                if (targetProp == null) continue;
                if (!IsTypeEqual(targetProp.PropertyType, prop.PropertyType)) continue;
                targetProp.SetValue(result, prop.GetValue(source));
            }
            return result;
        }
        
        private bool IsTypeEqual(Type prop1, Type prop2)
        {
            if (prop1.IsEnum && prop2.IsEnum) return true;
            return prop1 == prop2;
        }
    }
}