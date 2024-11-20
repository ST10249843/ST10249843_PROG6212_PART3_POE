using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CMCS.Controllers
{
    public class ClaimController : Controller
    {
        // Static list to simulate a data source
        private static readonly List<Claim> Claims = new List<Claim>();


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


        [Authorize(Roles = "Lecturer")]  // Both lecturers and admins can view claims
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

    }
}
