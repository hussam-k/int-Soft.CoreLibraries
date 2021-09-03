using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using intSoft.MVC.Core.Identity;
using Microsoft.AspNet.Identity;

namespace intSoft.MVC.Core.CustomValueProvider
{
    public class CreationDateUserIdValueProvider : IValueProvider
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public CreationDateUserIdValueProvider(ICurrentUser<IUser<Guid>> currentUser)
        {
            if (currentUser != null && currentUser.User != null)
            {
                _values.Add("CreatedBy", currentUser.User.Id);
                _values.Add("CreatedById", currentUser.User.Id);
            }
            _values.Add("CreationDate", DateTime.Now);
            _values.Add("CreatedDate", DateTime.Now);
        }

        public bool ContainsPrefix(string prefix)
        {
            return _values.ContainsKey(prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            return _values.ContainsKey(key)
                ? new ValueProviderResult(_values[key], _values[key].ToString(), CultureInfo.CurrentCulture)
                : null;
        }
    }
}