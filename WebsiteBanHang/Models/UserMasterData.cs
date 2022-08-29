using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class UserMasterData
    {
        public int Id { get; set; }
        
        [DisplayName("Họ")]
        public string FirstName { get; set; }
        [DisplayName("Tên")]
        public string LastName { get; set; }
        [DisplayName("Eamil")]
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
    }
}