using System;
using System.Runtime.Serialization;

namespace Iviz.MsgsGen.Dynamic
{
    [DataContract]
    public class Property
    {
        public Property(string name, IField value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        [DataMember] public string Name { get; }
        [DataMember] public IField Value { get; }

        public void Deconstruct(out string name, out IField value)
        {
            name = Name;
            value = Value;
        }
    }
}