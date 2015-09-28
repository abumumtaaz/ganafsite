using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GanafWeb.Domain;

namespace GanafWeb.Logic.Abstract
{
    public interface IBlogger
    {
        string AddPost(post post);
        string DeletePost(int id);
        string ModifyPost(post post);
        post GetPost(int id);
        IEnumerable<post> GetPosts();
        string Comment(string message, int postId);
        void Save();
    }
}
