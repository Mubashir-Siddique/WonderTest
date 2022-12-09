using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAnalytics.Core
{
    public class APIResponse
    {
        public int StatusCode { get; set; } = APIResponseCodes.STATUSCODEOK;
        public string StatusMessage { get; set; } = GameAnalytics.Constant.Constant.SuccessResponse;

        public object Response { get; set; }
        public APIResponse()
        {

        }
    }

    public class APIResponseCodes
    {

        public static int STATUSCODEOK { get { return 0; } }
        public static int STATUSCODEERROR { get { return 1; } }
    }
}
