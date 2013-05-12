using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public interface ICrypt<T,TM>
    {
        T Encrypt(TM decrypt);
        TM Decrypt(T encrypt);
    }
}
