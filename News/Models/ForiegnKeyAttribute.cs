using System;

namespace News.Models
{
    internal class ForiegnKeyAttribute : Attribute
    {
        private string v;

        public ForiegnKeyAttribute(string v)
        {
            this.v = v;
        }
    }
}