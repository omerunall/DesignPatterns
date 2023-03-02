using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Template.UserCard
{
    public class DefaultUserCardTemplate : UserCardTemplate
    {



        protected override string SetFooter()
        {
            return string.Empty;
        }

        protected override string SetPicure()
        {
            return $"<img class='card-img-top' src='/pictures/profile-user.png' alt='Card image cap'>";
        }
    }
}
