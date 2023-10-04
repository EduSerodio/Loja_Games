using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lojaGames.Security
{
    public class Settings
    {
        private static string secret = "00313095a4ad6e505cdddbafbeb1cc01eaed40a4daafaaba0156b08d27dec404";

        public static string Secret { get => secret; set => secret = value; }
    }
}