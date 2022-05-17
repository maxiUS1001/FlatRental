﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model
{
    public class Validation
    {
        public static bool CheckValid(object o)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(o);

            if (!Validator.TryValidateObject(o, context, results, true))
            {
                foreach (var error in results)
                {
                    throw new InvalidConstraintException(error.ErrorMessage);
                }
                return false;
            }
            else
                return true;
        }
    }
}
