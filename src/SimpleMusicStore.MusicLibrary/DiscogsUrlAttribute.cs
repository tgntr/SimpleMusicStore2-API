using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SimpleMusicStore.MusicLibrary
{
	public class DiscogsUrlAttribute : ValidationAttribute
	{
		private const string ValidDiscogsUrlPattern = @"https:\/\/www\.discogs\.com\/([^\/]+\/)?((release)|(master))\/[0-9]+([^\/]+)?";
		public DiscogsUrlAttribute()
		{
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var url = UrlToValidate(value);

			if (!IsValidDiscogsUrl(url))
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}

		private static bool IsValidDiscogsUrl(string url)
		{
			return Regex.IsMatch(url, ValidDiscogsUrlPattern);
		}

		private string UrlToValidate(object value)
		{
			return (string)value;
		}
	}
}