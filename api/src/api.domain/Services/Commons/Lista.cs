using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.Commons
{
    public class Lista
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public Lista(string key, string value)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
        }

    }
}
