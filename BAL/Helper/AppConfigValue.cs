using System;
using System.Configuration;

namespace BAL.Helper
{
    public static class AppConfigValue
    {
        public static string MyValue { get { return ConfigurationManager.AppSettings[""].ToString(); } }

        public static string EncryptionKey { get { return ConfigurationManager.AppSettings["EncryptionKey"].ToString(); } }
        public static string ServiceBaseUrl { get { return ConfigurationManager.AppSettings["ServiceBaseUrl"].ToString(); } }
        public static string ServiceBaseUrlVR { get { return ConfigurationManager.AppSettings["ServiceBaseUrlVR"].ToString(); } }
        public static string ServiceLoginAPI { get { return ConfigurationManager.AppSettings["ServiceLoginAPI"].ToString(); } }
        public static string ServiceGetHotelAvailabilityAPI { get { return ConfigurationManager.AppSettings["ServiceGetHotelAvailabilityAPI"].ToString(); } }
        public static string ServiceGetCondoAvailabilityAPI { get { return ConfigurationManager.AppSettings["ServiceGetCondoAvailabilityAPI"].ToString(); } }
        public static string ServiceGetHotelDestinationAPI { get { return ConfigurationManager.AppSettings["ServiceGetHotelDestinationAPI"].ToString(); } }
        public static string ServiceGetHotelsAPI { get { return ConfigurationManager.AppSettings["ServiceGetHotelsAPI"].ToString(); } }
        public static string ServiceGetCountriesAPI { get { return ConfigurationManager.AppSettings["ServiceGetCountriesAPI"].ToString(); } }
        public static string ServiceGetStatesAPI { get { return ConfigurationManager.AppSettings["ServiceGetStatesAPI"].ToString(); } }
        public static string ServiceGetCondoRegionsAPI { get { return ConfigurationManager.AppSettings["ServiceGetCondoRegionsAPI"].ToString(); } }
        public static string ServiceGetSelectedHotelDetails { get { return ConfigurationManager.AppSettings["ServiceGetSelectedHotelDetails"].ToString(); } }
        public static string ServiceAddHotelAPI { get { return ConfigurationManager.AppSettings["ServiceAddHotelAPI"].ToString(); } }
        public static string ServiceGetCondoRegionCityResortsAPI { get { return ConfigurationManager.AppSettings["ServiceGetListofRCIRegionCityResort"].ToString(); } }
        public static string ServiceGetCondoRegionCityResortsVRAPI { get { return ConfigurationManager.AppSettings["ServiceGetListofRCIRegionCityResortVR"].ToString(); } }
        public static int HotelGeneralRuleId { get { return Helper.ConvertToInt(ConfigurationManager.AppSettings["HotelGeneralRuleId"].ToString()); } }
        public static int CondoGeneralRuleId { get { return Helper.ConvertToInt(ConfigurationManager.AppSettings["CondoGeneralRuleId"].ToString()); } }
        public static string WebBaseUrl { get { return ConfigurationManager.AppSettings["WebBaseUrl"].ToString(); } }
        public static string ServiceConfirmHotelAPI { get { return ConfigurationManager.AppSettings["ServiceConfirmHotelAPI"].ToString(); } }
        public static string ServiceGetAvailableDatesVRAPI { get { return ConfigurationManager.AppSettings["ServiceGetListOfAvailableDatesVR"].ToString(); } }
        public static string ServiceConfirmCondoAvailabilityAPI { get { return ConfigurationManager.AppSettings["ServiceConfirmCondoAvailability"].ToString(); } }
        public static string ServiceConfirmCondoBookingAPI { get { return ConfigurationManager.AppSettings["ServiceConfirmCondoBooking"].ToString(); } }
        public static string ServiceHotelProfileVRAPI { get { return ConfigurationManager.AppSettings["ServiceHotelProfileVR"].ToString(); } }

        public static string ServiceGetOffersListAPI { get { return ConfigurationManager.AppSettings["ServiceGetOffersList"].ToString(); } }
        public static string ServiceGetOfferSummaryAPI { get { return ConfigurationManager.AppSettings["ServiceGetOfferSummary"].ToString(); } }

        public static string PPQApiKey { get { return ConfigurationManager.AppSettings["PPQApiKey"].ToString(); } }

        public static string SMPT_Email { get { return ConfigurationManager.AppSettings["SMPT_Email"].ToString(); } }
        public static string SMPT { get { return ConfigurationManager.AppSettings["SMPT"].ToString(); } }
        public static string SMPT_Password { get { return ConfigurationManager.AppSettings["SMPT_Password"].ToString(); } }
        public static string SMTP_Port { get { return ConfigurationManager.AppSettings["SMTP_PORT"].ToString(); } }
        public static string AdminEmail { get { return ConfigurationManager.AppSettings["AdminEmail"].ToString(); } }

        public static string ContactNumber { get { return ConfigurationManager.AppSettings["ContactNumber"].ToString(); } }
        public static string ContactCity { get { return ConfigurationManager.AppSettings["ContactCity"].ToString(); } }
        public static string ContactCountry { get { return ConfigurationManager.AppSettings["ContactCountry"].ToString(); } }
        public static string ContactEmail { get { return ConfigurationManager.AppSettings["ContactEmail"].ToString(); } }


        public static string Title { get { return ConfigurationManager.AppSettings["Title"].ToString(); } }
        public static string VENDOR { get { return ConfigurationManager.AppSettings["VendorID"].ToString(); } }

        public static bool AllRealRCIBooking { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["AllowRealRCIBooking"]); } }

        public static bool AuthenticateTravelCredit { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["AuthenticateTravelCredit"]); } }
        public static string TravelCreditUser { get { return ConfigurationManager.AppSettings["TravelCreditUser"]; } }
        public static string TravelCreditPassword { get { return ConfigurationManager.AppSettings["TravelCreditPassword"]; } }
    }
}
