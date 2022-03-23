using System;

namespace LegacyApp
{
    public class UserService
    {
        ClientRepository clientRepository = new ClientRepository();

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || IsNotEmailValid(email)|| IsNotAdult(dateOfBirth))
            {
                return false;
            }

            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            SetCreditLimit(client.Name, user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private void SetCreditLimit(string clientType, User user)
        {
            if (clientType == "ImportantClient")
            {
                GetCreditLimit(user, 2);
            }
            else
            {
                user.HasCreditLimit = true;
                GetCreditLimit(user, 1);
            }
        }

        private void GetCreditLimit(User user, int creditMultiplier)
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * creditMultiplier;
                user.CreditLimit = creditLimit;
            }
        }

        private bool IsNotAdult(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age < 21;
        }

        private bool IsNotEmailValid(string email)
        {
            return !(email.Contains("@") && email.Contains("."));
        }
    }
}
