using CoWorking.App.Models;
using System.Linq;
using System;
using CoWorkingApp.Data.Tools;

namespace CoWorkingApp.Data
{
    public class UserData
    {
        private JsonManager<User>  jsonManager;

        public UserData()
        {
            jsonManager = new JsonManager<User>();
        }

        public bool CreateAdmin()
        {
            var usercollection = jsonManager.GetCollection();

            if(!usercollection.Any(p => p.Name == "ADMIN" &&
                                p.LastName =="ADMIN" &&
                                p.Email == "ADMIN"))
                                {
                                    try
                                    {
                                        var adminUser = new User()
                                    {
                                        Name = "ADMIN",
                                        LastName = "ADMIN",
                                        Email = "ADMIN",
                                        UserId = Guid.NewGuid(),
                                        Password = EncrypData.EncryptText("4dmin!")
                                    };
                                    usercollection.Add(adminUser);
                                    jsonManager.SaveCollection(usercollection);
                                    
                                    }
                                    catch (System.Exception)
                                    {
                                        return false;
                                    }
                                  return true;  
                                }
                                return true;
        }

        public User Login(string User, string Password, bool isAdmin = false)
        {
            var usercollection=  jsonManager.GetCollection();
            var passwordEncript = EncrypData.EncryptText(Password);
            if(isAdmin) User = "ADMIN";
            var userFound = usercollection.FirstOrDefault(p => p.Email == User && p.Password == passwordEncript);
            return userFound;

        }
        public bool CreateUser(User newUser)
        {
            newUser.Password = EncrypData.EncryptText(newUser.Password);
            var usercollection = jsonManager.GetCollection();
            usercollection.Add(newUser);
            jsonManager.SaveCollection(usercollection);
            return true;
        }
        public User FindUser(string email)
        {
            var userCollection = jsonManager.GetCollection();
            return userCollection.FirstOrDefault(p => p.Email == email);
        }

        public bool EditUser(User editUser)
        {
            editUser.Password = EncrypData.EncryptText(editUser.Password);
            var usercollection = jsonManager.GetCollection();
            var indexUser = usercollection.FindIndex(p => p.UserId == editUser.UserId);
            usercollection[indexUser] = editUser;
            jsonManager.SaveCollection(usercollection);
            return true;
        }
        public bool DeleteUser(Guid userID)
        {
            var userCollection = jsonManager.GetCollection();
            userCollection.Remove(userCollection.Find(p => p.UserId == userID));
            jsonManager.SaveCollection(userCollection);
            return true;
        }


    }
}