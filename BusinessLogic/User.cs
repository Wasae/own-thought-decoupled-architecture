using System;
using InterfaceBL.Repository;

namespace BusinessLogic
{
    public class User : IUser
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string CreatedDate { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string ModifiedDate { get; set; }
        public int IsActive { get; set; }
    }
}