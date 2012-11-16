using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Models.ViewModels;
using Raven.Client;

namespace WebAPI.Controllers
{
    public class PostController : BaseController
    {
        readonly Post[] _posts = new Post[]{
            new Post{Id=1,Title="This is Post1",PostedOn=DateTime.Now,Text="Lorem Ipsum is simply dummy text     of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."},
            new Post{Id=2,Title="This is Post2",PostedOn=DateTime.Now,Text="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."}
        };

        Comment[] _comments = new Comment[]{
            new Comment{Id=1,CommentText="This is comment1", PostId=1,PostedOn=DateTime.Now},
            new Comment{Id=2,CommentText="This is comment2", PostId=1,PostedOn=DateTime.Now},
            new Comment{Id=3,CommentText="This is comment1", PostId=2,PostedOn=DateTime.Now},
            new Comment{Id=4,CommentText="This is comment2", PostId=2,PostedOn=DateTime.Now}
        };

         
        //use this for creating sample posts/comments data
        public IEnumerable<Post> Get(int id)
        {
            RavenSession.Store(_posts[0]);
            //RavenSession.Store(comments);
            RavenSession.SaveChanges();
            return from p in _posts
                     where p.Id==id
                      select p;
        }

        //public IEnumerable<PostViewModel> Get()
        //{
        //    return (from p in posts
        //            select new PostViewModel
        //            {
        //                Text = p.Text,
        //                Title = p.Title,
        //                PostedOn = p.PostedOn,
        //                Comments = comments.Where(c => c.PostId == p.Id).ToArray()
        //            });
        //}

        public IEnumerable<PostViewModel> Get() 
        {
            return (from p in RavenSession.Query<Post>().ToList()
                    select new PostViewModel
                    {
                        Id=p.Id,
                        Text = p.Text,
                        Title = p.Title,
                        PostedOn = p.PostedOn,
                        Comments =  RavenSession.Query<Comment>().Where(c => c.PostId == p.Id).ToArray()
                    });
        }

        public Post PostBPost(Post p)
        {
            p.Id = RavenSession.Query<Post>().Count()+1;
            RavenSession.Store(p);
            RavenSession.SaveChanges();
            
            return p;
        }

        
    }
}
