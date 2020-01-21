using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapcompare.Interfaces
{
    interface IHash
    {
        BitArray GetHash();
        bool IsSimilar();
    }
}
