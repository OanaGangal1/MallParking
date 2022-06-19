using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace DataLayer.Sets
{
    public class Set<T> where T : BaseEntity, new()
    {
        private static readonly Dictionary<Guid, T> records = new();
        
        public static T New()
        {
            var newRecord = new T();
            records.Add(newRecord.Id, newRecord);
            return newRecord;
        }

        public static bool Delete(T record)
        {
            if (!records.ContainsKey(record.Id)) 
                return false;

            records.Remove(record.Id);
            return true;

        }

        public static bool Update(Guid id, T newValue)
        {
            if (!records.ContainsKey(id)) 
                return false;

            records[id] = newValue;
            return true;

        }

        public static IEnumerable<T> All() => records.Values;

        public static IEnumerable<T> Where(Func<T, bool> condition)
        {
            return records.Select(x => x.Value).Where(condition);
        }
                                                                    
    }
}
