using System;

namespace CT.DistanceAPI.Model
{
    public class IATACode
    {
        private IATACode() { }

        public string Code { get; private set; }

        public bool IsValid { get; internal set; } = true;

        public override string ToString()
            => !IsValid ? "<Invalid>" : Code;

        public static IATACode FromString(string code)
        {
            if (String.IsNullOrWhiteSpace(code))
                return new IATACode() { IsValid = false };
            return new IATACode() { Code = code.ToUpper() };
        }
    }
}
