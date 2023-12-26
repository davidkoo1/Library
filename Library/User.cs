using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserAdres { get; set; }
        public DateTime UserBirthDay { get; set; }
        //может быть нужен еще статус?(типо чтоб не абы кто, абы что добавлял) 

    }
}
