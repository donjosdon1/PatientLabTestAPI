using System;
using System.Collections.Generic;
using System.Text;

namespace PatientLabTestAPI.Common
{
    public static class Constants
    {
        public const string RecordnotfoundErrorCode = "400";
        public const string RecordnotfoundErrorMessage = "Record not found";
        public const string GenericErrorcode = "500";
        public const string GenericErrorMessage = "Somethig went wrong. Please try again.";
        public const string RecordCreatedCode = "100";
        public const string RecordCreatedMessage = "Record has been created";
        public const string RecordUpdatedCode = "200";
        public const string RecordUpdatedMessage = "Record has been updated";
        public const string RecordDeletedCode = "300";
        public const string RecordDeletedMessage = "Record has been deleted";
        public const string RecordfoundCode = "600";
        public const string RecordfoundMessage = "Record has been found";
    }
}
