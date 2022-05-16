using System;
using Microsoft.EntityFrameworkCore;
using svgApi.Models;

namespace svgApi.Controllers
{
	public class RectangleContext: DbContext
	{
		public RectangleContext(DbContextOptions<RectangleContext> options): base(options)
		{
		}

		public DbSet<Rectangle> Rectangles { get; set; } = default!;
	}
}

