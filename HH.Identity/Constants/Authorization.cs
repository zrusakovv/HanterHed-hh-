namespace HH.Identity.Constants
{
    public class Authorization
    {
        //Роль по умолчанию 
        public enum Roles
        {
            Administrator,
            Company,
            Employee
        }
        
        public const string default_username = "user";
        
        public const string default_email = "user@secureapi.com";
        
        public const string default_password = "Pa$$w0rd.";
        
        public const Roles default_role = Roles.Employee;
    }
}