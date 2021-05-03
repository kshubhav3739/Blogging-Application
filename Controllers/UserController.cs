using Microsoft.AspNetCore.Mvc;
using BloggingApp.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace BloggingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IBlogRepository blogRepository;
        private readonly INotificationRepository notificationRepository;
        public UserController(IUserRepository _userRepository, IBlogRepository _blogRepository, INotificationRepository _notificationRepository)
        {
            userRepository = _userRepository;
            blogRepository = _blogRepository;
            notificationRepository = _notificationRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                int id = (int)HttpContext.Session.GetInt32("id");
                ViewBag.blogs = blogRepository.AllBlogs(id, 0);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public IActionResult AddBlog()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public IActionResult AddBlog(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                blog.userId = (int)HttpContext.Session.GetInt32("id");
                blog.dateTime = DateTime.Now;
                blogRepository.AddBlog(blog);
                TempData["Message"] = "New Blog Created successfully.";
                TempData["Class"] = "alert-success";
                return RedirectToAction("AddBlog");
            }
        }

        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                int id = (int)HttpContext.Session.GetInt32("id");
                User user = userRepository.GetUser(id);
                ViewBag.user = user;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public IActionResult UpdateProfile()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                int id = (int)HttpContext.Session.GetInt32("id");
                User user = userRepository.GetUser(id);
                ViewBag.user = user;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public IActionResult UpdateProfile(User user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                ViewBag.user = userRepository.GetUser((int)HttpContext.Session.GetInt32("id"));
                return View();
            }
            else
            {

                userRepository.UpdateUser(user);
                TempData["Message"] = "Your profile updated successfully.";
                TempData["Class"] = "alert-success";
                return RedirectToAction("Profile");
            }
        }
        [HttpGet]
        public IActionResult UpdateBlog(int id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                Blog blog = blogRepository.GetBlog(id);
                ViewBag.blog = blog;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public IActionResult UpdateBlog(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                ViewBag.blog = blogRepository.GetBlog(blog.id);
                return View();
            }
            else
            {
                blogRepository.UpdateBlog(blog);
                TempData["Message"] = "Your blog updated successfully.";
                TempData["Class"] = "alert-success";
                return RedirectToAction("Index");
            }
        }

        public IActionResult DeleteBlog(int id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                blogRepository.DeleteBlog(id);
                TempData["Message"] = "Blog Deleted successfully.";
                TempData["Class"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Notifications()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
                int id = (int)HttpContext.Session.GetInt32("id");
                ViewBag.notifications = notificationRepository.AllNotifications(id);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
