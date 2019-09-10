using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleMusicStore.Entities.Common
{
	public abstract class UserActivity
	{
		public UserActivity()
		{
            //TODO: See the references and what can you do about it.
			//Date = DateTime.UtcNow;
		}

		[Required]
		public string UserId { get; set; }
		public virtual User User { get; set; }

		public DateTime Date { get; set; }
	}
}
