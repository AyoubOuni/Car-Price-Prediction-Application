using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet.Data;
using Projet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Projet.Controllers
{
    public class Resetpassword : Controller
    {
        // GET: Resetpassword
        public IActionResult Checkemail()
        {
            return View();
        }
        private readonly AppDB _context; // Declaring the DbContext

        public Resetpassword(AppDB context) // Injecting the DbContext in the constructor
        {
            _context = context;
        }
        private string GenerateRandomString(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(chars);
        }

        [HttpPost]
        public IActionResult sendemail()
        {
            string email = Request.Form["Email"];

            if (!string.IsNullOrEmpty(email))
            {
                var existingUser = _context.users.FirstOrDefault(u => u.Email == email);

                if (existingUser != null)
                {
                    string resetUrl = "https://localhost:44375/NewPassword/" + GenerateRandomString(10);


                    DateTime requestedAt = DateTime.Now;
                    DateTime expiry = requestedAt.AddMinutes(2); // Set expiry time (adjust as needed)

                    // Add the reset request to the database
                    ResetPassword resetPassword = new ResetPassword
                    {
                        UserId = existingUser.Id,
                        Url = resetUrl,
                        RequestedAt = requestedAt,
                        Expiry = expiry // Set expiration time
                    };

                    _context.resetpasswords.Add(resetPassword);
                    _context.SaveChanges();



                    string userName = existingUser.Firstname; // Assuming the user's first name is stored in FirstName field

                    string fromMail = "ayoubelouni6@gmail.com";
                    string fromPassword = "kubh rxqj gcgo vjqc";

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromMail);
                    message.Subject = "Reset password";
                    message.To.Add(new MailAddress(email));

                    // Replace {{$user}} with the retrieved user's name
                    string emailBody = "<html><body><h1>Forgot Password</h1><p>Hello " + userName + ",</p><p>We received a request to reset your password. If you did not make this request, please ignore this email.</p><p>To reset your password, Follow the link below:</p><div style=\"font-weight:bold\">" + resetUrl + "</div><p>This password reset link will expire in 2 minutes.</p><p>If you have any questions, feel free to contact us.</p></body></html>";

                    message.Body = emailBody;
                    message.IsBodyHtml = true;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };

                    smtpClient.Send(message);

                    return RedirectToAction("Auth", "Login");
                }
                else
                {
                    // User not found in the database, add model error for invalid credentials
                    ModelState.AddModelError(string.Empty, "Invalid Email");
                    return View("Checkemail");
                }
            }

            // If email or password is empty, add model error and return to the login view
            ModelState.AddModelError(string.Empty, "Email is required");
            return View("Checkemail");
        }



        public IActionResult NewPassword(string key)
        {
            // Get the current URL
            var currentUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

            // Query the database to check if the URL exists and is not expired
            var resetEntry = _context.resetpasswords.FirstOrDefault(r => r.Url == currentUrl && r.Expiry > DateTime.UtcNow);

            if (resetEntry != null)
            {
                // URL exists and is not expired
                // Proceed with resetting the password or rendering the respective view
                return View();
            }
            else
            {
                // URL doesn't exist or is expired
                // Redirect to a 404 page or handle it accordingly
                return NotFound();
            }


        }



        [HttpPost]
        public IActionResult setNewPassword(string Password, string ConfirmPassword, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var resetEntry = _context.resetpasswords.FirstOrDefault(r => r.Url == key);

                if (resetEntry != null)
                {
                    if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
                    {
                       
                        var id = resetEntry.UserId;
                        var existingUser = _context.users.FirstOrDefault(u => u.Id == id);

                        existingUser.Password = Password; // Update the user's password with the new value
                        _context.Update(existingUser);
                        _context.SaveChanges();
                        
                            TempData["passwordMessage"] = "Password changed successfully.";

                        return RedirectToAction("Auth", "Login"); // Redirect to login page after updating the password
                    }
                    else
                    {
                        // Handle invalid password or other validation issues
                        ModelState.AddModelError(string.Empty, "Passwords cannot be empty");
                        return View(); // Return the view with validation errors
                    }
                }
                else
                {
                    return NotFound(); // Key not found in database
                }
            }
            else
            {
                return BadRequest(); // Key is null or not received
            }
        }



    }
}
