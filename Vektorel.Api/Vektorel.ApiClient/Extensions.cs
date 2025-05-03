using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vektorel.ApiClient;

internal static class Extensions
{
    public static bool HasFailed(this HttpResponseMessage response)
    {
        return !response.IsSuccessStatusCode;
    }
}
