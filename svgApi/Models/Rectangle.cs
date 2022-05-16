using System;
using System.ComponentModel.DataAnnotations;

namespace svgApi.Models
{
	public class Rectangle
	{
		public Rectangle()
		{
		}

		[Key]
		public int RectId { get; set; }
		public int? Height { get; set; }
		public int? Width { get; set; }
		public int? XCoordinate { get; set; }
		public int? YCoordinate { get; set; }

		[Required]
		public int SvgId { get; set; }

	}
}

