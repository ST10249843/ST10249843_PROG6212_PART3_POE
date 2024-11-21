using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace CMCS.Controllers
{
    public class ClaimController : Controller
    {
        // Static list to simulate a data source
        public static readonly List<Claim> Claims = new List<Claim>();
      
            private readonly ApplicationDbContext _context;

            public ClaimController(ApplicationDbContext context)
            {
                _context = context;
            }

            /*
            Author: Microsoft
            Date: 07/14/2023
            Title: Role-based authorization in ASP.NET Core
            Code version: ASP.NET Core 8.0
            Type: Source Code
            Availability: Microsoft Learn, https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-8.0
            */

            [Authorize(Roles = "Lecturer")]  // Only lecturers can access this
        public IActionResult SubmitClaim()
        {
            ViewBag.IsSuccess = (bool?)null;
            return View();
        }

        /*
        Author: Microsoft
        Date: April 13, 2023
        Title: How to upload a file to a Web site by using Visual C# 
        Type: Source Code
        Availability: Microsoft Learn, https://learn.microsoft.com/en-us/troubleshoot/developer/webapps/aspnet/development/upload-file-to-web-site
        */

        [HttpPost]
        [Authorize(Roles = "Lecturer")]  // Only lecturers can submit claims
        public async Task<IActionResult> SubmitClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                // Calculate the total amount for the claim
                claim.CalculateTotalAmount();

                // Check if a supporting document is uploaded
                if (claim.SupportingDocument != null && claim.SupportingDocument.Length > 0)
                {
                    // Save the supporting document to the uploads folder
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    var filePath = Path.Combine(uploadsFolder, claim.SupportingDocument.FileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await claim.SupportingDocument.CopyToAsync(stream);
                    }
                }
                else
                {
                    ModelState.AddModelError("SupportingDocument", "You must upload a file.");
                    return View(claim);
                }

                // Determine the claim status based on HoursWorked
                if (claim.HoursWorked >= 15)
                {
                    // Mark claim as pending for admin review
                    claim.Status = "Pending";
                }
                else if (claim.HoursWorked > 0 && claim.HoursWorked < 15)
                {
                    // Automatically approve the claim
                    claim.Status = "Approved";
                }
                else if (claim.HoursWorked <= 0)
                {
                    // Automatically deny the claim
                    claim.Status = "Denied";
                }

                // Assign a unique ID to the claim
                claim.ID = Claims.Count > 0 ? Claims.Max(c => c.ID) + 1 : 1;

                // Add the claim to the in-memory collection
                Claims.Add(claim);

                // Indicate success in the view
                ViewBag.IsSuccess = true;

                return View(claim);
            }

            return View(claim);
        }


        [Authorize(Roles = "Lecturer,HR")]
        public IActionResult ViewClaims()
        {
            var claims = Claims.OrderBy(c => c.DateOfSubmission).ToList();
            return View(claims);
        }

        // GET: ApproveClaims
        [Authorize(Roles = "Admin")]  // Only admins can approve claims
        public IActionResult ApproveClaims()
        {
            var claims = Claims.Where(c => c.Status == "Pending")
                               .OrderBy(c => c.DateOfSubmission)
                               .ToList();
            return View(claims);
        }

        // POST: ApproveClaims/Approve
        [HttpPost]
        [Authorize(Roles = "Admin")]  // Only admins can approve claims
        public IActionResult Approve(int claimId)
        {
            var claim = Claims.FirstOrDefault(c => c.ID == claimId);
            if (claim != null)
            {
                claim.Status = "Approved";
            }

            return RedirectToAction("ApproveClaims");
        }

        // POST: ApproveClaims/Deny
        [HttpPost]
        [Authorize(Roles = "Admin")]  // Only admins can deny claims
        public IActionResult Deny(int claimId)
        {
            var claim = Claims.FirstOrDefault(c => c.ID == claimId);
            if (claim != null)
            {
                claim.Status = "Denied";
            }

            return RedirectToAction("ApproveClaims");
        }
        // POST: ApproveClaim
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can approve claims
        public IActionResult ApproveClaim(int id)
        {
            var claim = Claims.FirstOrDefault(c => c.ID == id);
            if (claim != null)
            {
                claim.Status = "Approved";
            }

            return RedirectToAction("ApproveClaims");
        }

        // POST: DenyClaim
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can deny claims
        public IActionResult DenyClaim(int id)
        {
            var claim = Claims.FirstOrDefault(c => c.ID == id);
            if (claim != null)
            {
                claim.Status = "Denied";
            }

            return RedirectToAction("ApproveClaims");
        }

        [HttpGet]
        [HttpPost]
        [Authorize(Roles = "HR")]
        public IActionResult GenerateReport(string lecturerFirstName)
        {
            if (string.IsNullOrEmpty(lecturerFirstName))
            {
                return Content("Please provide a lecturer's first name to filter by.");
            }

            var filteredClaims = Claims
                .Where(c => c.Name.Contains(lecturerFirstName, StringComparison.OrdinalIgnoreCase) && c.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filteredClaims.Count == 0)
            {
                return Content($"No approved claims found for lecturer: {lecturerFirstName}");
            }

            double totalHoursWorked = 0;
            decimal totalAmount = 0;

            // Generate the HTML content for the report with embedded CSS styles
            StringBuilder htmlContent = new StringBuilder();
            htmlContent.Append("<html><head>");
            htmlContent.Append("<style>");
            htmlContent.Append("body { font-family: Arial, sans-serif; background-color: #F4F1E1; color: #4E3629; margin: 20px; }");
            htmlContent.Append("h1 { color: #6F4F32; text-align: center; font-size: 24px; }");
            htmlContent.Append("table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            htmlContent.Append("th, td { padding: 8px 12px; border: 1px solid #D3B29F; text-align: left; }");
            htmlContent.Append("th { background-color: #6F4F32; color: white; font-size: 16px; }");
            htmlContent.Append("tr:nth-child(even) { background-color: #F9F0E1; }");
            htmlContent.Append("tr:hover { background-color: #E5D0B5; }");
            htmlContent.Append("h3 { color: #6F4F32; font-size: 18px; }");
            htmlContent.Append("p { font-size: 16px; color: #4E3629; }");
            htmlContent.Append("</style>");
            htmlContent.Append("</head><body>");
            htmlContent.Append("<h1>Invoice Report For: " + lecturerFirstName + "</h1>");
            htmlContent.Append("<table><thead><tr><th>Module</th><th>Hours Worked</th><th>Hourly Rate</th><th>Date</th><th>Total</th></tr></thead><tbody>");

            foreach (var claim in filteredClaims)
            {
                htmlContent.Append("<tr>");
                htmlContent.Append("<td>" + claim.Module + "</td>");
                htmlContent.Append("<td>" + claim.HoursWorked + "</td>");
                htmlContent.Append("<td>" + claim.HourlyRate.ToString("C") + "</td>");
                htmlContent.Append("<td>" + claim.DateOfSubmission.ToString("ddd, dd MMMM yyyy") + "</td>");
                htmlContent.Append("<td>" + claim.TotalAmount.ToString("C") + "</td>"); 
                htmlContent.Append("</tr>");

                totalHoursWorked += claim.HoursWorked;
                totalAmount += claim.TotalAmount;
            }

            htmlContent.Append("</tbody></table>");

            htmlContent.Append("<h3>Summary</h3>");
            htmlContent.Append("<p>Total Claims Made: " + filteredClaims.Count + "</p>");
            htmlContent.Append("<p>Subtotal Hours Worked: " + totalHoursWorked + " hours</p>");
            htmlContent.Append("<p>Subtotal Amount: " + totalAmount.ToString("C") + "</p>");

            htmlContent.Append("</body></html>");

            string fileName = "Invoice_Report_" + lecturerFirstName + ".html";

            // Convert the HTML content to a byte array
            byte[] fileContent = Encoding.UTF8.GetBytes(htmlContent.ToString());

            // Return the HTML content as a downloadable file
            return File(fileContent, "text/html", fileName);
        }



    }
}
