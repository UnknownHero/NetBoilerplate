using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace Infrastructure.Utility
{
    public static class Mapper
    {
        public static ObjectMapperManager Instance
        {
            get { return ObjectMapperManager.DefaultInstance; }
        }

        public static List<TM> List<T, TM>(List<T> states)
        {
            var result = new List<TM>();

            result.AddRange(states.Select(state => Instance.GetMapper<T, TM>().Map(state)));

            return result;
        }

        public static TM Item<T, TM>(T item)
        {
            return Instance.GetMapper<T, TM>().Map(item);
        }

        /// <summary>
        ///     Map one object to dioferent object
        /// </summary>
        /// <typeparam name="T">From Type</typeparam>
        /// <typeparam name="TM">To Type</typeparam>
        /// <param name="fromObj">Source</param>
        /// <param name="toObj">Destanetion</param>
        /// <returns></returns>
        public static TM Item<T, TM>(T fromObj, TM toObj)
        {
            TM result = Instance.GetMapper<T, TM>().Map(fromObj, toObj);

            return toObj;
        }
    }
}
