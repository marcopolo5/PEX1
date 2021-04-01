using ElearningDatabase;
using ElearningDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Elearning.Business
{
    public class Register
    {
        public bool ValidateRegister(string username, string password, string firstname, string lastname, string confirmPassword, bool checkInstructor, string email)
        {
            RoleEnum role = RoleEnum.Student;

            if (checkInstructor)
            {
                role = RoleEnum.Trainer;
            }
            if (CheckPassword(password, confirmPassword))
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
                        Email= email

                    });
                    elearningContext.SaveChanges();
                }

                return true;

            }
            return false;

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

    }
}
