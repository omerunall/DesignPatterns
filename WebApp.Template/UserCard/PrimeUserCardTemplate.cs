using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Template.UserCard
{
    public class PrimeUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            var sb = new StringBuilder();
            sb.Append("<a href='#' class='card-link'>Mesaj Gönder</a>");
            sb.Append("<a href='#' class='card-link'>Profil Detayı</a>");
            sb.Append("<a href='#' class='card-link'>Detay Açıklama</a>");
            return sb.ToString();
        }

        protected override string SetPicure()
        {
            return $"<img class='card-img-top' src='{AppUser.PictureUrl}' alt='Card image cap'>";
        }
    }
}
