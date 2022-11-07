
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EFBlogPosts.Models;

namespace EFBlogPosts
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
            
        }
        public static void Menu()
        {
            Console.WriteLine("\nSelect Option:\n1. Display Blogs\n2. Add Blog\n3. Display Posts\n4. Add Post\nX. Exit\n");
            var input = Console.ReadLine();
            Switch(input);
        }
        public static void Switch(string input)
        {
             
            while(input != "X")
            {
                switch (input)
                {
                    case "1":
                        ReadBlogs();
                        Menu();
                        break;
                    case "2":
                        AddBlog();                     
                        Menu();
                        break;
                    case "3":
                        ReadBlogs();
                        Console.WriteLine("Select Blog");
                        try {
                            var selection = Convert.ToInt32(Console.ReadLine());
                            ListPosts(selection);
                        }
                        catch (FormatException){
                            Console.WriteLine("Invalid Input");
                        }
                        Menu(); 
                        break;
                    case "4":
                        ReadBlogs();
                        Console.WriteLine("Select Blog");
                        try
                        {
                            var selection = Convert.ToInt32(Console.ReadLine());
                            AddPost(selection);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        Menu();
                        break;
                    case "X":
                        break;
                        
                }
            }

        }
        public static void ReadBlogs()
        {
            using (var db = new BlogContext())
            {
                System.Console.WriteLine("Here is the list of blogs");
                foreach (var b in db.Blogs)
                {
                    System.Console.WriteLine($"Blog: {b.BlogId}: {b.Name}");
                }
            }
        }
        public static void AddBlog()
        {
            System.Console.WriteLine("Enter your Blog name");
            var blogname = Console.ReadLine();

            // // create new blog
            var blog = new Blog();
            blog.Name = blogname;

            // // save blog object to database
            using (var db = new BlogContext())
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
            }
        }
        public static void ListPosts(int selection)
        {
            using (var db = new BlogContext())
            {
                var blog = db.Blogs.Where(x => x.BlogId == selection).FirstOrDefault();
                // var blogsList = blog.ToList(); // convert to List from IQueryable

                System.Console.WriteLine($"Posts for Blog {blog.Name}");

                foreach (var post in blog.Posts)
                {
                    System.Console.WriteLine($"\tPost {post.PostId}\n{post.Title}\n{post.Content}");
                }
            }
        }
        public static void AddPost(int selection)
        {
            using (var db = new BlogContext())
            {   var blog = db.Blogs.Where(x => x.BlogId == selection).FirstOrDefault();

                System.Console.WriteLine("Enter your post title");
                var postTitle = Console.ReadLine();
                System.Console.WriteLine("Enter your post content");
                var postContent = Console.ReadLine();

                var post = new Post();
                post.Title = postTitle;
                post.Content = postContent;
                post.BlogId = blog.BlogId;

            
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }
    }
}
