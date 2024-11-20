using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    // The Claim class represents a lecturer's claim submission
    public class Claim
    {
        // Unique identifier for the claim
        public int ID { get; set; }

        // Lecturer's first name, required field
        [Required]
        public string Name { get; set; }

        // Lecturer's last name, required field
        [Required]
        public string Surname { get; set; }

        // Module related to the claim, required field
        [Required]
        public string Module { get; set; }

        // Status of the claim, defaults to "Pending"
        public string Status { get; set; } = "Pending";

        // Number of hours worked, required and must be a positive number
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Hours worked must be a positive number.")]
        public double HoursWorked { get; set; }

        // Hourly rate for the lecturer, required and must be a positive number
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Hourly rate must be a positive number.")]
        public int HourlyRate { get; set; }

        // Calculated total amount (hours worked * hourly rate), displayed as 'Total Amount (R)'
        [Display(Name = "Total Amount (R)")]
        public double TotalAmount { get; private set; }

        // Supporting document uploaded by the lecturer, if any
        public IFormFile SupportingDocument { get; set; }

        // Date when the claim was submitted, defaults to the current date and time
        public DateTime DateOfSubmission { get; set; } = DateTime.Now;

        // Method to calculate the total amount of the claim based on hours worked and hourly rate
        public void CalculateTotalAmount()
        {
            TotalAmount = HoursWorked * HourlyRate;
        }
    }
}
