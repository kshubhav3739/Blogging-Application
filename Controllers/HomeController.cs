using Microsoft.AspNetCore.Mvc;
using BloggingApp.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace BloggingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IBlogRepository blogRepository;
        private readonly ICommentRepository commentRepository;

        private readonly INotificationRepository notificationRepository;

        public HomeController(IUserRepository _userRepository, IBlogRepository _blogRepository, ICommentRepository _commentRepository, INotificationRepository _notificationRepository)
        {
            userRepository = _userRepository;
            blogRepository = _blogRepository;
            commentRepository = _commentRepository;
            notificationRepository = _notificationRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.blogs = blogRepository.AllBlogs();
                return View();
            }
        }

        public IActionResult Blogs()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.blogs = blogRepository.AllBlogs();
                return View();
            }
        }

        [HttpGet]
        public IActionResult Blog(int id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                Blog blog = blogRepository.GetBlog(id);
                ViewBag.blog = blog;
                User user = userRepository.GetUser(blog.userId);
                ViewBag.name = user.name;
                ViewBag.blogs = blogRepository.AllBlogs(user.id, id);
                ViewBag.comments = commentRepository.AllComments(id);
                return View();
            }
        }

        [HttpPost]
        public IActionResult Blog(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                Blog blog = blogRepository.GetBlog(comment.blogId);
                ViewBag.blog = blog;
                User user = userRepository.GetUser(blog.userId);
                ViewBag.name = user.name;
                ViewBag.blogs = blogRepository.AllBlogs(user.id, comment.blogId);
                ViewBag.comments = commentRepository.AllComments(comment.blogId);
                return View();
            }
            else
            {
                Blog blog = blogRepository.GetBlog(comment.blogId);
                ViewBag.blog = blog;
                User user = userRepository.GetUser(blog.userId);
                ViewBag.name = user.name;
                ViewBag.blogs = blogRepository.AllBlogs(user.id, comment.blogId);
                comment.dateTime = DateTime.Now;
                comment.id = 0;
                commentRepository.AddComment(comment);
                Notification notification = new Notification();
                notification.blogTitle = blog.title;
                notification.dateTime = DateTime.Now;
                notification.comment = comment.commentDes;
                notification.name = comment.name;
                notification.userId = blog.userId;
                notificationRepository.AddNotification(notification);
                ViewBag.comments = commentRepository.AllComments(comment.blogId);
                return View();
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Login(Login user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var validate = userRepository.Login(user.email, user.password);
                if (validate.Equals(true))
                {
                    User _user = userRepository.GetUser(user.email);
                    HttpContext.Session.SetString("name", _user.name);
                    HttpContext.Session.SetInt32("id", _user.id);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["Message"] = "Email-Id or Password is incorrect.";
                    TempData["Class"] = "alert-danger";
                    return RedirectToAction("Login");
                }
            }
        }
      
        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                user.dateTime = DateTime.Now;
                userRepository.AddUser(user);
                TempData["Message"] = "You are registered successfully.";
                TempData["Class"] = "alert-success";
                return RedirectToAction("Register");
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var validate = userRepository.Validate(user.email, user.mobile);

                if (validate.Equals(true))
                {
                    string password = userRepository.ForgotPassword(user.email, user.mobile);
                    TempData["Message"] = "You Password is :- " + password + "";
                    TempData["Class"] = "alert-success";
                    return RedirectToAction("ForgotPassword");
                }
                else
                {
                    TempData["Message"] = "You entered incorrect email or mobile.";
                    TempData["Class"] = "alert-danger";
                    return RedirectToAction("ForgotPassword");
                }
            }
        }


    }
}
