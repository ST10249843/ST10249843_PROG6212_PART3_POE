﻿using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SecurityClaim = System.Security.Claims.Claim;

namespace CMCS.Controllers
{
    public class AccountController : Controller
    {
        /*
        Author: GeeksforGeeks
        Date: February 7, 2019
        Title: C# | List Class
        Type: Source Code
        Availability: GeeksforGeeks, https://www.geeksforgeeks.org/c-sharp-list-class/
        */

        public static List<User> Users = new List<User>();
        private static User CurrentUser = null;
        private static List<CMCS.Models.Claim> Claims = new List<CMCS.Models.Claim>();  


        /*
        Author: Microsoft
        Date: N/A
        Title: DbContext Class
        Type: Documentation
        Availability: Microsoft Learn, https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0
        */

        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult LoginRegister(string tab)
        {
            ViewBag.ActiveTab = tab ?? "login"; 
            return View();
        }
        

        /*
        Author: YogeshKumar Hadiya
        Date: March 2, 2016
        Title: Create Login (Sign In) and Registration (Sign Up) Form in C# Windows Form with Database
        Availability: C# Corner, https://www.c-sharpcorner.com/article/create-loginsign-in-and-registration-sign-up-form-in-c-sharp-windows-form-with-da/
        */

        // POST: LoginRegister
        [HttpPost]
        public async Task<IActionResult> LoginRegister(User user, string actionType)
        {
            ViewBag.ActiveTab = actionType == "Login" ? "login" : "register";

            if (actionType == "Register")
            {
                if (ModelState.IsValid)
                {
                    if (!Users.Any(u => u.Email == user.Email))
                    {
                        user.Id = Users.Count + 1;
                        Users.Add(user);

                        return RedirectToAction("LoginRegister", new { tab = "login" });
                    }

                    ModelState.AddModelError("", "Email is already registered.");
                }
                return View("LoginRegister", user);
            }
            else if (actionType == "Login")
            {
                var loggedInUser = Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                if (loggedInUser != null)
                {
                    var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, loggedInUser.Email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, loggedInUser.Role)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid email or password.");
                return View("LoginRegister", user);
            }

            return View("LoginRegister", user);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListLecturers()
        {
            // Check if the current user is not in the HR role
            if (!User.IsInRole("HR"))
            {
                // Set error message and redirect to Home if the user is not authorized
                TempData["ErrorMessage"] = "Access Denied! You do not have permission to access this resource.";
                return RedirectToAction("Index", "Home");
            }

            // Filter the users to include only those with the "Lecturer" role
            List<User> lecturers = Users.Where(u => u.Role == "Lecturer").ToList();

            // Pass the filtered list of lecturers to the view
            return View(lecturers);
        }

        [Authorize(Roles = "HR")] 
        public IActionResult EditLecturer(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            return View(user); // Pass the user to the edit view
        }

        [Authorize(Roles = "HR")]  
        [HttpPost]
        public IActionResult EditLecturer(User updatedUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }

            // Update user details
            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Email = updatedUser.Email;

            return RedirectToAction("ListLecturers"); // Redirect back to the list of lecturers
        }

        [Authorize(Roles = "HR")] 
        // GET: Delete a lecturer
        public IActionResult DeleteLecturer(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            return View(user); // Confirm deletion
        }

        [Authorize(Roles = "HR")]
        // POST: Confirm delete
        [HttpPost, ActionName("DeleteLecturer")]
        public IActionResult ConfirmDeleteLecturer(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }

            Users.Remove(user); // Remove the user from the list

            return RedirectToAction("ListLecturers"); // Redirect back to the list of lecturers
        }

        // POST: Logout
        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the current user
            CurrentUser = null;

            // Redirect to home page after logging out
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            TempData["ErrorMessage"] = "Access Denied! You do not have permission to access this resource.";
            return RedirectToAction("Index", "Home");
        } 
    }
}
