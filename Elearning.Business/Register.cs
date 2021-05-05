using ElearningDatabase;
using ElearningDatabase.Models;
using System.Collections.Generic;
using System.Linq;

namespace Elearning.Business
{
    public class Register
    {
        public User ValidateRegister(string username, string password, string firstname, string lastname, string confirmPassword, bool checkInstructor, string email)
        {
            RoleEnum role = RoleEnum.Student;
            if(!CheckIfUserExists(username))
            {
                if (checkInstructor)
                {
                    role = RoleEnum.Trainer;
                }
                if (CheckPassword(password, confirmPassword) && IsValidEmail(email))
                {
                    using (ElearningContext elearningContext = new ElearningContext())
                    {
                        elearningContext.Users.Add(new User()
                        {
                            FirstName = firstname,
                            LastName = lastname,
                            Username = username,
                            Password = password,
                            Role = role,
                            Email = email
                        });
                        elearningContext.SaveChanges();
                        var newId = elearningContext.Users.Max(x => x.Id);
                        
                        var returnedUser = elearningContext.Users.Where(x => x.Id == newId).ToList().FirstOrDefault();
                        return returnedUser;
                    }

                }
                else
                {
                    throw new System.Exception("Invalid password or email!");
                }

            }
            else
            {
                throw new System.Exception("Username taken!");
            }
 
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckPassword(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                //min 6 chars, max 12 chars
                if (password.Length < 6 || password.Length > 12)
                    return false;

                //No white space
                if (password.Contains(" "))
                    return false;

                //At least 1 upper case letter
                if (!password.Any(char.IsUpper))
                    return false;

                //At least 1 lower case letter
                if (!password.Any(char.IsLower))
                    return false;

                //No two similar chars consecutively
                for (int i = 0; i < password.Length - 1; i++)
                {
                    if (password[i] == password[i + 1])
                        return false;
                }

                //At least 1 special char
                string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
                char[] specialCharactersArray = specialCharacters.ToCharArray();
                foreach (char c in specialCharactersArray)
                {
                    if (password.Contains(c))
                        return true;
                }
            }

            return false;
        }

        public bool CheckIfUserExists(string username)
        {
            using (ElearningContext elearningContext = new ElearningContext())
            {
                var users = elearningContext.Users.Where(x => x.Username == username).ToList();
                if(users.Count()>0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}