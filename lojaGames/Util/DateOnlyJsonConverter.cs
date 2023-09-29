using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lojaGames.Util
{
    public class DateOnlyJsonConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    {
         public DateOnlyJsonConverter() 
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}