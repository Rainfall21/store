﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class OrderPayment
    {
        public string UniqueCode { get; }
        public string Description { get; }
        public IReadOnlyDictionary<string, string> Parameteres { get; }
        public OrderPayment(string uniqueCode,
                             string description,
                             IReadOnlyDictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
                throw new ArgumentException(nameof(uniqueCode));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(nameof(description));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            UniqueCode = uniqueCode;
            Description = description;
            Parameteres = parameters;
        } 
    }
}