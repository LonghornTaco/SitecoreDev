using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Sitecore.Data.Validators;

namespace SitecoreDev.Web.Custom.Validators
{
   [Serializable]
   public class SsnValidator : StandardValidator
   {
      private string _ssnRegEx = @"^\d{3}-?\d{2}-?\d{4}$";

      public override string Name { get { return "SSN Validator"; } }

      public SsnValidator(SerializationInfo info, StreamingContext context) 
         : base(info, context)
      {
      }

      public SsnValidator()
      {
      }

      protected override ValidatorResult Evaluate()
      {
         string value = base.GetControlValidationValue();

         if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, _ssnRegEx))
            return ValidatorResult.Valid;

         base.Text = "SSN is not valid";

         return base.GetFailedResult(ValidatorResult.Error);
      }

      protected override ValidatorResult GetMaxValidatorResult()
      {
         return base.GetFailedResult(ValidatorResult.Error);
      }
   }
}