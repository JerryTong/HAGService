using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAG.Domain.Model.Enum
{
    public enum StatusCode
    {
        /// <summary>
        /// No error occurred.
        /// </summary>
        Success,

        /// <summary>
        /// Something fail.
        /// </summary>
        Failure = -1,

        /// <summary>
        /// An attempt was made to perform an illegal or unsupported operation with the device, or an invalid parameter value was used.
        /// </summary>
        Illegal = -2,
    }
}
