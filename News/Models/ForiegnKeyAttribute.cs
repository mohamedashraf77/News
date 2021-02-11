using System;

namespace News.Models
{
    internal class ForiegnKeyAttribute : Attribute
    {
        private string _foriegnKeyName;

        public ForiegnKeyAttribute(string foriegnKeyName)
        {
            this._foriegnKeyName = foriegnKeyName;
        }
    }
}