using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCGP.COA.COMMON.Utilities
{
    public static class ObjectUtil
    {
        public static void CopyProperties<T, TU>(T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(q => q.CanRead).ToList();
            var destProps = typeof(TU).GetProperties().Where(q => q.CanWrite).ToList();

            foreach (var destProp in destProps)
            {
                var sourcePropsSameName = sourceProps.Where(q => q.Name == destProp.Name).ToList();

                if (sourcePropsSameName.Any())
                {
                    if (destProp.CanWrite)
                    {
                        var sourceProp = sourcePropsSameName.FirstOrDefault(q => q.PropertyType == destProp.PropertyType);
                        if (sourceProp == null) sourceProp = sourcePropsSameName.FirstOrDefault();

                        if (destProp.PropertyType == sourceProp.PropertyType)
                        {
                            destProp.SetValue(dest, sourceProp.GetValue(source, null), null);
                        }
                        else
                        {
                            try
                            {
                                destProp.SetValue(dest, sourceProp.GetValue(source, null), null);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
            }
        }

        public static TTarget Convert<TSource, TTarget>(TSource sourceItem)
        {
            if (null == sourceItem)
            {
                return default(TTarget);
            }

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace, NullValueHandling = NullValueHandling.Ignore };

            var serializedObject = JsonConvert.SerializeObject(sourceItem, deserializeSettings);

            return JsonConvert.DeserializeObject<TTarget>(serializedObject);
        }
    }
}
