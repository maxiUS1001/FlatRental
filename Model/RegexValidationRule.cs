using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FlatRental.Model
{
	public class EmptyStringValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string input = value.ToString();

			if (string.IsNullOrWhiteSpace(input))
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}

	public class PositiveIntValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string input = value.ToString();

			bool rt = Regex.IsMatch(input, @"^[1-9]\d*$");
			if (!rt)
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}	
	}

	public class FloatValueValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string input = value.ToString();

			bool rt = Regex.IsMatch(input, @"^\d+([.,]\d{1,2})?$");
			if (!rt)
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}

	public class EmailValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string input = value.ToString();

			bool rt = Regex.IsMatch(input, @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$");
			if (!rt)
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}

	public class NameValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string input = value.ToString();

			bool rt = Regex.IsMatch(input, @"^([a-zA-Zа-яА-ЯёЁ]{2,15})$");
			if (!rt)
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}

	public class TelephoneValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string input = value.ToString();

			bool rt = Regex.IsMatch(input, @"^\+375(29|33|44|25)[0-9]{7}$");
			if (!rt)
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}

	public class PasswordValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string input = value.ToString();

			bool rt = Regex.IsMatch(input, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$");
			if (!rt)
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}
}
