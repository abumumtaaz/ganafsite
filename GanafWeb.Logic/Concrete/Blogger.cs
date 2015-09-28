using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GanafWeb.Domain;
using GanafWeb.Logic.Abstract;
using GanafWeb.Logic.Constants;

namespace GanafWeb.Logic.Concrete
{
    public class Blogger : IBlogger
    {
        readonly GanafDBEntities _entities = new GanafDBEntities();

        public string AddPost(Domain.post post)
        {
            try
            {
                _entities.posts.Add(post);
                Save();
                return "Post added successfully";
            }
            catch (Exception)
            {
                return "An error occured. Please retry?ERROR";
            }
        }

        public string DeletePost(int id)
        {
            try
            {
                var result = _entities.posts.Find(id);
                _entities.posts.Remove(result);
                Save();
                return "Post deleted successfully";
            }
            catch (Exception)
            {
                return "An error occured. Please retry?ERROR";
            }
        }

        public string ModifyPost(post post)
        {
            try
            {
                var result = _entities.posts.Find(post.id);
                result.topic = post.topic;
                result.intro = post.intro;
                result.body = post.body;
                Save();
                return "Post Modified successfully";
            }
            catch (Exception)
            {
                return "An error occured. Please retry?ERROR";
            }
        }

        public IEnumerable<post> GetPosts()
        {
            try
            {
                var result = (from x in _entities.posts orderby x.postDate select x).AsEnumerable();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Comment(string message, int postId)
        {
            try
            {
                IRequest request = new Request();
                var com = new Comment
                {
                    postId = postId,
                    userId = request.GetUser(SessionParameters.GetCurrentUser()).userId,
                    comment1 = message,
                    commentDate = DateTime.Now
                };
                _entities.Comments.Add(com);
                Save();
                return "Comment Added Successfully";
            }
            catch (Exception)
            {
                return "An error occured. Please retry?ERROR";
            }
        }

        public void Save()
        {
            _entities.SaveChanges();
        }


        public post GetPost(int id)
        {
            try
            {
                var result = _entities.posts.Find(id);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
