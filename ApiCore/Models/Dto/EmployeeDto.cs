﻿using System.ComponentModel.DataAnnotations;

namespace ApiCore.Models.Dto
{
	public class EmployeeDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Salary { get; set; }
		public string UAN { get; set; }
	}
}
