using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FakeN.Web
{
    public static class Helper
    {
        public static Dictionary<string, object> ToDictionary(object anonymous)
        {
            var items = new Dictionary<string, object>();
            if (anonymous == null) 
                return items;

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(anonymous))
            {
                var obj2 = descriptor.GetValue(anonymous);
                items.Add(descriptor.Name, obj2);
            }
            return items;
        }

        public static NameValueCollection ToNameValue(object anonymous)
        {
            var items = new NameValueCollection();
            if (anonymous == null)
                return items;

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(anonymous))
            {
                var obj2 = descriptor.GetValue(anonymous);
                items.Add(descriptor.Name, obj2 == null ? "" : obj2.ToString());
            }
            return items;
        }
    }
}