using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    // Represents a view model for claim submission
    public class ViewClaim
    {
        // Unique identifier for the claim
        public int Id { get; set; }

        // Date when the claim was submitted, required field
        [Required]
        public DateTime DateOfSubmission { get; set; }

        // Number of hours worked, must be a positive integer
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Hours Worked must be a positive number.")]
        public int HoursWorked { get; set; }

        // Hourly rate for the work, must be a positive number
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Hourly Rate must be a positive number.")]
        public double HourlyRate { get; set; }

        // Total amount calculated (optional field that could be computed)
        public double TotalAmount { get; set; }

        // Supporting document required for the claim, uploaded as a file
        [Required]
        public IFormFile SupportingDocument { get; set; }

        // Status of the claim (e.g., Pending, Approved, Rejected)
        public string Status { get; set; }
    }
}
