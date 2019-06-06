using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleMusicStore.Models.Entities
{
	//todo chec if data entities can inherit from abstract classes
	public abstract class Entity<T>
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public T Id { get; set; }
	}
}
