using System;
using System.Collections.Generic;
using System.Text;

namespace AppDomain.Contexts
{
    public interface IContext
    {
        string ConnectionString { get; }
    }
}
