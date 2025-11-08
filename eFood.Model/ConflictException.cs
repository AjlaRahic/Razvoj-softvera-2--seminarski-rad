using System;

namespace eFood.Model
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
