using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Model.Enum
{
   
    public class CallType
    {
        public static readonly CallType UK = new("UK", "+44");
        public static readonly CallType USA = new("USA", "+1");
        public static readonly CallType UKMobile = new("UKMobile", "+447");
        public static readonly CallType Ireland = new("Ireland", "+353");
        public static readonly CallType International = new("International", string.Empty);

        private CallType(string type, string prefix)
        {
            Type = type;
            Prefix = prefix;
        }

        public string Type { get; set; }

        public string Prefix { get; set; }

        public static List<CallType> List()
        {
            return new List<CallType>(){
                UK, USA, UKMobile, Ireland, International
            };
        } 

        public static CallType GetCallTypeByNumber(string number)
        {
            return List()
                .OrderByDescending(x => x.Prefix)
                .FirstOrDefault(x => number.StartsWith(x.Prefix)) ?? International;
        } 
    }
}
